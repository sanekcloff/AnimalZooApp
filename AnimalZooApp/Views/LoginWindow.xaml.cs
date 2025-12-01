using System;
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

namespace AnimalZooApp.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private Dictionary<string, string> _tests;
        private KeyValuePair<string, string> _selectedTest;
        public LoginWindow()
        {
            InitializeComponent();
            _tests = new Dictionary<string, string>()
            {
                {"2 + 2 = ?","4"},
                {"2 + 6 = ?","8"},
                {"Как переводится Pizza","пицца"},
            };
            RefreshTest();
            if (Properties.Settings.Default.RememeberMe)
            {
                RememberMeCheckBox.IsChecked = true;
                OpenGeneralWindow();
            }
        }
        private void RefreshTest()
        {
            TestTextBlock.Text = (_selectedTest = _tests.ElementAt(new Random().Next(_tests.Count))).Key;
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            if (TestTextBox.Text.ToLower() == _selectedTest.Value.ToLower())
            {
                MessageBox.Show("Верно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Properties.Settings.Default.Save();
                OpenGeneralWindow();
            }
            else
            {
                MessageBox.Show("Не верно!","Уведомление",MessageBoxButton.OK,MessageBoxImage.Warning);
                RefreshTest();
            }
        }

        private void OpenGeneralWindow()
        {
            var tempWnd = Application.Current.MainWindow;
            Application.Current.MainWindow = new GeneralWindow();
            tempWnd.Close();
            Application.Current.MainWindow.Show();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) TestButton_Click(sender, e);
        }

        private void RememberMeCheckBox_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.RememeberMe = RememberMeCheckBox.IsChecked!.Value;
        }
    }
}
