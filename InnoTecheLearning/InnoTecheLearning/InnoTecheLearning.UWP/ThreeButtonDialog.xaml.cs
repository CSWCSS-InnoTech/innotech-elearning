﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace InnoTecheLearning.UWP
{
    using static InnoTecheLearning.Utils.ThreeButtonDialog;
    public partial class ThreeButtonDialog : ContentDialog
    {
        public ThreeButtonDialogResult Result { get; set; }
        Action Button1;
        Action Button2;
        Action Button3;

        public ThreeButtonDialog(string Title, string Message, string Button1, Action Button1Clicked,
                string Button2, Action Button2Clicked, string Button3, Action Button3Clicked)
        {
            InitializeComponent();
            Result = ThreeButtonDialogResult.Nothing;
            this.Title = Title;
            msg.Text = Message;
            btn1.Content = Button1;
            btn2.Content = Button2;
            btn3.Content = Button3;
            this.Button1 = Button1Clicked;
            this.Button2 = Button2Clicked;
            this.Button3 = Button3Clicked;
        }

        // Handle the button clicks from dialog
        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            Result = ThreeButtonDialogResult.Yes;
            Button1();
            // Close the dialog
            Hide();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Result = ThreeButtonDialogResult.No;
            Button2();
            // Close the dialog
            Hide();
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            Result = ThreeButtonDialogResult.Cancel;
            Button3();
            // Close the dialog
            Hide();
        }
    }

}