using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.notificaciones {
    [Table("Propiedades")]
    public class Propiedad {
        [Index][Key][MaxLength(10)]
        public String Nombre { get; set; }
        public String Valor { get; set; }

        public Propiedad() {

        }
    }
}