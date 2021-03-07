using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using VKBoard.VKeyboard.Models;
using VKBoard.VKeyboard.Views.Layouts;

namespace VKBoard.VKeyboard.ViewModels
{
    public class VKeyboardViewModel : INotifyPropertyChanged
    {
        public VKeyboardViewModel()
        {
            InitializeLayouts();
        }

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
            int index = layouts.ToList().IndexOf(SelectedLayout);

            if (index + 1 < layouts.Length)
            {
                SelectedLayout = layouts[index + 1];
            }
            else
            {
                SelectedLayout = layouts.FirstOrDefault();
            }
        }

        private void InitializeLayouts()
        {
            layouts = new VKeyboardLayoutPage[] { englishLayout, russianLayout };

            SelectedLayout = layouts.FirstOrDefault();

            foreach (VKeyboardLayoutPage item in layouts)
            {
                item.OnSwitchLayoutRequest += OnSwitchLayoutRequest;
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
