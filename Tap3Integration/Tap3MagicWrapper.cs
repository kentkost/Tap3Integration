using System.Runtime.InteropServices;

namespace Tap3Integration;

public class Tap3MagicWrapper
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
    /// Reads a file of any encoding and outputs a new file with a new encoding.
    /// </summary>
    /// <param name="inputEncoding">The encoding of the input file</param>
    /// <param name="outputEncoding">The encoding of the output file</param>
    /// <param name="inFilePath">The file path for the input file</param>
    /// <param name="outFilePath">The file path for the output file</param>
    /// <returns>-1 on fail and number of bytes encoded otherwise</returns>
    [DllImport("TestDll_d.dll", EntryPoint = "decode_tap0311_datainterchange_file2file", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern int decode_tap0311_datainterchange_file2file(int inputEncoding, int outputEncoding, string inFilePath, string outFilePath);

    /// <summary>
    /// Reads a buffer of any encoding and outputs a new file with a new encoding
    /// </summary>
    /// <param name="inputEncoding">The encoding of the input file</param>
    /// <param name="outputEncoding">The encoding of the output file</param>
    /// <param name="inputBuffer">The input buffer</param>
    /// <param name="inBufferSize">The size of the input buffer</param>
    /// <param name="outFilePath">The file path for the output file</param>
    /// <returns>-1 on fail and number of bytes encoded otherwise</returns>
    [DllImport("TestDll_d.dll", EntryPoint = "decode_tap0311_datainterchange_buffer2file", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern int decode_tap0311_datainterchange_buffer2file(int inputEncoding, int outputEncoding, byte[] inputBuffer, ulong inBufferSize, string outFilePath);


    /// <summary>
    /// Reads a buffer and writes a new output buffer to an address
    /// </summary>
    /// <param name="inputEncoding">The encoding of the input file</param>
    /// <param name="outputEncoding">The encoding of the output file</param>
    /// <param name="inputBuffer">The input buffer</param>
    /// <param name="inBufferSize">The size of the input buffer</param>
    /// <param name="byteArrayPtr">The address that the output buffer is written to</param>
    /// <returns>Number of bytes encoded to output buffer</returns>
    [DllImport("TestDll_d.dll", EntryPoint = "decode_tap0311_datainterchange_buffer2buffer", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern int decode_tap0311_datainterchange_buffer2buffer(int inputEncoding, int outputEncoding, byte[] inputBuffer, ulong inBufferSize, ref IntPtr byteArrayPtr);

    /// <summary>
    /// Reads a file and writes a new output buffer to an address
    /// </summary>
    /// <param name="inputEncoding">The encoding of the input file</param>
    /// <param name="outputEncoding">The encoding of the output file</param>
    /// <param name="inFilePath">The file path to the input buffer</param>
    /// <param name="byteArrayPtr">The address that the output buffer is written to</param>
    /// <returns>Number of bytes encoded to output buffer</returns>
    [DllImport("TestDll_d.dll", EntryPoint = "decode_tap0311_datainterchange_file2buffer", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern int decode_tap0311_datainterchange_file2buffer(int inputEncoding, int outputEncoding, string inFilePath, ref IntPtr byteArrayPtr);


    /// <summary>
    /// Reads a file of any encoding and outputs a new file with a new encoding.
    /// </summary>
    /// <param name="inputEncoding">The encoding of the input file</param>
    /// <param name="outputEncoding">The encoding of the output file</param>
    /// <param name="inFilePath">The file path for the input file</param>
    /// <param name="outFilePath">The file path for the output file</param>
    /// <returns>-1 on fail and number of bytes encoded otherwise</returns>
    public static int DecodeFileToFile(ASN1Encoding inputEncoding, ASN1Encoding outputEncoding, string inFilePath, string outFilePath)
    {
        int inEnc = (int)inputEncoding;
        int outEnc = (int)outputEncoding;
        return Tap3MagicWrapper.decode_tap0311_datainterchange_file2file(inEnc, outEnc, inFilePath, outFilePath);
    }

    /// <summary>
    /// Reads a buffer of any encoding and outputs a new file with a new encoding
    /// </summary>
    /// <param name="inputEncoding">The encoding of the input file</param>
    /// <param name="outputEncoding">The encoding of the output file</param>
    /// <param name="inputBuffer">The input buffer</param>
    /// <param name="inputBufferSize">The size of the input buffer</param>
    /// <param name="outFilePath">The file path for the output file</param>
    /// <returns>-1 on fail and number of bytes encoded otherwise</returns>
    public static int DecodeBufferToFile(ASN1Encoding inputEncoding, ASN1Encoding outputEncoding, byte[] inputBuffer, ulong inputBufferSize, string outFilePath)
    {
        int inEnc = (int)inputEncoding;
        int outEnc = (int)outputEncoding;
        return Tap3MagicWrapper.decode_tap0311_datainterchange_buffer2file(inEnc, outEnc, inputBuffer, inputBufferSize, outFilePath);
    }

    /// <summary>
    /// Reads a buffer and writes a new byte array
    /// </summary>
    /// <param name="inputEncoding">The encoding of the input file</param>
    /// <param name="outputEncoding">The encoding of the output buffer</param>
    /// <param name="inputBuffer">The input buffer</param>
    /// <param name="inputBufferSize">The size of the input buffer</param>
    /// <returns>A byte array corresponding to the output buffer</returns>
    public static byte[] DecodeBufferToBuffer(ASN1Encoding inputEncoding, ASN1Encoding outputEncoding, byte[] inputBuffer, ulong inputBufferSize)
    {
        int inEnc = (int)inputEncoding;
        int outEnc = (int)outputEncoding;
        IntPtr byteArrayPtr = IntPtr.Zero;

        var encodedBytes = Tap3MagicWrapper.decode_tap0311_datainterchange_buffer2buffer(inEnc, outEnc, inputBuffer, inputBufferSize, ref byteArrayPtr);

        byte[] outBuffer = new byte[encodedBytes];
        Marshal.Copy(byteArrayPtr, outBuffer, 0, encodedBytes);
        return outBuffer;
    }

    /// <summary>
    /// Reads a buffer and writes a string
    /// </summary>
    /// <param name="inputEncoding">The encoding of the input file</param>
    /// <param name="outputEncoding">The encoding of the output string</param>
    /// <param name="inputBuffer">The input buffer</param>
    /// <param name="inputBufferSize">The size of the input buffer</param>
    /// <returns>A string representation of the outout buffer. Helpful when output is XER or JER</returns>
    public static string DecodeBufferToString(ASN1Encoding inputEncoding, ASN1Encoding outputEncoding, byte[] inputBuffer, ulong inputBufferSize)
    {
        int inEnc = (int)inputEncoding;
        int outEnc = (int)outputEncoding;
        IntPtr byteArrayPtr = IntPtr.Zero;

        var encodedBytes = Tap3MagicWrapper.decode_tap0311_datainterchange_buffer2buffer(inEnc, outEnc, inputBuffer, inputBufferSize, ref byteArrayPtr);

        byte[] outBuffer = new byte[encodedBytes];
        Marshal.Copy(byteArrayPtr, outBuffer, 0, encodedBytes);
        var str = System.Text.Encoding.UTF8.GetString(outBuffer);

        return str;
    }

    /// <summary>
    /// Reads a file and writes a string
    /// </summary>
    /// <param name="inputEncoding">The encoding of the input file</param>
    /// <param name="outputEncoding">The encoding of the output string</param>
    /// <param name="inFilePath">The file path for the input file</param>
    /// <returns>A string representation of the outout buffer. Helpful when output is XER or JER</returns>
    public static string DecodeFileToString(ASN1Encoding inputEncoding, ASN1Encoding outputEncoding, string inFilePath)
    {
        int inEnc = (int)inputEncoding;
        int outEnc = (int)outputEncoding;
        IntPtr byteArrayPtr = IntPtr.Zero;

        var encodedBytes = Tap3MagicWrapper.decode_tap0311_datainterchange_file2buffer(inEnc, outEnc, inFilePath, ref byteArrayPtr);
        byte[] outBuffer = new byte[encodedBytes];
        Marshal.Copy(byteArrayPtr, outBuffer, 0, encodedBytes);
        var str = System.Text.Encoding.UTF8.GetString(outBuffer);

        return str;
    }

    /// <summary>
    /// Reads a file and writes a byte array
    /// </summary>
    /// <param name="inputEncoding">The encoding of the input file</param>
    /// <param name="outputEncoding">The encoding of the output array</param>
    /// <param name="inFilePath">The file path for the input file</param>
    /// <returns>A byte array corresponding to the output buffer</returns>
    public static byte[] DecodeFileToBuffer(ASN1Encoding inputEncoding, ASN1Encoding outputEncoding, string inFilePath)
    {
        int inEnc = (int)inputEncoding;
        int outEnc = (int)outputEncoding;
        IntPtr byteArrayPtr = IntPtr.Zero;

        var encodedBytes = Tap3MagicWrapper.decode_tap0311_datainterchange_file2buffer(inEnc, outEnc, inFilePath, ref byteArrayPtr);
        byte[] outBuffer = new byte[encodedBytes];
        Marshal.Copy(byteArrayPtr, outBuffer, 0, encodedBytes);
        
        return outBuffer;
    }
}