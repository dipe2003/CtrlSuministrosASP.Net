using ControlSuministrosASP.Net.Models.operario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.lote {
    [Table("Ingresos")]
    public class Ingreso {
        public int IngresoId { get; set; }
        public Nullable<DateTime> FechaIngreso { get; set; }
        public float CantidadIngreso { get; set; }
        public string NumeroFactura { get; set; }
        public string ObservacionesIngreso { get; set; }

        public virtual Lote LoteIngreso { get; set; }

        public virtual Operario OperarioIngresoSuministro { get; set; }

        //	Constructores
        public Ingreso() { }
        public Ingreso(DateTime FechaIngreso, float CantidadIngreso, string NumeroFactura, string Observaciones) {
            this.FechaIngreso = FechaIngreso;
            this.CantidadIngreso = CantidadIngreso;
            this.NumeroFactura = NumeroFactura;
            this.ObservacionesIngreso = Observaciones;
        }

    }
}
