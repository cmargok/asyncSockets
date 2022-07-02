using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Cargo
    {
        public Cargo()
        {
            Empleados = new HashSet<Empleado>();
            Historicos = new HashSet<Historico>();
        }

        public int CargoId { get; set; }
        public string CargoNombre { get; set; } = null!;
        public decimal CargoSueldoMinimo { get; set; }
        public decimal CargoSueldoMaximo { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
        public virtual ICollection<Historico> Historicos { get; set; }
    }
}
