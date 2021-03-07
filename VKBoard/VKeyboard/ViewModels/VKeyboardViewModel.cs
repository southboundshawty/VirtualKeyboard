using System.ComponentModel;
using System.Runtime.CompilerServices;

using VKBoard.VKeyboard.Models;
using VKBoard.VKeyboard.Services;
using VKBoard.VKeyboard.Views.Layouts;

namespace VKBoard.VKeyboard.ViewModels
{
    public class VKeyboardViewModel : INotifyPropertyChanged
    {
        public VKeyboardViewModel()
        {
            keyboardOperationsService = VKeyboardOperationsService.Instance;

            InitializeLayouts();
        }

        private readonly VKeyboardOperationsService keyboardOperationsService;

        private VKeyboardLayoutPage[] layouts;

        private readonly EnglishLayout englishLayout = new EnglishLayout();
        private readonly RussianLayout russianLayout = new RussianLayout();

        private VKeyboardLayoutPage selectedLayout;

        public VKeyboardLayoutPage SelectedLayout
        {
            get => selectedLayout;
            set
            {
                selectedLayout = value;
                OnPropertyChanged();
            }
        }

        private void SwitchLayout()
        {
            keyboardOperationsService.SwitchLayout();

            SetKeyboardLayout();
        }

        private void InitializeLayouts()
        {
            layouts = new VKeyboardLayoutPage[] { englishLayout, russianLayout };

            SetKeyboardLayout();

            foreach (VKeyboardLayoutPage item in layouts)
            {
                item.OnSwitchLayoutRequest += OnSwitchLayoutRequest;
            }
        }

        private void SetKeyboardLayout()
        {
            if (keyboardOperationsService.CurrentKeyboardLayout == KeyboardLayout.RUS)
            {
                SelectedLayout = russianLayout;
            }
            else
            {
                SelectedLayout = englishLayout;
            }
        }

        private void OnSwitchLayoutRequest(object sender)
        {
            SwitchLayout();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
