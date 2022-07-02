using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class LozalizacionDepartamento
    {
        public int LocalizId { get; set; }
        public int DptoId { get; set; }

        public virtual Departamento Dpto { get; set; } = null!;
        public virtual Localizacion Localiz { get; set; } = null!;
    }
}
