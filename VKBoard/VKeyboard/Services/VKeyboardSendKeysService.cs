using System.Runtime.InteropServices;

namespace VKBoard.VKeyboard.Services
{
    public class VKeyboardSendKeysService
    {
        private VKeyboardSendKeysService() { }

        private static readonly object lockerObject = new object();

        public static VKeyboardSendKeysService inctance;

        public static VKeyboardSendKeysService Instance
        {
            get
            {
                lock (lockerObject)
                {
                    if (inctance == null)
                    {
                        inctance = new VKeyboardSendKeysService();
                    }

                    return inctance;
                }
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern short VkKeyScan(char ch);

        private const int KEY_DOWN = 0x0001;
        private const int KEY_UP = 0x0002;

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

        public void DownKey(char symbol)
        {
            byte bytesSymbol = (byte)VkKeyScan(symbol);

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
