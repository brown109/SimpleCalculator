﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleCalculator
{
    //
    // Code behind for Help WIndow
    // The Help window is almost all text with just an exit button
    //
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
           InitializeComponent();
        }
        private void Button_ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
