using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VKBoard.VKeyboard.Services
{
    public enum KeyboardLayout
    {
        RUS, ENG
    }

    public class VKeyboardOperationsService
    {
        #region Singleton
        private VKeyboardOperationsService() { }

        private static readonly object lockerObject = new object();

        private static VKeyboardOperationsService inctance;

        public static VKeyboardOperationsService Instance
        {
            get
            {
                lock (lockerObject)
                {
                    if (inctance == null)
                    {
                        inctance = new VKeyboardOperationsService();
                    }

                    return inctance;
                }
            }
        }
        #endregion

        #region WinAPI
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern short VkKeyScan(char ch);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll")]
        private static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetKeyboardLayout(int idThread);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool GetKeyboardLayoutName([Out] StringBuilder pwszKLID);
        #endregion

        private const int KEY_DOWN_FLAG = 0x0001;
        private const int KEY_UP_FLAG = 0x0002;

        private const int KEY_CAPS = 0x14;
        private const int KEY_BACK_SPACE = 0x08;
        private const int KEY_SPACE = 0x20;
        private const int KEY_ARROW_LEFT = 0x25;
        private const int KEY_ARROW_RIGHT = 0x27;

        private const string RUS_LYT = "00000419";
        private const string ENG_LYT = "00000409";

        public KeyboardLayout CurrentKeyboardLayout => GetKeyboardLayoutType();

        //public void PressCAPS()
        //{
        //    bool CapsLock = (((ushort)GetKeyState(KEY_CAPS)) & 0xffff) != 0;

        //    PressKey(KEY_CAPS);
        //}

        public void PressArrowLeftKey()
        {
            PressKey(KEY_ARROW_LEFT);
        }

        public void PressArrowRightKey()
        {
            PressKey(KEY_ARROW_RIGHT);
        }

        public void PressSpaceKey()
        {
            PressKey(KEY_SPACE);
        }

        public void PressBackspaceKey()
        {
            PressKey(KEY_BACK_SPACE);
        }

        public void PressKey(char symbol)
        {
            byte bytesSymbol = (byte)VkKeyScan(symbol);

            SendKey(bytesSymbol, KEY_DOWN_FLAG);
            SendKey(bytesSymbol, KEY_UP_FLAG);
        }

        public void PressKey(byte bytesSymbol)
        {
            SendKey(bytesSymbol, KEY_DOWN_FLAG);
            SendKey(bytesSymbol, KEY_UP_FLAG);
        }

        public void DownKey(byte bytesSymbol)
        {
            SendKey(bytesSymbol, KEY_DOWN_FLAG);
        }

        public void UpKey(byte bytesSymbol)
        {
            SendKey(bytesSymbol, KEY_UP_FLAG);
        }

        public KeyboardLayout GetKeyboardLayoutType()
        {
            StringBuilder input = new StringBuilder();

            GetKeyboardLayoutName(input);

            if (input.ToString() == RUS_LYT)
            {
                return KeyboardLayout.RUS;
            }

            return KeyboardLayout.ENG;
        }

        public void SwitchLayout()
        {
            if (CurrentKeyboardLayout == KeyboardLayout.RUS)
            {
                LoadKeyboardLayout(ENG_LYT, 1);
            }
            else
            {
                LoadKeyboardLayout(RUS_LYT, 1);
            } 

            GetKeyboardLayoutType();
        }

        private void SendKey(byte bytesSymbol, int dwFlag)
        {
            keybd_event(bytesSymbol, 0, dwFlag, 0);
        }
    }
}
