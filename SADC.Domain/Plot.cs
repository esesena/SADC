using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADC.Domain
{
    public class Plot
    {

        //[Key]
        public int Id { get; set; }

        //[Display(Name = "Nome")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres!")]
        //[Required(ErrorMessage = "Nome é obrigatório!")]
        public string Name { get; set; }

        //[Display(Name = "Localização")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres!")]
        //[Required(ErrorMessage = "Localização é obrigatório!")]
        public string Location { get; set; }

        //[Display(Name = "Área")]
        //[Required(ErrorMessage = "Área é obrigatório!")]
        public double Size { get; set; }

        //[Display(Name = "Tipo de Solo")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres!")]
        //[Required(ErrorMessage = "Tipo de solo é obrigatório!")]
        public string SoilType { get; set; }

        //[Display(Name = "Fazenda")]
        //[Required(ErrorMessage = "Fazenda é obrigatório!")]
        public int FarmId { get; set; }
        public Farm Farm { get; set; }

        public IEnumerable<PlantingPlot> PlantingPlot { get; set; }
    }
}