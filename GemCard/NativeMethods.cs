using System;
using System.Runtime.InteropServices;

namespace GemCard
{
    internal static class NativeMethods
    {

        private const string _lib = "winscard.dll";
        //private const string _lib = "libpcsclite.so";

        [DllImport(_lib, SetLastError = true)]
        internal static extern int SCardGetStatusChange(UInt32 hContext,
            UInt32 dwTimeout,
            [In, Out] SCard_ReaderState[] rgReaderStates,
            UInt32 cReaders);

        [DllImport(_lib, SetLastError = true)]
        internal static extern int SCardListReaders(UInt32 hContext,
            [MarshalAs(UnmanagedType.LPTStr)] string mszGroups,
            IntPtr mszReaders,
            out UInt32 pcchReaders);

        [DllImport(_lib, SetLastError = true)]
        internal static extern int SCardEstablishContext(UInt32 dwScope,
            IntPtr pvReserved1,
            IntPtr pvReserved2,
            IntPtr phContext);

        [DllImport(_lib, SetLastError = true)]
        internal static extern int SCardReleaseContext(UInt32 hContext);

        [DllImport(_lib, SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int SCardConnect(UInt32 hContext,
            [MarshalAs(UnmanagedType.LPTStr)] string szReader,
            UInt32 dwShareMode,
            UInt32 dwPreferredProtocols,
            IntPtr phCard,
            IntPtr pdwActiveProtocol);

        [DllImport(_lib, SetLastError = true)]
        internal static extern int SCardDisconnect(UInt32 hCard, UInt32 dwDisposition);

        [DllImport(_lib, SetLastError = true)]
        internal static extern int SCardTransmit(UInt32 hCard,
            [In] ref SCard_IO_Request pioSendPci,
            byte[] pbSendBuffer,
            UInt32 cbSendLength,
            IntPtr pioRecvPci,
            [Out] byte[] pbRecvBuffer,
            out UInt32 pcbRecvLength
            );

        [DllImport(_lib, SetLastError = true)]
        internal static extern int SCardBeginTransaction(UInt32 hContext);

        [DllImport(_lib, SetLastError = true)]
        internal static extern int SCardEndTransaction(UInt32 hContext, UInt32 dwDisposition);

        [DllImport(_lib, SetLastError = true)]
        internal static extern int SCardGetAttrib(UInt32 hCard,
            UInt32 dwAttribId,
            [Out] byte[] pbAttr,
            out UInt32 pcbAttrLen);

    }
}
