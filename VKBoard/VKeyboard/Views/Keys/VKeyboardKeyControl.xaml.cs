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

        private ICommand pressKeyCommand;
        public ICommand PressKeyCommand => pressKeyCommand ??=
            new VkeyboardCommand(obj =>
            {
                PressKey();
            });

        private void PressKey()
        {
            VKeyboardOperationsService.Instance.PressKey(Symbol);
        }
    }
}
