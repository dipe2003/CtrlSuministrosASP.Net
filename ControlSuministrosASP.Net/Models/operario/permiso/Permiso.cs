using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.operario.permiso {
    [Table("Permisos")]
    public class Permiso {
        public int PermisoId { get; set; }
        public string NombrePermiso { get; set; }

        public virtual IList<Operario> OperariosPermiso { get; set; }

        //  constructores
        public Permiso() {
            this.OperariosPermiso = new List<Operario>();
        }
        public Permiso(string NombrePermiso) {
            this.NombrePermiso = NombrePermiso;
            this.OperariosPermiso = new List<Operario>();
        }

        // Lista de Operarios
        public void addOperarioPermiso(Operario OperarioPermiso) {
            this.OperariosPermiso.Add(OperarioPermiso);
            if(OperarioPermiso==null || !OperarioPermiso.Equals(this)) {
                OperarioPermiso.PermisoOperario = this;
            }
        }
        public void removeOperarioPermiso(Operario OperarioPermiso) { this.OperariosPermiso.Remove(OperarioPermiso); }
    }
}