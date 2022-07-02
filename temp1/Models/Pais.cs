using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Pais
    {
        public Pais()
        {
            Ciudades = new HashSet<Ciudad>();
        }

        public int pais_ID { get; set; }
        public string pais_nombre { get; set; } = null!;

        public virtual ICollection<Ciudad> Ciudades { get; set; }
    }
}
