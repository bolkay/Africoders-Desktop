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

namespace AfricodersProject
{
    /// <summary>
    /// Interaction logic for CategoryDetailsWindow.xaml
    /// </summary>
    public partial class CategoryDetailsWindow : Window
    {
        public CategoryDetailsWindow()
        {
            InitializeComponent();
        }
        
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void topPanel_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

            DragMove();
        }
    }
}
