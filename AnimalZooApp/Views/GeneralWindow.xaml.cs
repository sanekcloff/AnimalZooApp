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
    }
}
