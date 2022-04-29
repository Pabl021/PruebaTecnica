namespace PruebaTecnica.Models
{
    public class Propiedad
    {
        public int Id { get; set; }
        public int? PropertyTypeId { get; set; }
        public int? OwnerId { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public double Area { get; set; }
        public double? ConstructionArea { get; set; }
    }
}
