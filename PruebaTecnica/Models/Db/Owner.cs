using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models.Db
{
    public partial class Owner
    {
        public Owner()
        {
            Properties = new HashSet<Property>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string? Email { get; set; }
        public string IdentificationNumber { get; set; } = null!;
        public string? Address { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
