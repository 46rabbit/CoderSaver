using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string initialRoute = "";
        List<string> fileList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            initialRoute = GetApplicationRoot();
            initialRoute = System.IO.Path.Combine(initialRoute, "cosaSave");

            DirectoryInfo fi = new DirectoryInfo(initialRoute);
            if (!fi.Exists)
            {
                fi.Create();
            }

            refreashList();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();

            if (window1.DialogResult.HasValue && window1.DialogResult.Value)
            {
                refreashList();
            }
        }

        private void refreashList()
        {
            listBox.Items.Clear();
            fileList.Clear();

            string[] fileArray = Directory.GetFiles(initialRoute);

            foreach(var item in fileArray)
            {
                fileList.Add(item);
                listBox.Items.Add(Path.GetFileName(item));
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

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            fileList.RemoveAt(listBox.SelectedIndex-1);

            FileInfo fileInfo = new FileInfo(fileList[listBox.SelectedIndex-1]);
            fileInfo.Delete();

            refreashList();
        }
    }
}
