using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using VKBoard.VKeyboard.Services;

namespace VKBoard.VKeyboard.Views.Keys
{
    /// <summary>
    /// Логика взаимодействия для VKeyboardLanguageKey.xaml
    /// </summary>
    public partial class VKeyboardLanguageKey : UserControl
    {
        public VKeyboardLanguageKey()
        {
            InitializeComponent();

            SetKeyboardLayoutName();
        }

        public string LayoutName
        {
            get => (string)GetValue(LayoutNameProperty);
            set => SetValue(LayoutNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for LayoutName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayoutNameProperty =
            DependencyProperty.Register("LayoutName", typeof(string), typeof(VKeyboardLanguageKey), new PropertyMetadata(default));

        public ICommand SwitchLanguageCommand
        {
            get => (ICommand)GetValue(SwitchLanguageCommandProperty);
            set => SetValue(SwitchLanguageCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for SwitchLanguageCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SwitchLanguageCommandProperty =
            DependencyProperty.Register("SwitchLanguageCommand", typeof(ICommand), typeof(VKeyboardLanguageKey), new PropertyMetadata(default));

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetKeyboardLayoutName();
            SwitchLanguageCommand?.Execute(sender); 
        }

        private void SetKeyboardLayoutName()
        {
            KeyboardLayout layout = VKeyboardOperationsService.Instance.CurrentKeyboardLayout;

            if (layout == KeyboardLayout.ENG)
            {
                LayoutName = "ENG";
            }
            else
            {
                LayoutName = "RUS";
            }
        }
    }
}
