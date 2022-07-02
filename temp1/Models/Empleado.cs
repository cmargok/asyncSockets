using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Empleado
    {
        public Empleado()
        {
            Historicos = new HashSet<Historico>();
            InverseEmplGerente = new HashSet<Empleado>();
        }

        public int EmplId { get; set; }
        public string EmplPrimerNombre { get; set; } = null!;
        public string? EmplSegundoNombre { get; set; }
        public string? EmplEmail { get; set; }
        public decimal EmplSueldo { get; set; }
        public double? EmplComision { get; set; }
        public int EmplCargoId { get; set; }
        public int EmplGerenteId { get; set; }
        public int EmplDptoId { get; set; }

        public virtual Cargo EmplCargo { get; set; } = null!;
        public virtual Departamento EmplDpto { get; set; } = null!;
        public virtual Empleado EmplGerente { get; set; } = null!;
        public virtual ICollection<Historico> Historicos { get; set; }
        public virtual ICollection<Empleado> InverseEmplGerente { get; set; }
    }
}
