using SADC.Domain.Enums;

namespace SADC.Domain
{
    public class Seed
    {
        //[Key]
        public int Id { get; set; }
  
        //[Display(Name = "Cultura")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        public Cultivation Cultivation { get; set; }

        //[Display(Name = "Descrição")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres!")]
        public string Description { get; set; }

        //[Display(Name = "Hábito de Crescimento")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres!")]
        public string GrowthHabit { get; set; }

        //[Display(Name = "Altura da Planta")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        public decimal Height { get; set; }

        //[Display(Name = "Floração")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        public decimal Flowering { get; set; }

        //[Display(Name = "Resistência ao Acamamento")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        public string Resistence { get; set; }

        //[Display(Name = " ")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        public decimal MaturationGroup { get; set; }

        //[Display(Name = "Consumo Médio de Sementes")]
        //[Required(ErrorMessage = "{0} é obrigatório!")]
        public string SeedConsumption { get; set; }
    }
}