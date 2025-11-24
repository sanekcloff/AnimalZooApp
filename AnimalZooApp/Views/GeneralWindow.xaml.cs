using AnimalZooApp.Context;
using AnimalZooApp.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для GeneralWindow.xaml
    /// </summary>
    public partial class GeneralWindow : Window
    {
        private AppDbContext _ctx;
        private List<string> _filthValues;
        private List<string> _sortValues;
        private string _selectedFilth;
        private string _selectedSort;
        public GeneralWindow()
        {
            InitializeComponent();
            _ctx = new AppDbContext();
            FilthComboBox.ItemsSource = _filthValues = new List<string>()
            {
                "Без фильтрации",
                "Мужской",
                "Женский",
            };
            SortComboBox.ItemsSource = _sortValues = new List<string>()
            {
                "Без сортировки",
                "По весу животного(убыв.)",
                "По весу животного(возр.)",
            };
            FilthComboBox.SelectedValue = _selectedFilth = _filthValues[0];
            SortComboBox.SelectedValue = _selectedSort = _sortValues[0];
            UpdateList();

            var val1 = new String("1");
            var val2 = new String("1").GetHashCode();
            MessageBox.Show(val1.GetHashCode().ToString());
            MessageBox.Show(val2.GetHashCode().ToString());
        }
        private void UpdateList()
        {
            ValiersListView.ItemsSource = Sort(Search(Filth(_ctx.Animals.Include(v=>v.Valier).ToList())));
        }
        private ICollection<Animal> Search(ICollection<Animal> animals)
        {
            if(string.IsNullOrWhiteSpace(SearchTextBox.Text)) return animals;
            else return animals.Where(v=>v.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) || v.DescriptionText.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
        }
        private ICollection<Animal> Sort(ICollection<Animal> animals)
        {
            if (_sortValues[1] == _selectedSort) return animals.OrderByDescending(v => v.Weight).ToList();
            else if (_sortValues[2] == _selectedSort) return animals.OrderBy(v => v.Weight).ToList();
            else return animals;
        }
        private ICollection<Animal> Filth(ICollection<Animal> animals)
        {
            if (_selectedFilth == _filthValues[1]) return animals.Where(v => !v.IsWoman).ToList();
            else if (_selectedFilth == _filthValues[2]) return animals.Where(v => v.IsWoman).ToList();
            return animals;
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedSort = (string)SortComboBox.SelectedValue;
            UpdateList();
        }

        private void FilthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFilth = (string)FilthComboBox.SelectedValue;
            UpdateList();
        }

        private void AddAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            new AnimalManageWindow(null,_ctx).ShowDialog();
            UpdateList();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new AnimalManageWindow(ValiersListView.SelectedValue as Animal, _ctx).ShowDialog();
            UpdateList();
        }

        private void ListViewItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete || ValiersListView.SelectedValue == null) return;

            var input = MessageBox.Show("Вы точно хотите удалить эту запись?","Удаление",MessageBoxButton.YesNo,MessageBoxImage.Question);

            if (input == MessageBoxResult.Yes)
            {
                _ctx.Animals.Remove((Animal)ValiersListView.SelectedValue);
                _ctx.SaveChanges();
                UpdateList();
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            var tempWnd = Application.Current.MainWindow;
            Properties.Settings.Default.RememeberMe = false; 
            Properties.Settings.Default.Save();
            Application.Current.MainWindow = new LoginWindow();
            tempWnd.Close();
            Application.Current.MainWindow.Show();
        }
    }
}
