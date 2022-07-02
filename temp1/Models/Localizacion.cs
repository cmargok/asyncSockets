using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Localizacion
    {
        public int LocalizId { get; set; }
        public string LocalizNombre { get; set; } = null!;
        public int? CiudId { get; set; }

        public virtual Ciudad? Ciud { get; set; }
    }
}
