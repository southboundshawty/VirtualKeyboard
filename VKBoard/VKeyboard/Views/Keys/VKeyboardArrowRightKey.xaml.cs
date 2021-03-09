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

using VKBoard.VKeyboard.Services;

namespace VKBoard.VKeyboard.Views.Keys
{
    /// <summary>
    /// Логика взаимодействия для VKeyboardArrowRightKey.xaml
    /// </summary>
    public partial class VKeyboardArrowRightKey : UserControl
    {
        public VKeyboardArrowRightKey()
        {
            InitializeComponent();
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            VKeyboardOperationsService.Instance.PressArrowRightKey();
        }
    }
}
