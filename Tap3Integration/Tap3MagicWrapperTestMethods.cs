using System.Runtime.InteropServices;

namespace Tap3Integration;

internal class Tap3MagicWrapperTestMethods
{
    // Just a simple Test to see if it is set up correctly.
    [DllImport("TestDll_d.dll", EntryPoint = "DllTest", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int DllTest(int input);

    // Wont exit program. And will report no errors. More like a printf.
    [DllImport("TestDll_d.dll", EntryPoint = "perror_test", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int perror_test();

    // Test to see what return code the program gives. It returns 1 in this case and closes the program
    [DllImport("TestDll_d.dll", EntryPoint = "exit_test1", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int exit_test1();

    // Test to see what return code the program gives. It returns 0 in this case and closes the program
    [DllImport("TestDll_d.dll", EntryPoint = "exit_text0", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int exit_text0();

}