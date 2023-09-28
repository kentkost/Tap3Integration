using System.Runtime.InteropServices;

namespace Tap3Integration;

internal class Tap3MagicWrapper
{
    public enum ASN1Encoding
    {
        BER  = 0, 
        DER  = 1, 
        XER  = 2, 
        OER  = 3, 
        UPER = 4, 
        PER  = 5, 
        JER  = 6,
    }

    /// <summary>
    /// Read a file of any encoding and output to a new file with a new encoding.
    /// </summary>
    /// <param name="inputEncoding"></param>
    /// <param name="outputEncoding"></param>
    /// <param name="inFilePath"></param>
    /// <param name="outFilePath"></param>
    /// <returns></returns>
    [DllImport("TestDll_d.dll", EntryPoint = "decode_tap0311_datainterchange_file2file", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int decode_tap0311_datainterchange_file2file(int inputEncoding, int outputEncoding, string inFilePath, string outFilePath);

    /// <summary>
    /// Read a buffer of any encoding and output to a new file with a new encoding
    /// </summary>
    /// <param name="inputEncoding"></param>
    /// <param name="outputEncoding"></param>
    /// <param name="inputBuffer"></param>
    /// <param name="inBufferSize"></param>
    /// <param name="outFilePath"></param>
    /// <returns></returns>
    [DllImport("TestDll_d.dll", EntryPoint = "decode_tap0311_datainterchange_buffer2file", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int decode_tap0311_datainterchange_buffer2file(int inputEncoding, int outputEncoding, byte[] inputBuffer, ulong inBufferSize, string outFilePath);


    // Read a file of any encoding and output to new encoding.
    [DllImport("TestDll_d.dll", EntryPoint = "decode_tap0311_datainterchange_buffer2buffer", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int decode_tap0311_datainterchange_buffer2buffer(int inputEncoding, int outputEncoding, byte[] inputBuffer, ulong inBufferSize);


    // Read a file of any encoding and output to new encoding.
    [DllImport("TestDll_d.dll", EntryPoint = "decode_tap0311_datainterchange_file2buffer", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int decode_tap0311_datainterchange_file2buffer(int inputEncoding, int outputEncoding, string inFilePath);


}