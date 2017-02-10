using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.lote {
    [Table("Lotes")]
    public class Lote {
        public int LoteId { get; set; }
        public Nullable<DateTime> VencimientoLote { get; set; }
        public string NumeroLote { get; set; }
        public bool Perecedero { get; set; }

        private Suministro suministroLote;
        public virtual Suministro SuministroLote {
            get{
                return this.suministroLote;
            }
            set {
                this.suministroLote = value;
                if (value.LotesSuministros == null) value.LotesSuministros = new List<Lote>();
                if(!value.LotesSuministros.Contains(this)){
                    value.AddLote(this);
                }
            }
        }

        public virtual IList<Ingreso> IngresosLote { get; set; }
        public virtual IList<Salida> SalidasLote { get; set; }

        //	Constructores
        public Lote() { }
        public Lote(DateTime VencimientoLote, string NumeroLote, bool Perecedero) {
            if (!Perecedero) {
                this.VencimientoLote = DateTime.MinValue;
            }
            else {
                this.VencimientoLote = VencimientoLote;
            }
            this.NumeroLote = NumeroLote;
            this.Perecedero = Perecedero;
            SalidasLote = new List<Salida>();
            IngresosLote = new List<Ingreso>();
        }

        //	Ingresos - Salidas

        /// <summary>
        /// Devuelve la cantidad ingresada del Suministro con el lote.
        /// </summary>
        /// <returns></returns>
        public float getCantidadIngresosLote() {
            float stock = 0f;
            for (int i = 0; i < this.IngresosLote.Count; i++) {
                stock += this.IngresosLote.ElementAt(i).CantidadIngreso;
            }
            return stock;
        }

        /// <summary>
        /// Devuelve la cantidad egresada del Suministro con el lote.
        /// </summary>
        /// <returns></returns>
        public float getCantidadSalidasLote() {
            float stock = 0f;
            for (int i = 0; i < this.SalidasLote.Count; i++) {
                stock += this.SalidasLote.ElementAt(i).CantidadSalida;
            }
            return stock;
        }

        /// <summary>
        /// Devuelve la cantidad en stock del Suministro con el lote.
        /// </summary>
        /// <returns></returns>
        public float getCantidadStock() {
            return this.getCantidadIngresosLote() - this.getCantidadSalidasLote();
        }

        public void AddIngresoLote(Ingreso IngresoLote) {
            this.IngresosLote.Add(IngresoLote);
            if(IngresoLote.LoteIngreso == null || !IngresoLote.LoteIngreso.Equals(this)) {
                IngresoLote.LoteIngreso = this;
            }
        }

        public void AddSalidaLote(Salida SalidaLote) {
            this.SalidasLote.Add(SalidaLote);
            if(SalidaLote.LoteSalida == null || !SalidaLote.LoteSalida.Equals(this)) {
                SalidaLote.LoteSalida = this;
            }
        }

        /// <summary>
        /// Comprueba cada ingreso y devuelve el ultimo registro.
        /// </summary>
        /// <returns></returns>
        public Ingreso getUltimoIngreso() {
            int index = 0;
            if (IngresosLote.Count != 0) {
                DateTime max = IngresosLote.ElementAt(0).FechaIngreso.Value;
                for (int i = 0; i < IngresosLote.Count(); i++) {
                    int compare = IngresosLote.ElementAt(i).FechaIngreso.Value.CompareTo(max);
                    if (compare == 1) {
                        max = IngresosLote.ElementAt(i).FechaIngreso.Value;
                        index = i;
                    }
                }
                return IngresosLote.ElementAt(index);
            }
            return null;
        }

        public bool EstaVencido() {
            if (!Perecedero) return false;
            DateTime hoy = DateTime.Today;
            bool vencido = false;
            try {
                int compare = VencimientoLote.Value.CompareTo(hoy);
                vencido = compare < 0;
            }
            catch (NullReferenceException ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return vencido;
        }
    }
}