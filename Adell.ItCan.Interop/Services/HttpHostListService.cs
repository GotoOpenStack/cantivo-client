using System;
using System.IO;
using System.Net;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Adell.Convenience.Extensions;
using Adell.ItCan.Interop.Entities;

namespace Adell.ItCan.Interop.Services
{
    public class HttpHostListService : IHostListService
    {
        private NetworkCredential _creds;
        private IPEndPoint _ipEndPoint;
        private const string path = "/api/?key=";

        public HttpHostListService(NetworkCredential creds, IPEndPoint iPEndPoint)
        {
            _creds = creds;
            _ipEndPoint = iPEndPoint;
        }

        private static string GetUrl(NetworkCredential creds, IPEndPoint ipEndpoint)
        {
            var key = creds.UserName + ":" + creds.Password;
            return String.Format("http://{0}:{1}{2}{3}",
                ipEndpoint.Address, ipEndpoint.Port, path, key.ToBase64String());
        }

        private static IEnumerable<desktop> Deserialize(string s)
        {
            var reader = new XmlTextReader(new StringReader(s));
            var serializer = new XmlSerializer(typeof (config));
            var config = (config) serializer.Deserialize(reader);
            if (config.error != null)
                throw new Exception(String.Format("Server reported error: {0}", config.error));
            return new List<desktop>(config.desktops);
        }

        public IObservable<IEnumerable<desktop>> GetHostListAsync()
        {
            
            var url = GetUrl(_creds, _ipEndPoint);
            var test = new WebClient { Proxy = null };

            var query =
                Observable
                .FromEventPattern<DownloadStringCompletedEventArgs>(test, "DownloadStringCompleted")
                    .Select(evt => evt.EventArgs)
                    .Assert(evt => evt.Error == null, evt => evt.Error);
            test.DownloadStringAsync(new Uri(url));
            return from q in query select Deserialize(q.Result);
        }
    }
}