using System.Runtime.InteropServices;

namespace VKBoard.VKeyboard.Services
{
    public class VKeyboardOperationsService
    {
        private VKeyboardOperationsService() { }

        private static readonly object lockerObject = new object();

        public static VKeyboardOperationsService inctance;

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

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern short VkKeyScan(char ch);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        private const int KEY_DOWN = 0x0001;
        private const int KEY_UP = 0x0002;
        private const int KEY_CAPS = 0x14;

        public void PressCAPS()
        {
            bool CapsLock = (((ushort)GetKeyState(KEY_CAPS)) & 0xffff) != 0;

            PressKey(KEY_CAPS);
        }

        public void PressKey(char symbol)
        {
            byte bytesSymbol = (byte)VkKeyScan(symbol);

            SendKey(bytesSymbol, KEY_DOWN);
            SendKey(bytesSymbol, KEY_UP);
        }

        public void PressKey(byte bytesSymbol)
        {
            SendKey(bytesSymbol, KEY_DOWN);
            SendKey(bytesSymbol, KEY_UP);
        }

        public void DownKey(byte bytesSymbol)
        {
            SendKey(bytesSymbol, KEY_DOWN);
        }

        public void UpKey(byte bytesSymbol)
        {
            SendKey(bytesSymbol, KEY_UP);
        }

        private void SendKey(byte bytesSymbol, int dwFlag)
        {
            keybd_event(bytesSymbol, 0, dwFlag, 0);
        }
    }
}
