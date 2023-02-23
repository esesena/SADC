using SADC.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADC.Domain
{
    public class Notice
    {
        //[Key]
        public int Id { get; set; }

        //[Display(Name = "Nome")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        //[StringLength(50, ErrorMessage = "Máximo de {1} caracteres!")]
        public string Name { get; set; }

        //[Display(Name = "Descrição")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        //[StringLength(250, ErrorMessage = "Máximo de {1} caracteres!")]
        public string Description { get; set; }

        //[DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        //[Display(Name = "Prioridade")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        public Priorities Prioritie { get; set; }
    }
}
