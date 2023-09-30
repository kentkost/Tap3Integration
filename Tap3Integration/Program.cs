using System.Text;
using Tap3Integration;
using static Tap3Integration.Tap3MagicWrapper;
using System.IO;
using System.Runtime.InteropServices;

namespace Tap3Integration;

class Program
{
    static string inPath = @"E:/repos/tap3magic/build/debug-readers/tap3-sample-DataInterChange-3_11.ber";
    static string outPath = @"E:/repos/tap3magic/build/debug-readers/resultssss.xer";
    static ASN1Encoding inEncoding = ASN1Encoding.BER;
    static ASN1Encoding outEncoding = ASN1Encoding.XER;

    static void Main(string[] args)
    {
        TestingFile2File(); // ✅
        //TestingBuffer2File(); // ✅
        //TestingBuffer2Buffer(); // ✅
        //TestingFile2Buffer(); // ✅
        Console.WriteLine("Ended Successfully");
    }

    static void TestingFile2File()
    {
        Tap3MagicWrapper.DecodeFileToFile(inEncoding, outEncoding, inPath, outPath);
    }

    static void TestingBuffer2File()
    {
        var inputBuffer = File.ReadAllBytes(inPath);
        var length = Convert.ToUInt64(inputBuffer.Length);
        Tap3MagicWrapper.DecodeBufferToFile(inEncoding, outEncoding, inputBuffer, length, outPath);
    }

    static void TestingBuffer2Buffer()
    {
        var inputBuffer = File.ReadAllBytes(inPath);
        var length = Convert.ToUInt64(inputBuffer.Length);
        var encodedBytes = Tap3MagicWrapper.DecodeBufferToBuffer(inEncoding, outEncoding, inputBuffer, length);
        var str = System.Text.Encoding.UTF8.GetString(encodedBytes);
    }

    static void TestingFile2Buffer()
    {

        var encodedBytes = Tap3MagicWrapper.DecodeFileToBuffer(inEncoding, outEncoding, inPath);

        var str = System.Text.Encoding.UTF8.GetString(encodedBytes);

    }
}