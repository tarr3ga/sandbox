using System;
using System.Text;
using System.Runtime.InteropServices;

namespace EnumProc
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class WndSearcher
    {
        private delegate bool EnumWindowsProc(IntPtr hWnd, ref SearchData data);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc,
            [MarshalAsAttribute(UnmanagedType.Struct)] ref SearchData data);


    }

    public class SearchData
    {

    }
}
