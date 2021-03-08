using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            holdKeyTimer = new Timer
            {
                Enabled = false,
                Interval = 100
            };

            holdKeyTimer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (holdedKeyTime >= holdKeyTime && AlternativeSymbol != '\0')
                {
                    AlternativeSymbolVisibility = Visibility.Visible;

                    holdKeyTimer.Enabled = false;

                    holdedKeyTime = 0;

                    return;
                }
                else
                {
                    AlternativeSymbolVisibility = Visibility.Collapsed;
                }

                holdedKeyTime += 100;
            });

        }

        private const int holdKeyTime = 300;

        private DateTime holdKeyDateTime;

        private readonly Timer holdKeyTimer;

        private int holdedKeyTime = 0;

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
            DependencyProperty.Register("AlternativeSymbol", typeof(char), typeof(VKeyboardKeyControl), new PropertyMetadata('\0'));

        public Visibility AlternativeSymbolVisibility
        {
            get => (Visibility)GetValue(AlternativeSymbolVisibilityProperty);
            set => SetValue(AlternativeSymbolVisibilityProperty, value);
        }

        // Using a DependencyProperty as the backing store for AlternativeSymbolVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlternativeSymbolVisibilityProperty =
            DependencyProperty.Register("AlternativeSymbolVisibility", typeof(Visibility), typeof(VKeyboardKeyControl), new PropertyMetadata(Visibility.Collapsed));

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            holdKeyDateTime = DateTime.Now;

            holdKeyTimer.Enabled = true;

            alternativeSymbolPopUp.IsOpen = true;
        }

        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            holdKeyTimer.Enabled = false;

            if (DateTime.Now.Subtract(holdKeyDateTime).TotalMilliseconds >= holdKeyTime)
            {
                VKeyboardOperationsService.Instance.PressKey(AlternativeSymbol);
            }
            else
            {
                VKeyboardOperationsService.Instance.PressKey(Symbol);
            }

            AlternativeSymbolVisibility = Visibility.Collapsed;

            alternativeSymbolPopUp.IsOpen = false;
        }
    }
}
