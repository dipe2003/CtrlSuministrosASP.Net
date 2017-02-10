using ControlSuministrosASP.Net.Models.operario.permiso;
using ControlSuministrosASP.Net.Models.suministro.lote;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.operario {
    [Table("Operarios")]
    public class Operario {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OperarioId { get; set; }
        public string NombreOperario { get; set; }
        public string ApellidoOperario { get; set; }
        public string CorreoOperario { get; set; }
        public string PasswordOperario { get; set; }
        public bool RecibeAlertas { get; set; }

        public virtual IList<Ingreso> IngresosSuministrosOperario { get; set; }
        public virtual IList<Salida> SalidasSuministrosOperario { get; set; }

        private Permiso permisoOperario;
        public virtual Permiso PermisoOperario
        {
            get { return permisoOperario; }
            set
            {
                this.permisoOperario = value;
                if (!value.OperariosPermiso.Contains(this)){
                    value.OperariosPermiso.Add(this);
                };
            }
        }

        //	Constructores
        public Operario() { }
        public Operario(int IdOperario, string NombreOperario, string ApellidoOperario, string CorreoOperario, string PasswordOperario) {
            this.OperarioId = IdOperario;
            this.NombreOperario = NombreOperario;
            this.ApellidoOperario = ApellidoOperario;
            this.CorreoOperario = CorreoOperario;
            this.PasswordOperario = PasswordOperario;
            this.IngresosSuministrosOperario = new List<Ingreso>();
            this.SalidasSuministrosOperario = new List<Salida>();
            this.RecibeAlertas = false;
        }

        //	Ingresos - Egresos
        public void addIngresoInsumoOperario(Ingreso IngresoSuministroOperario) {
            this.IngresosSuministrosOperario.Add(IngresoSuministroOperario);
            if(IngresoSuministroOperario.OperarioIngresoSuministro==null || !IngresoSuministroOperario.Equals(this)){
                IngresoSuministroOperario.OperarioIngresoSuministro = this;
            }
        }
        public void removeIngresoSuministroOperario(Ingreso IngresoSuministroOperario) {
            this.IngresosSuministrosOperario.Remove(IngresoSuministroOperario);
            if(IngresoSuministroOperario.OperarioIngresoSuministro != null && IngresoSuministroOperario.Equals(this)){
                IngresoSuministroOperario.OperarioIngresoSuministro = null;
            }
        }
        public void addSalidaSuministroOperario(Salida SalidaSuministroOperario) {
            this.SalidasSuministrosOperario.Add(SalidaSuministroOperario);
            if(SalidaSuministroOperario.OperarioSalidaSuministro == null || !SalidaSuministroOperario.OperarioSalidaSuministro.Equals(this)) {
                SalidaSuministroOperario.OperarioSalidaSuministro = this;
            }
        }
        public void removeSalidaSuministroOperario(Salida SalidaSuministroOperario) {
            this.SalidasSuministrosOperario.Remove(SalidaSuministroOperario);
            if(SalidaSuministroOperario.OperarioSalidaSuministro!=null && SalidaSuministroOperario.OperarioSalidaSuministro.Equals(this)) {
                SalidaSuministroOperario.OperarioSalidaSuministro = null;
            }
        }
    }
}