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
            _filthValues = new List<string>()
            {
                "Без фильтрации",
                "Без животных",
                "С животными",
            };
            _sortValues = new List<string>()
            {
                "Без сортировки",
                "По количеству животных(убыв.)",
                "По количеству животных(возр.)",
            };
            UpdateList();
        }
        private void UpdateList()
        {
            ValiersListView.ItemsSource = Search(_ctx.Valiers.Include(v=>v.Animals).ToList());
        }
        private ICollection<Valier> Search(ICollection<Valier> valiers)
        {
            if(string.IsNullOrWhiteSpace(SearchTextBox.Text)) return valiers;
            else return valiers.Where(v=>v.Title.ToLower().Contains(SearchTextBox.Text.ToLower()) || v.DescriptionText.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
        }
        private ICollection<Valier> Sort(ICollection<Valier> valiers)
        {
            if (_sortValues[1] == _selectedSort) return valiers.OrderByDescending(v => v.Animals.Count).ToList();
            else if (_sortValues[2] == _selectedSort) return valiers.OrderBy(v => v.Animals.Count).ToList();
            else return valiers;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

    }
}
