namespace PruebaTecnica.Models
{
    public class ResumenPropiedad
    {

        public ResumenPropiedad()
        {
            Propiedades = new List<Propiedad>();
            TipoPropiedades = new List<TipoPropiedad>();
            Duenio = new List<Duenio>();
        }
        public List<Propiedad> Propiedades { get; set; }

        public List<TipoPropiedad> TipoPropiedades { get; set; }

        public List<Duenio> Duenio { get; set; }

        public int Id { get; set; }
        public int? PropertyTypeId { get; set; }
        public int? OwnerId { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public double Area { get; set; }
        public double? ConstructionArea { get; set; }
    }
}
