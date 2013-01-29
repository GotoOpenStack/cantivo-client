using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using GemCard;

namespace Adell.ItClient.Common.Services
{
    public class CardAuthenticationService
    {
        private static readonly string[] AllowedReaders =
            new[]
                {
                    "OMNIKEY CardMan 5x21-CL 0"
                };

        private readonly IObservable<Unit> _inserts;
        private readonly IObservable<Unit> _removes;
        private readonly CardNative _native;


        public IObservable<Tuple<uint, IObservable<Unit>>> Insertion
        {
            get
            {
                return
                    Observable.Create<Tuple<uint, IObservable<Unit>>>
                        (obs =>
                             {
                                 string reader;
                                 try
                                 {
                                     reader = (from r in _native.ListReaders()
                                               where AllowedReaders.Contains(r)
                                               select r).FirstOrDefault();
                                 }
                                 catch (Exception ex)
                                 {
                                     obs.OnError(ex);
                                     return Disposable.Empty;
                                 }

                                 if (reader != null)
                                     _native.StartCardEvents(reader);
                                 else
                                 {
                                     obs.OnError(new Exception("No cardreader available"));
                                     return Disposable.Empty;
                                 }
                                 var src =
                                     from insert in _inserts
                                     let id = GetCardId(reader)
                                     select Tuple.Create(id, _removes.Take(1));
                                 src.Subscribe(obs);
                                 return Disposable.Create(() => _native.StopCardEvents());
                             });
            }
        }

        public CardAuthenticationService()
        {
            _native = new CardNative();
            _inserts =
                Observable.FromEventPattern<EventArgs>(_native, "OnCardInserted")
                    .Select(_ => new Unit());

            _removes =
                Observable.FromEventPattern<EventArgs>(_native, "OnCardRemoved")
                    .Select(_ => new Unit());

        }


        //TODO: Make GetCardId Rx, with retry
        private IObservable<uint> GetCardIdAsync(string reader)
        {
            return Observable.Start(() => GetCardId(reader));
        }

        private uint GetCardId(string reader)
        {
            _native.Connect(reader, SHARE.Shared, PROTOCOL.T0orT1);
            var getUid = new byte[] {0xFF, (byte) Command.GetData, 0x00, 0x00, 0x00};
            var response = new APDUResponse(_native.TransmitRaw(getUid, 0xFF));
            _native.Disconnect(DISCONNECT.Unpower);
            response.ThrowError();
            var id = BitConverter.ToUInt32(response.Data, 0);
            return id;
        }
    }

}


// private byte[] key = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

//private static APDUCommand LoadKey(byte[] key)
//   {
//       return new APDUCommand(
//           0xFF, (byte)Command.ExternalAuthenticate,
//           0x20, 0x1a, key, (byte)key.Length);
//   }

//   private static APDUCommand GetUidCommand()
//   {
//       return new APDUCommand(
//           0xFF, (byte)Command.GetData,
//           0x00, 0x00, null, 0);
//   }
//http://www.nxp.com/acrobat_download2/other/identification/M001053_MF1ICS50_rev5_3.pdf
//Console.WriteLine("//////////////////////////////////////////");
//Console.WriteLine("connected");
//var cmd = new APDUCommand(0xFF, 0x41, 0x00, 0x00, null, 0);
//FF 82 00 00 06 A0 A1 A2 A3 A4 A5
// var cmd = new APDUCommand(0xFF, 0x82, 0, 0, 
//    new byte[] {0x06, 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5}, 0);
//cmd.Update(new APDUParam());
//var v = iCard.Transmit(cmd);
//var test = new byte[] {0xFF, 0x82, 0, 0, 0x06, 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5};
             

//Console.WriteLine(id);
//Console.WriteLine(_native.Transmit(GetUidCommand()).Error());
//Console.WriteLine(_native.Transmit(LoadKey(key)).Error());
//Console.WriteLine(new APDUResponse(iCard.TransmitRaw(test, 2)).Error());

//iCard.TransmitRaw( 
//    new byte[] {0x04, 0x01, 0x01, 0x00, 0x02, 0x18, 0x05, 0x91, 0xaf}, 2);

//Console.WriteLine(new APDUResponse(iCard.TransmitRaw(loadKey, 255)).Error());
//new APDUResponse(_native.TransmitRaw(auth, 255)).ThrowError();
//_native.Disconnect(DISCONNECT.Unpower);

//http://forums.oracle.com/forums/thread.jspa?messageID=7291328
//the $FF class commands are managed by the reader instead of the card, and are highly reader specific. (the only card command with a FF class is PPS, and must usually be the first command after reset)

//var getUid = new byte[] { 0xFF, (byte)Command.GetData, 0x00, 0x81, 0x00 };

//var auth = new byte[]
//               {
//                   0xFF, (byte) Command.InternalAuthenitcate,
//                   0x00, 0x04, 0x60, 0x00
//               };
