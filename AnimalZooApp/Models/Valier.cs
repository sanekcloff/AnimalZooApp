using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalZooApp.Models
{
    public class Valier
    {
        public Valier()
        {
            Title = string.Empty;
            Description = null;
            Width = 0;
            Height = 0;
            Length = 0;
        }
        public Valier(string title, string? description, decimal width, decimal height, decimal length)
        {
            Title = title;
            Description = description;
            Width = width;
            Height = height;
            Length = length;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public decimal Area => 2 * (Length * Width + Width * Height + Height * Length);
        public string DescriptionText => string.IsNullOrWhiteSpace(Description) ? "Без описания" : Description;
        public bool IsEmpty => !Animals.Any();
        public override string ToString()
        {
            return $"[{Id}] {Title} {DescriptionText} - Площадь: {Area} м³\n\t-{string.Join("\n\t-",Animals)}";
        }

        public ICollection<Animal> Animals { get; set; } = new List<Animal>();

    }
}
