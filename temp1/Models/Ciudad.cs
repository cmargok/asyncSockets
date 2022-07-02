using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Ciudad
    {
        public Ciudad()
        {
           Localizaciones = new HashSet<Localizacion>();
        }

        public int CiudId { get; set; }
        public string CiudNombre { get; set; } = null!;
        public int? PaisId { get; set; }

        public virtual Pais? Pais { get; set; }
       public virtual ICollection<Localizacion> Localizaciones { get; set; }

    }
}
