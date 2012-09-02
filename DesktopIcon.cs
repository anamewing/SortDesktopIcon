using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;


namespace SortDesktopIcon
{
    class DesktopIcon
    {

        /// <summary>
        /// 获取所有桌面项
        /// </summary>
        public IList<IcoObj> getICO()
        {
            IntPtr vHandle = WinAPI.FindWindow("Progman", null);

            vHandle = WinAPI.FindWindowEx(vHandle, IntPtr.Zero, "SHELLDLL_DefView", null);

            vHandle = WinAPI.FindWindowEx(vHandle, IntPtr.Zero, "SysListView32", null);

            int vItemCount = WinAPI.ListView_GetItemCount(vHandle);

            uint vProcessId;

            WinAPI.GetWindowThreadProcessId(vHandle, out vProcessId);



            IntPtr vProcess = WinAPI.OpenProcess(WinAPI.PROCESS_VM_OPERATION | WinAPI.PROCESS_VM_READ |

                WinAPI.PROCESS_VM_WRITE, false, vProcessId);

            IntPtr vPointer = WinAPI.VirtualAllocEx(vProcess, IntPtr.Zero, 4096,

                WinAPI.MEM_RESERVE | WinAPI.MEM_COMMIT, WinAPI.PAGE_READWRITE);

            IList<IcoObj> icoObj = new List<IcoObj>();//所有桌面项目

            try
            {

                for (int i = 0; i < vItemCount; i++)
                {

                    byte[] vBuffer = new byte[256];

                    WinAPI.LVITEM[] vItem = new WinAPI.LVITEM[1];

                    vItem[0].mask = WinAPI.LVIF_TEXT;

                    vItem[0].iItem = i;

                    vItem[0].iSubItem = 0;

                    vItem[0].cchTextMax = vBuffer.Length;

                    vItem[0].pszText = (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(WinAPI.LVITEM)));

                    uint vNumberOfBytesRead = 0;



                    WinAPI.WriteProcessMemory(vProcess, vPointer,

                        Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0),

                        Marshal.SizeOf(typeof(WinAPI.LVITEM)), ref vNumberOfBytesRead);

                    WinAPI.SendMessage(vHandle, WinAPI.LVM_GETITEMW, i, vPointer.ToInt32());

                    WinAPI.ReadProcessMemory(vProcess,

                        (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(WinAPI.LVITEM))),

                        Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0),

                        vBuffer.Length, ref vNumberOfBytesRead);

                    string vText = Encoding.Unicode.GetString(vBuffer, 0,

                        (int)vNumberOfBytesRead);

                    WinAPI.ListView_GetItemPosition(vHandle, i, vPointer);

                    Point[] vPoint = new Point[1];



                    WinAPI.ReadProcessMemory(vProcess, vPointer,

                        Marshal.UnsafeAddrOfPinnedArrayElement(vPoint, 0),

                        Marshal.SizeOf(typeof(Point)), ref vNumberOfBytesRead);

                    icoObj.Add(new IcoObj(vText.Substring(0, vText.IndexOf("\0")), vHandle, vPoint[0]));
                }

            }

            finally
            {

                WinAPI.VirtualFreeEx(vProcess, vPointer, 0, WinAPI.MEM_RELEASE);

                WinAPI.CloseHandle(vProcess);

            }

            return icoObj;
        }

    }
}
