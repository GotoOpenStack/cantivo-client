using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GemCard
{

    //http://www.ttfn.net/techno/smartcards/iso7816_4.html#ss6_1
    public enum Command : byte
    {
        EraseBinary  = 0x0E,
        Verify = 0x20,
        ManageChannel = 0x70,
        ExternalAuthenticate = 0x82,
        GetChallenge = 0x84,
        InternalAuthenitcate = 0x88,
        SelectFile = 0xA4,
        ReadBinary = 0xB0,
        ReadRecords = 0xB2,
        GetResponse = 0xC0,
        Envelope = 0xC2,
        GetData = 0xCA,
        WriteBinary = 0xD0,
        WriteRecord = 0xD2,
        UpdateBinary = 0xD6,
        PutData = 0xDA,
        UpdateData =  0xDC,
        AppendRecord = 0xE2
    }


    public static class APDUResponseExtensions
    {
        

        public static void ThrowError(this APDUResponse response)
        {
            if (response.SW1 == 0x90) return;
            
            var simple = new Dictionary<byte, string>
                        {
                            {0x90, "No further qualification"},
                            {0x61, "SW2 indicates the number of response bytes still available"},
                            {0x64, "State of non-volatile memory unchanged"},
                            {0x66, "Reserved for security-related issues"},
                            {0x67, "Wrong length"},
                            {0x6B, "Wrong parameter(s) P1-P2"},
                            {0x6C, "Wrong length Le: SW2 indicates the exact length "},
                            {0x6D, "Instruction code not supported or invalid"},
                            {0x6E, "Class not supported"},
                            {0x6F, "No precise diagnosis"},
                        };
            var complex =
                new Dictionary<byte, Dictionary<byte, string>>
                    {
                        {0x62, new Dictionary<byte, string>
                                   {
                                      {0x00, "State of non-volatile memory unchanged, No information given"}, 
                                      {0x81, "State of non-volatile memory unchanged, Part of returned data may be corrupted"}, 
                                      {0x82, "State of non-volatile memory unchanged, End of file/record reached before reading Le bytes"}, 
                                      {0x83, "State of non-volatile memory unchanged, Selected file invalidated"}, 
                                      {0x84, "State of non-volatile memory unchanged, FCI not formatted according to 5.1.5"}, 
                                   }},
                        {0x63, new Dictionary<byte, string>
                                   {
                                      {0x00, "State of non-volatile memory changed, No information given"}, 
                                      {0x81, "State of non-volatile memory changed, File filled up by the last write"} 
                           //           {0xCX, "State of non-volatile memory changed, Counter provided by 'X' (valued from 0 to 15) (exact meaning depending on the command)"}, 
                                   }},
                        {0x65, new Dictionary<byte, string>
                                   {
                                      {0x00, "State of non-volatile memory changed, No information given"}, 
                                      {0x81, "State of non-volatile memory changed, Memory failure"}, 
                                   }},
                        {0x68, new Dictionary<byte, string>
                                   {
                                      {0x00, "Functions in CLA not supported, No information given"}, 
                                      {0x81, "Functions in CLA not supported, Logical channel not supported"}, 
                                      {0x82, "Functions in CLA not supported, Secure messaging not supported"}, 
                                   }},

                                   {0x69, new Dictionary<byte, string>
                                   {
                                      {0x00, "Command not allowed, No information given"}, 
                                      {0x81, "Command not allowed, Command incompatible with file structure "}, 
                                      {0x82, "Command not allowed, Security status not satisfied"}, 
                                      {0x83, "Command not allowed, Authentication method blocked"}, 
                                      {0x84, "Command not allowed, Referenced data invalidated"}, 
                                      {0x85, "Command not allowed, Conditions of use not satisfied"}, 
                                      {0x86, "Command not allowed, Command not allowed (no current EF)"}, 
                                      {0x87, "Command not allowed, Expected SM data objects missing"}, 
                                      {0x88, "Command not allowed, SM data objects incorrect"}, 
                                   }},
                                {0x6A, new Dictionary<byte, string>
                                   {
                                      {0x00, "Wrong parameter(s) P1-P2, No information given"}, 
                                      {0x80, "Wrong parameter(s) P1-P2, Incorrect parameters in the data field"}, 
                                      {0x81, "Wrong parameter(s) P1-P2, Function not supported"}, 
                                      {0x82, "Wrong parameter(s) P1-P2, File not found"}, 
                                      {0x83, "Wrong parameter(s) P1-P2, Record not found"}, 
                                      {0x84, "Wrong parameter(s) P1-P2, Not enough memory space in the file"}, 
                                      {0x85, "Wrong parameter(s) P1-P2, Lc inconsistent with TLV structure"}, 
                                      {0x86, "Wrong parameter(s) P1-P2, Incorrect parameters P1-P2"}, 
                                      {0x87, "Wrong parameter(s) P1-P2, Lc inconsistent with P1-P2"}, 
                                      {0x88, "Wrong parameter(s) P1-P2, Referenced data not found"}
                                   }}
                    };

            var error = default(string);
            if (simple.ContainsKey(response.SW1))
                error = simple[response.SW1];

            else if (complex.ContainsKey(response.SW1) &&
                complex[response.SW1].ContainsKey(response.SW2))
            {
                error = complex[response.SW1][response.SW2];
            }
            else 
                error = "Unknown error";
            throw new Exception(error);
        }
    }


     public class CNameAttribue : Attribute
    {
        public CNameAttribue(string name)
        {
            
        }

    }
     public enum ErrorCodes : uint
     {
         [CNameAttribue("SCARD_F_INTERNAL_ERROR")]
         InternalError = 0x80100001,
         [CNameAttribue("SCARD_E_CANCELLED")]
         Cancelled,

         [CNameAttribue("SCARD_E_INVALID_HANDLE")]
         InvaliHandle,

         [CNameAttribue("SCARD_E_INVALID_PARAMETER")]
         InvalidParameter,

         [CNameAttribue("SCARD_E_INVALID_TARGET")]
         InvalidTarget,

         [CNameAttribue("SCARD_E_NO_MEMORY")]
         NoMemory,

         [CNameAttribue("SCARD_F_WAITED_TOO_LONG")]
         WaitedTooLong,

         [CNameAttribue("SCARD_E_INSUFFICIENT_BUFFER")]
         InsufficientBuffer,
         [CNameAttribue("SCARD_E_UNKNOWN_READER")]
         UnknownReader,
         [CNameAttribue("SCARD_E_TIMEOUT")]
         TimeOut,

         [CNameAttribue("SCARD_E_SHARING_VIOLATION")]
         SharingViolation,
         [CNameAttribue("SCARD_E_NO_SMARTCARD")]
         NoSmartcard,
         [CNameAttribue("SCARD_E_UNKNOWN_CARD")]
         UnknownCard,
         [CNameAttribue("SCARD_E_CANT_DISPOSE")]
         CantDispose,

         [CNameAttribue("SCARD_E_PROTO_MISMATCH")]
         ProtocolMismatch,
         [CNameAttribue("SCARD_E_NOT_READY")]
         NotReady,
         [CNameAttribue("SCARD_E_INVALID_VALUE")]
         InvalidValue,
         [CNameAttribue("SCARD_E_SYSTEM_CANCELLED")]
         SystemCancelled,

         [CNameAttribue("SCARD_F_COMM_ERROR")]
         CommunicationError,
         [CNameAttribue("SCARD_F_UNKNOWN_ERROR")]
         UnknownError,



         [CNameAttribue("SCARD_E_INVALID_ATR")]
         InvalidAtr,
         [CNameAttribue("SCARD_E_NOT_TRANSACTED")]
         NotTransacted,
         [CNameAttribue("SCARD_E_READER_UNAVAILABLE")]
         ReaderUnavailable,

         [CNameAttribue("SCARD_P_SHUTDOWN")]
         Shutdown,

         [CNameAttribue("SCARD_E_PCI_TOO_SMALL")]
         PciTooSmall,
         [CNameAttribue("SCARD_E_READER_UNSUPPORTED")]
         ReaderUnsupported,

         [CNameAttribue("SCARD_E_DUPLICATE_READER")]
         DuplicateReader,

         [CNameAttribue("SCARD_E_CARD_UNSUPPORTED")]
         CardUnsupported,

         [CNameAttribue("SCARD_E_NO_SERVICE")]
         NoService,

         [CNameAttribue("SCARD_E_SERVICE_STOPPED")]
         ServiceStopped,

         [CNameAttribue("SCARD_E_UNEXPECTED")]
         Unexpected,

         [CNameAttribue("SCARD_E_ICC_INSTALLATION")]
         IccInstallation,

         [CNameAttribue("SCARD_E_ICC_CREATEORDER")]
         IccCreatreOrder,

         [CNameAttribue("SCARD_E_UNSUPPORTED_FEATURE")]
         UnsupportedFeature,

         [CNameAttribue("SCARD_E_DIR_NOT_FOUND")]
         DirectoryNotFound,

         [CNameAttribue("SCARD_E_FILE_NOT_FOUND")]
         FileNotFound,

         [CNameAttribue("SCARD_E_NO_DIR")]
         NoDirectory,

         [CNameAttribue("SCARD_E_NO_FILE")]
         NoFile,

         [CNameAttribue("SCARD_E_NO_ACCESS")]
         NoAccess,

         [CNameAttribue("SCARD_E_WRITE_TOO_MANY")]
         WriteTooMany,

         [CNameAttribue("SCARD_E_BAD_SEEK")]
         BadSeek,

         [CNameAttribue("SCARD_E_INVALID_CHV")]
         InvalidChv,

         [CNameAttribue("SCARD_E_UNKNOWN_RES_MNG")]
         UnknownResourcesMng,
         [CNameAttribue("SCARD_E_NO_SUCH_CERTIFICATE")]
         NoSuchCertitificate,

         [CNameAttribue("SCARD_E_CERTIFICATE_UNAVAILABLE")]
         CertificateUnavailable,

         [CNameAttribue("SCARD_E_NO_READERS_AVAILABLE")]
         NoReadersAvailable,

         [CNameAttribue("SCARD_E_COMM_DATA_LOST")]
         CommDataLost,
         [CNameAttribue("SCARD_E_NO_KEY_CONTAINER")]
         NoKeyContainer,
         [CNameAttribue("SCARD_E_SERVER_TOO_BUSY")]
         ServerTooBusy,

         [CNameAttribue("SCARD_W_UNSUPPORTED_CARD")]
         UnsupportedCard,

         [CNameAttribue("SCARD_W_UNRESPONSIVE_CARD")]
         UnresponsiveCard = 0x80100066

         //
         //SCARD_W_UNPOWERED_CARD
         //SCARD_W_RESET_CARD
         //SCARD_W_REMOVED_CARD
         //SCARD_W_SECURITY_VIOLATION
         //SCARD_W_WRONG_CHV
         //SCARD_W_CHV_BLOCKED
         //SCARD_W_EOF
         //SCARD_W_CANCELLED_BY_USER
         //SCARD_W_CARD_NOT_AUTHENTICATED
     }
}
