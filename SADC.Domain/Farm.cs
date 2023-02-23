using SADC.Domain.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace SADC.Domain
{
    public class Farm
    {
        //[Key]
        public int Id { get; set; }

        //[Display(Name = "Nome")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres!")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        public string Name { get; set; }

        //[Display(Name = "Localização")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres!")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        public string Location { get; set; }
        public string ImageURL { get; set; }

        //[Display(Name = "Área")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        public double Size { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<EmployeesFarms> EmployeesFarms { get; set; }


        public IEnumerable<Plot> Plots { get; set; }
    }
}