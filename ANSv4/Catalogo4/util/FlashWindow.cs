//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Catalogo.util
{
    public class FlashWindow
    {
	    public enum enuFlashOptions : uint
	    {
		    FLASHW_ALL = 0x3,
		    // Flash both the window caption and taskbar button. 
		    // This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
		    FLASHW_CAPTION = 0x1,
		    // Flash the window caption. 
		    FLASHW_STOP = 0,
		    // Stop flashing. The system restores the window to its original state. 
		    FLASHW_TIMER = 0x4,
		    // Flash continuously, until the FLASHW_STOP flag is set. 
		    FLASHW_TIMERNOFG = 0xc,
		    // Flash continuously until the window comes to the foreground. 
		    FLASHW_TRAY = 0x2
	    }

	    public struct FlashWindowInfo
	    {
		    public int cbSize;
		    public IntPtr hwnd;
		    public uint dwFlags;
		    public uint uCount;
		    public uint dwTimeout;
	    }
	  
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	    public static extern bool FlashWindowEx(ref FlashWindowInfo pInfo);

	    internal void FlashWindow1(Form frmForm, enuFlashOptions FlashWindowInfoFlags, uint intFlashTimes = 5)
	    {
		    if ((frmForm.WindowState == FormWindowState.Minimized) | FlashWindowInfoFlags == enuFlashOptions.FLASHW_STOP) {
			    FlashWindowInfo info = default(FlashWindowInfo);
			    var _with1 = info;
			    _with1.cbSize = Marshal.SizeOf(info);
                _with1.dwFlags = uint.Parse(FlashWindowInfoFlags.ToString());
 			    // See enumeration for flag values 
			    _with1.dwTimeout = 0;
			    //Flash rate in ms or default cursor blink rate 
                _with1.hwnd = frmForm.Handle;  
			    _with1.uCount = intFlashTimes;
			    // Number of times to flash 
			    FlashWindowEx(ref info);
		    }
	    }



//        //CDialog derived class
//        CMyDialog* m_pMyDlg;
//        FLASHWINFO info;
//        void CreateFlashWindow()
//        {
//            //Create a window
//            m_pMyDlg = new CMyDialog();
//            m_pMyDlg->Create(IDD_MYDIALOG);

//            //Show window
//            m_pMyDlg->ShowWindow(SW_MINIMIZE);

//            //Fill blinking info
//            if (m_pMyDlg->m_hWnd)
//            {
//                info.cbSize = sizeof(info);
//                info.hwnd = m_pMyDlg->m_hWnd;
//                info.dwFlags = FLASHW_ALL;
//                info.dwTimeout = 0;
//                info.uCount = 3;
//            }
//        }

//        //any time the minimized application window
//        //need to get user attention, call this method
//        void OnNotifyUser()
//{
//::FlashWindowEx(&info);
//}

    }
}


