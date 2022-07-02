using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Departamento
    {
        public Departamento()
        {
            Empleados = new HashSet<Empleado>();
            Historicos = new HashSet<Historico>();
        }

        public int DptoId { get; set; }
        public string DptoNombre { get; set; } = null!;

        public virtual ICollection<Empleado> Empleados { get; set; }
        public virtual ICollection<Historico> Historicos { get; set; }
    }
}
