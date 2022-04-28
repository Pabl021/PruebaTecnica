using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models.Db
{
    public partial class PropertyType
    {
        public PropertyType()
        {
            Properties = new HashSet<Property>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Property> Properties { get; set; }
    }
}
