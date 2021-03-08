using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using VKBoard.VKeyboard.Commands;
using VKBoard.VKeyboard.Services;

namespace VKBoard.VKeyboard.Views.Keys
{
    /// <summary>
    /// Логика взаимодействия для VKeyboardSpacialKeyControl.xaml
    /// </summary>
    public partial class VKeyboardSpacialKeyControl : UserControl
    {
        public VKeyboardSpacialKeyControl()
        {
            InitializeComponent();
        }

        // private DateTime pressedDateTime;

        public object KeyContent
        {
            get { return (object)GetValue(KeyContentProperty); }
            set { SetValue(KeyContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyContentProperty =
            DependencyProperty.Register("KeyContent", typeof(object), typeof(VKeyboardKeyControl), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(VKeyboardKeyControl), new PropertyMetadata(null));

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Command?.Execute(this);
            //pressedDateTime = DateTime.Now;
        }

        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (DateTime.Now.Subtract(pressedDateTime).TotalMilliseconds > 300)
            //{

            //}
            //else
            //{
            //    VKeyboardOperationsService.Instance.PressKey(Symbol);
            //}
        }
    }
}
