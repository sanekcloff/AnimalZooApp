using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalZooApp.Models
{
    [Flags]
    public enum Diet
    {
        Carnivorous=1, 
        Herbivorous=2 
    }
    public class Animal
    {
        public Animal()
        {
            Name = string.Empty;
            DateOfBirth = DateTime.Now;
            IsWoman = false;
            Diet = Diet.Herbivorous;
            Weight = 0;
        }
        public Animal(string name, string? description, DateTime dateOfBirth, bool isWoman, Diet diet, decimal weight)
        {
            Name = name;
            Description = description;
            DateOfBirth = dateOfBirth;
            IsWoman = isWoman;
            Diet = diet;
            Weight = weight;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsWoman { get; set; }
        public Diet Diet { get; set; }
        public decimal Weight { get; set; }

        public int ValierId { get; set; }
        public Valier Valier { get; set; } = new Valier();

        public string Gender => IsWoman ? "Женский" : "Мужской";
        public string DietText => Diet switch
        {
            Diet.Carnivorous => "Плотоядный",
            Diet.Herbivorous => "Травоядный",
            Diet.Carnivorous | Diet.Herbivorous => "Всеядный",
            _ => "Не определённый"
        };
        public string DescriptionText => string.IsNullOrWhiteSpace(Description) ? "Без описания" : Description;
        public ushort Age
        {
            get
            {
                // вычисляем года исходя из года текущего и года рождения
                var yearDiff = DateTime.Now.Year - DateOfBirth.Year;
                // от даты рождения отнимаем текущую дату с вычитанием годов разницы
                if (DateOfBirth > DateTime.Now.AddYears(-yearDiff)) yearDiff--;
                return (ushort)yearDiff;
            }
        }

        public override string ToString() => $"[{Id}] {Name} - {DescriptionText}: Дата рождения {DateOfBirth:d} - Возраст {Age}; Гендер {Gender}; Вес {Weight} кг; Рацион питания {DietText}";
    }
}
