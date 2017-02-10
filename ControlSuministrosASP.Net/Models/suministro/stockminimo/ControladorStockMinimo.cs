using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.stockminimo {
    public class ControladorStockMinimo {
        private static ManejadorStockMinimo mStockMinimo = ManejadorStockMinimo.getInstancia();

        private static ControladorStockMinimo Instancia;
        private ControladorStockMinimo() { }
        public static ControladorStockMinimo getInstancia() {
            if (Instancia == null) Instancia = new ControladorStockMinimo();
            return Instancia;
        }

        public StockMinimo CrearStockMinimo(float CantidadStockMinimo, DateTime FechaVigenciaStockMinimo) {
            StockMinimo stock = new StockMinimo(CantidadStockMinimo, FechaVigenciaStockMinimo, true);
            if (mStockMinimo.CrearStockMinimo(stock) != -1) {
                return stock;
            }
            return null;
        }

        public StockMinimo BuscarStockMinimo(int IdStockMinimo) {
            return mStockMinimo.ObtenerStockMinimo(IdStockMinimo);
        }
    }
}