using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace SortDesktopIcon
{
    public class WinAPI
    {
        public const uint LVM_FIRST = 0x1000;

        public const uint LVM_GETITEMCOUNT = LVM_FIRST + 4;

        public const uint LVM_GETITEMW = LVM_FIRST + 75;

        public const uint LVM_GETITEMPOSITION = LVM_FIRST + 16;

        public const uint LVM_GETHEADER = LVM_FIRST + 31;

        public const uint LVM_SETITEMW = LVM_FIRST + 76;

        public const uint LVM_SETITEMPOSITION = LVM_FIRST + 15;

        public const uint LVM_REDRAWITEMS = LVM_FIRST + 21;



        [DllImport("user32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, uint lParam);

        [DllImport("user32.DLL")]
        public static extern IntPtr FindWindow(string lpszClass, string lpszWindow);

        [DllImport("user32.DLL")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint dwProcessId);



        public const uint PROCESS_VM_OPERATION = 0x0008;

        public const uint PROCESS_VM_READ = 0x0010;

        public const uint PROCESS_VM_WRITE = 0x0020;



        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        public const uint MEM_COMMIT = 0x1000;

        public const uint MEM_RELEASE = 0x8000;



        public const uint MEM_RESERVE = 0x2000;

        public const uint PAGE_READWRITE = 4;



        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);



        [DllImport("kernel32.dll")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);



        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);



        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);



        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
        // 获得窗口矩形
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        // 获得客户区矩形
        [DllImport("user32.dll")]
        public static extern int GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        // 矩形结构
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            int left;
            int top;
            int right;
            int bottom;
        }


        public struct LVITEM
        {

            public int mask;

            public int iItem;

            public int iSubItem;

            public int state;

            public int stateMask;

            public IntPtr pszText; // string

            public int cchTextMax;

            public int iImage;

            public IntPtr lParam;

            public int iIndent;

            public int iGroupId;

            public int cColumns;

            public IntPtr puColumns;

        }

        public static uint MAKELPARAM(double wLow, double wHigh)
        {
            return (uint)(wHigh * 0x10000 + wLow);
        }

        public static int LVIF_TEXT = 0x0001;


        /// <summary>
        /// 获取ListView行数
        /// </summary>
        /// <param name="AHandle"></param>
        /// <returns></returns>
        public static int ListView_GetItemCount(IntPtr AHandle)
        {

            return SendMessage(AHandle, LVM_GETITEMCOUNT, 0, 0);

        }

        /// <summary>
        /// 获取ListView标题栏句柄
        /// </summary>
        /// <param name="AHandle"></param>
        /// <returns></returns>
        public static IntPtr ListView_GetHeader(IntPtr AHandle)
        {
            return (IntPtr)SendMessage(AHandle, LVM_GETHEADER, 0, 0);
        }



        public static bool ListView_GetItemPosition(IntPtr AHandle, int AIndex, IntPtr APoint)
        {

            return SendMessage(AHandle, LVM_GETITEMPOSITION, AIndex, APoint.ToInt32()) != 0;

        }

        public static bool ListView_RedrawItems(IntPtr AHandle, int iFirst, int iLast)
        {
            return SendMessage(AHandle, LVM_REDRAWITEMS, iFirst, iLast) != 0;
        }
    }
}

