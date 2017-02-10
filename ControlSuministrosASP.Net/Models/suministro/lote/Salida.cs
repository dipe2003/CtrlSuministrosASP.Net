using ControlSuministrosASP.Net.Models.operario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.lote {
    [Table("Salidas")]
    public class Salida {
        public int SalidaId { get; set; }
        public DateTime FechaSalida { get; set; }
        public float CantidadSalida { get; set; }
        public string ObservacionesSalida { get; set; }

        public virtual Lote LoteSalida { get; set; }

        public virtual Operario OperarioSalidaSuministro { get; set; }

        //	Constructores
        public Salida() { }
        public Salida(DateTime FechaSalida, float CantidadSalida, string ObservacionesSalida) {
            this.FechaSalida = FechaSalida;
            this.CantidadSalida = CantidadSalida;
            this.ObservacionesSalida = ObservacionesSalida;
        }
    }
}