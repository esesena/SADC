namespace SADC.Domain
{
    public class Farm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageURL { get; set; }
        public double Size { get; set; }
        public ICollection<EmployeesFarms> Employees { get; set; }
        public ICollection<Field> Fields { get; set; }
    }
}