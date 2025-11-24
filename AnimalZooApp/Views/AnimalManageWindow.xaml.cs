using AnimalZooApp.Context;
using AnimalZooApp.Models;
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
    /// Логика взаимодействия для AnimalManageWindow.xaml
    /// </summary>
    public partial class AnimalManageWindow : Window
    {
        private readonly Animal _animal;
        private readonly AppDbContext _ctx;
        private bool _isAdd;
        public AnimalManageWindow(Animal? animal, AppDbContext ctx)
        {
            InitializeComponent();
            _ctx = ctx;
            ValierComboBox.ItemsSource = _ctx.Valiers.ToList();
            DietComboBox.ItemsSource = new List<string>()
            {
                "Плотоядный","Травоядный","Всеядный"
            };
            if (animal == null)
            {
                _animal = new Animal();
                _isAdd = true;
                DateOfBirthDatePicker.SelectedDate = _animal.DateOfBirth;
                DietComboBox.SelectedValue = _animal.Diet switch
                {
                    Diet.Carnivorous => DietComboBox.Items[0],
                    Diet.Herbivorous => DietComboBox.Items[1],
                    _ => DietComboBox.Items[2],
                };
                ValierComboBox.SelectedIndex = 0;
                ActionButton.Content = "Добавить";
                Title = "Окно добавления животного";
            }
            else
            {
                _animal = animal;
                _isAdd = false;
                NameTextBox.Text = _animal.Name;
                DescriptionTextBox.Text = _animal.Description;
                DateOfBirthDatePicker.SelectedDate = _animal.DateOfBirth;
                IsWomanCheckBox.IsChecked = _animal.IsWoman;
                DietComboBox.SelectedValue = _animal.Diet switch
                {
                    Diet.Carnivorous => DietComboBox.Items[0],
                    Diet.Herbivorous => DietComboBox.Items[1],
                    _ => DietComboBox.Items[2],
                };
                WeightTextBox.Text = _animal.Weight.ToString();
                ValierComboBox.SelectedValue = _animal.Valier;
                ActionButton.Content = "Обновить";
                Title = "Окно обновления животного";
            }
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {

            _animal.Name = NameTextBox.Text;
            _animal.Description = DescriptionTextBox.Text;
            _animal.DateOfBirth = DateOfBirthDatePicker.SelectedDate!.Value;
            _animal.IsWoman = IsWomanCheckBox.IsChecked!.Value;
            _animal.Diet = (string)DietComboBox.SelectedItem switch
            {
                "Плотоядный" => Diet.Carnivorous,
                "Травоядный" => Diet.Herbivorous,
                _ => Diet.Carnivorous | Diet.Herbivorous,
            };
            _animal.Weight = Convert.ToDecimal(WeightTextBox.Text);
            _animal.Valier = (Valier)ValierComboBox.SelectedValue;

            if (_isAdd) _ctx.Animals.Add(_animal);

            _ctx.SaveChanges();
            Close();
        }
    }
}
