namespace SADC.Application.Dtos
{
    public class FarmDto
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageURL { get; set; }
        public double Size { get; set; }
        public ICollection<FieldDto> Fields { get; set; }
    }
}
