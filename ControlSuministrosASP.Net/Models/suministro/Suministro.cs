using ControlSuministrosASP.Net.Models.proveedor;
using ControlSuministrosASP.Net.Models.suministro.lote;
using ControlSuministrosASP.Net.Models.suministro.stockminimo;
using ControlSuministrosASP.Net.Models.suministro.unidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro {
    [Table("Suministros")]
    public abstract class Suministro {
        public int SuministroId { get; set; }
        public string NombreSuministro { get; set; }
        public string DescripcionSuministro { get; set; }
        public string CodigoSAPSuministro { get; set; }
        public bool Vigente { get; set; }

        private Unidad unidadSuministro;
        public virtual Unidad UnidadSuministro {
            get {
                return this.unidadSuministro;
            }
            set {
                this.unidadSuministro = value;
                if (!value.SuministrosUnidad.Contains(this)) {
                    value.addSuministroUnidad(this);
                }
            }
        }

        public virtual IList<Lote> LotesSuministros { get; set; }
        public virtual IList<StockMinimo> StocksMinimosSuministro { get; set; }

        public StockMinimo StockMinimoVigente {
            get { return this.getStockMinimoSuministro(); }

        }

        public string TipoSuministro {
            get {
                string[] arr = this.GetType().ToString().Split('.');
                return (arr[4].Split('_'))[0];
            }
        }

        public DateTime UltimoIngreso {
            get {
                DateTime fecha = DateTime.MinValue;
                try {
                    fecha = getUltimoIngreso().FechaIngreso.Value;
                }catch(NullReferenceException ex) { }
                return fecha;
            }
        }

        public float StockActual {
            get {
                return this.getStock();
            }
        }

        public bool TieneLotesVencidos {
            get {
                if (this.getLotesVencidosEnStock().Count() > 0) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public bool DebajoStockMinimo {
            get {
                return this.isDebajoStockMinimo();
            }
        }


        public virtual Proveedor ProveedorSuministro { get; set; }

        //	Constructores
        public Suministro() { }
        public Suministro(string NombreSuministro, string DescripcionSuministro, string CodigoSAPSuministro,
                Unidad UnidadSuministro, Proveedor ProveedorSuministro) {
            this.NombreSuministro = NombreSuministro;
            this.DescripcionSuministro = DescripcionSuministro;
            this.CodigoSAPSuministro = CodigoSAPSuministro;
            this.UnidadSuministro = UnidadSuministro;
            this.StocksMinimosSuministro = new List<StockMinimo>();
            this.LotesSuministros = new List<Lote>();
            this.ProveedorSuministro = ProveedorSuministro;
            this.Vigente = true;
        }


        //	StocksMinimos
        public void AddStockMinimoSuministro(StockMinimo StockMinimoSuministro) {
            for (int i = 0; i < this.StocksMinimosSuministro.Count; i++) {
                this.StocksMinimosSuministro.ElementAt(i).VigenciaStockMinimo = false;
            }
            this.StocksMinimosSuministro.Add(StockMinimoSuministro);
            if (StockMinimoSuministro.StockMinimoSuministro == null || !StockMinimoSuministro.StockMinimoSuministro.Equals(this)) {
                StockMinimoSuministro.StockMinimoSuministro = this;
            }
        }
        public StockMinimo getStockMinimoSuministro() {
            for (int i = 0; i < StocksMinimosSuministro.Count; i++) {
                if (StocksMinimosSuministro.ElementAt(i).VigenciaStockMinimo) {
                    return StocksMinimosSuministro.ElementAt(i);
                }
            }
            return null;
        }

        //	Lotes
        public void AddLote(Lote LoteSuministro) {
            this.LotesSuministros.Add(LoteSuministro);
            if (LoteSuministro.SuministroLote == null || !LoteSuministro.SuministroLote.Equals(this)) {
                LoteSuministro.SuministroLote = this;
            }
        }

        public float getStock() {
            float stock = 0f;
            for (int i = 0; i < this.LotesSuministros.Count; i++) {
                stock += this.LotesSuministros.ElementAt(i).getCantidadStock();
            }
            return stock;
        }

        public float getTotalIngreso() {
            float total = 0f;
            for (int i = 0; i < this.LotesSuministros.Count; i++) {
                total += LotesSuministros.ElementAt(i).getCantidadIngresosLote();
            }
            return total;
        }

        public float getTotalSalida() {
            float total = 0f;
            for (int i = 0; i < LotesSuministros.Count; i++) {
                total += LotesSuministros.ElementAt(i).getCantidadSalidasLote();
            }
            return total;
        }

        public bool isDebajoStockMinimo() {
            return getStockMinimoSuministro().CantidadStockMinimo > getStock();
        }

        /// <summary>
        /// Devuelve los lotes vencidos.No se tiene en cuenta si existe en stock o se dio de baja.
        /// </summary>
        /// <returns></returns>
        public List<Lote> getLotesVencidos() {
            List<Lote> lotes = new List<Lote>();
            DateTime hoy = DateTime.Today;
            for (int i = 0; i < LotesSuministros.Count; i++) {
                Lote lote = LotesSuministros.ElementAt(i);
                if (lote.Perecedero) {
                    try {
                        int compare = lote.VencimientoLote.Value.CompareTo(hoy);
                        if (compare < 0) {
                            lotes.Add(lote);
                        }
                    }
                    catch (NullReferenceException ex) {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return lotes;
        }

        /// <summary>
        /// Devuelve los lotes vencidos.Solo se devuelven los que existan en stock.
        /// </summary>
        /// <returns></returns>
        public List<Lote> getLotesVencidosEnStock() {
            List<Lote> lotes = new List<Lote>();
            DateTime hoy = DateTime.Today;
            for (int i = 0; i < LotesSuministros.Count; i++) {
                Lote lote = LotesSuministros.ElementAt(i);
                if (lote.Perecedero && lote.getCantidadStock()>0) {
                    try {
                        int compare = lote.VencimientoLote.Value.CompareTo(hoy);
                        if (compare < 0) {
                            lotes.Add(lote);
                        }
                    }
                    catch (NullReferenceException ex) {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return lotes;
        }

        /// <summary>
        /// Retorna el ultimo ingreso del suministro.
        /// </summary>
        /// <returns>Retorna null si no hay ingresos.</returns>
        public Ingreso getUltimoIngreso() {
            int index = 0;
            if (LotesSuministros.Count != 0) {
                for (int i = 0; i < this.LotesSuministros.Count; i++) {
                    Ingreso ingreso = LotesSuministros.ElementAt(i).getUltimoIngreso();
                    if (ingreso != null) {
                        DateTime max = ingreso.FechaIngreso.Value;
                        int compare = LotesSuministros.ElementAt(i).getUltimoIngreso().FechaIngreso.Value.CompareTo(max);
                        if (compare == -1) {
                            max = this.LotesSuministros.ElementAt(i).getUltimoIngreso().FechaIngreso.Value;
                            index = i;
                        }
                    }
                }
                return this.LotesSuministros.ElementAt(index).getUltimoIngreso();
            }
            return null;
        }
    }
}