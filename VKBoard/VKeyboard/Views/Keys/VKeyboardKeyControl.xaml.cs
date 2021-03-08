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

            timer = new Timer
            {
                Enabled = false,
                Interval = 100
            };

            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (pressTime >= holdKeyTime)
                {
                    AlternativeSymbolVisibility = Visibility.Visible;

                    timer.Enabled = false;

                    pressTime = 0;

                    return;
                }
                else
                {
                    AlternativeSymbolVisibility = Visibility.Collapsed;
                }

                pressTime += 100;
            });

        }

        private const int holdKeyTime = 300;

        private DateTime pressedDateTime;

        private readonly Timer timer;
        private int pressTime = 0;

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
            pressedDateTime = DateTime.Now;

            timer.Enabled = true;

            //alternativeSymbolPopUp.IsOpen = AlternativeSymbol != '\0';
            alternativeSymbolPopUp.IsOpen = true;
        }

        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            timer.Enabled = false;

            if (DateTime.Now.Subtract(pressedDateTime).TotalMilliseconds > holdKeyTime)
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
