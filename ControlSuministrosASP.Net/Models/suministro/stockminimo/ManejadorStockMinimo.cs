using ControlSuministrosASP.Net.Models.ContextoBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.stockminimo {
    public class ManejadorStockMinimo {
        private static SuministrosDBContext db = SuministrosDBContext.getInstancia();

        private static ManejadorStockMinimo Instancia;
        private ManejadorStockMinimo() { }
        public static ManejadorStockMinimo getInstancia() {
            if (Instancia == null) Instancia = new ManejadorStockMinimo();
            return Instancia;
        }

        public int CrearStockMinimo(StockMinimo unidad) {
            try {
                db.StocksMinimos.Add(unidad);
                db.SaveChanges();
                return unidad.StockMinimoId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int ActualizarStockMinimo(StockMinimo unidad) {
            try {
                db.Entry(unidad).State = EntityState.Modified;
                db.SaveChanges();
                return unidad.StockMinimoId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int BorrarStockMinimo(int IdStockMinimo) {
            try {
                StockMinimo stock = db.StocksMinimos.Find(IdStockMinimo);
                db.StocksMinimos.Remove(stock);
                db.SaveChanges();
                return IdStockMinimo;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public StockMinimo ObtenerStockMinimo(int IdStockMinimo) {
            try {
                StockMinimo stock = db.StocksMinimos.Find(IdStockMinimo);
                return stock;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public IList<StockMinimo> ListarStockMinimoes() {
            IList<StockMinimo> stocks = new List<StockMinimo>();
            try {
                stocks = db.StocksMinimos.ToList<StockMinimo>();
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return stocks;
        }

        public StockMinimo ObtenerStockMinimoSuministro(int IdSuministro) {
            try {
                var query = from stock in db.StocksMinimos join sum in db.Suministros on stock.StockMinimoSuministro equals sum where sum.SuministroId == IdSuministro select stock;
                return query.ElementAt(0);
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }
    }
}