using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.stockminimo {
    [Table("Stocksminimos")]
    public class StockMinimo {
        public int StockMinimoId { get; set; }
        public float CantidadStockMinimo { get; set; }
        public DateTime FechaVigenteStockMinimo { get; set; }
        public bool VigenciaStockMinimo { get; set; }

        private Suministro stockMinimoSuministro;
        public virtual Suministro StockMinimoSuministro
        {
            get { return this.stockMinimoSuministro; }
            set
            {
                this.stockMinimoSuministro = value;
                if (value.StocksMinimosSuministro == null) value.StocksMinimosSuministro = new List<StockMinimo>();
                if (!value.StocksMinimosSuministro.Contains(this)){
                    value.AddStockMinimoSuministro(this);
                }
            }
        }

        //	Constructores
        public StockMinimo() { }
        public StockMinimo(float CantidadStockMinimo, DateTime FechaVigenteStockMinimo, bool VigenciaStockMinimo) {
            this.CantidadStockMinimo = CantidadStockMinimo;
            this.FechaVigenteStockMinimo = FechaVigenteStockMinimo;
            this.VigenciaStockMinimo = VigenciaStockMinimo;
        }
    }
}