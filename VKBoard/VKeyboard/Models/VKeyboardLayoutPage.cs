using System.Windows.Controls;
using System.Windows.Input;

using VKBoard.VKeyboard.Commands;

namespace VKBoard.VKeyboard.Models
{
    public class VKeyboardLayoutPage : Page
    {
        public delegate void SwitchLayoutRequestHandler(object sender);

        public event SwitchLayoutRequestHandler OnSwitchLayoutRequest;

        private ICommand switchLayoutCommand;
        public ICommand SwitchLayoutCommand => switchLayoutCommand ??= 
            new VkeyboardCommand(obj => OnSwitchLayoutRequest?.Invoke(this));
    }
}
