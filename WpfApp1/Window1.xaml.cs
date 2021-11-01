using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        string initialRoute = "";

        public Window1()
        {
            InitializeComponent();
            initialRoute = GetApplicationRoot();

            initialRoute = System.IO.Path.Combine(initialRoute, "cosaSave");
        }

        private void Okbtn_Click(object sender, RoutedEventArgs e)
        {
            saveFiletoPC();
            this.DialogResult = true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void saveFiletoPC()
        {
            string currentFileRoute = System.IO.Path.Combine(initialRoute, fileNameTxtBox.Text + ".cosa");
            FileInfo fi = new FileInfo(currentFileRoute);

            if(fi.Exists)
            {
                if(MessageBox.Show("The file you want to create already exists.\nDo you want to overwirte it?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
                {
                    fi.Delete();
                }
                else
                {
                    return;
                }
            }

            using (StreamWriter file = new StreamWriter(currentFileRoute))
            {
                file.Write(contentTxtbox.Text);
            }
        }

        public string GetApplicationRoot()
        {
            var exePath = System.IO.Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }
    }
}
