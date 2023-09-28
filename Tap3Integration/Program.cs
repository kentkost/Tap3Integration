using System.Text;
using Tap3Integration;
using static Tap3Integration.Tap3MagicWrapper;
using System.IO;

namespace Tap3Integration;

class Program
{
    static string inPath = @"E:/repos/tap3magic/build/debug-readers/tap3-sample-DataInterChange-3_11.ber";
    static string outPath = @"E:/repos/tap3magic/build/debug-readers/results.xer";
    static int inEncoding = (int)ASN1Encoding.BER;
    static int outEncoding = (int)ASN1Encoding.XER;

    static void Main(string[] args)
    {
        //TestingFile2File(); // ✅
        //TestingBuffer2File(); // ✅
        TestingBuffer2Buffer();
    }


    static void TestingFile2File()
    {
        Tap3MagicWrapper.decode_tap0311_datainterchange_file2file(inEncoding, outEncoding, inPath, outPath);
    }

    static void TestingBuffer2File()
    {
        var inputBuffer = File.ReadAllBytes(inPath);
        var length = Convert.ToUInt64(inputBuffer.Length);
        Tap3MagicWrapper.decode_tap0311_datainterchange_buffer2file(inEncoding, outEncoding, inputBuffer, length, outPath);
    }

    static void TestingBuffer2Buffer()
    {
        var inputBuffer = File.ReadAllBytes(inPath);
        var length = Convert.ToUInt64(inputBuffer.Length);
        var res = Tap3MagicWrapper.decode_tap0311_datainterchange_buffer2buffer(inEncoding, outEncoding, inputBuffer, length);
        //string str = Encoding.Default.GetString(res);
    }
}