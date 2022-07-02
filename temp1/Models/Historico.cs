using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Historico
    {
        public int EmphistId { get; set; }
        public DateTime EmphistFechaRetiro { get; set; }
        public int EmphistDptoId { get; set; }
        public int EmphistCargoId { get; set; }
        public int EmphistEmplId { get; set; }

        public virtual Cargo EmphistCargo { get; set; } = null!;
        public virtual Departamento EmphistDpto { get; set; } = null!;
        public virtual Empleado EmphistEmpl { get; set; } = null!;
    }
}
