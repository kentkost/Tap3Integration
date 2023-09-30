using Tap3Integration;
using static Tap3Integration.Tap3MagicWrapper;

namespace Tap3IntegrationTests
{
    public class Tap3MagicWrapperTests
    {
        static string inPath = @"E:/repos/tap3magic/build/debug-readers/tap3-sample-DataInterChange-3_11.ber";
        static string outPath = @"E:/repos/tap3magic/build/debug-readers/resultssss.xer";
        static ASN1Encoding inEncoding = ASN1Encoding.BER;
        static ASN1Encoding outEncoding = ASN1Encoding.XER;


        [Fact]
        public void TestingFile2File()
        {
            int res = Tap3MagicWrapper.DecodeFileToFile(inEncoding, outEncoding, inPath, outPath);
            Assert.True(res > 0);
        }
        [Fact]
        public void TestingBuffer2File()
        {
            var inputBuffer = File.ReadAllBytes(inPath);
            var length = Convert.ToUInt64(inputBuffer.Length);
            int res = Tap3MagicWrapper.DecodeBufferToFile(inEncoding, outEncoding, inputBuffer, length, outPath);
            Assert.True(res > 0);
        }

        [Fact]
        public void TestingBuffer2Buffer()
        {
            var inputBuffer = File.ReadAllBytes(inPath);
            var length = Convert.ToUInt64(inputBuffer.Length);
            var encodedBytes = Tap3MagicWrapper.DecodeBufferToBuffer(inEncoding, outEncoding, inputBuffer, length);
            var str = System.Text.Encoding.UTF8.GetString(encodedBytes);
            Assert.StartsWith("<DataInterChange",str,StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact]
        public void TestingFile2Buffer()
        {
            var encodedBytes = Tap3MagicWrapper.DecodeFileToBuffer(inEncoding, outEncoding, inPath);

            var str = System.Text.Encoding.UTF8.GetString(encodedBytes);
            Assert.StartsWith("<DataInterChange", str, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}