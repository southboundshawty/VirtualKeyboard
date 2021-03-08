using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using VKBoard.VKeyboard.Commands;
using VKBoard.VKeyboard.Services;

namespace VKBoard.VKeyboard.Views.Keys
{
    /// <summary>
    /// Логика взаимодействия для VKeyboardKeyControl.xaml
    /// </summary>
    public partial class VKeyboardKeyControl : UserControl
    {
        public VKeyboardKeyControl()
        {
            InitializeComponent();
        }

        private DateTime pressedDateTime;

        public char Symbol
        {
            get => (char)GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }

        // Using a DependencyProperty as the backing store for Symbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(char), typeof(VKeyboardKeyControl), new PropertyMetadata(default));

        public char AlternativeSymbol
        {
            get => (char)GetValue(AlternativeSymbolProperty);
            set => SetValue(AlternativeSymbolProperty, value);
        }

        // Using a DependencyProperty as the backing store for AlternativeSymbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlternativeSymbolProperty =
            DependencyProperty.Register("AlternativeSymbol", typeof(char), typeof(VKeyboardKeyControl), new PropertyMetadata(default));

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            pressedDateTime = DateTime.Now;

            alternativeSymbolPopUp.IsOpen = AlternativeSymbol != '\0';
        }

        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DateTime.Now.Subtract(pressedDateTime).TotalMilliseconds > 300)
            {

                VKeyboardOperationsService.Instance.PressKey(AlternativeSymbol);
            }
            else
            {
                VKeyboardOperationsService.Instance.PressKey(Symbol);
            }

            alternativeSymbolPopUp.IsOpen = false;
        }
    }
}
