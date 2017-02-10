using ControlSuministrosASP.Net.Models.ContextoBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.lote {
    public class ManejadorLote {
        private static SuministrosDBContext db = SuministrosDBContext.getInstancia();

        private static ManejadorLote Instancia;
        private ManejadorLote() { }
        public static ManejadorLote getInstancia() {
            if (Instancia == null) Instancia = new ManejadorLote();
            return Instancia;
        }

        public int CrearLote(Lote lote) {
            try {
                db.Lotes.Add(lote);
                db.SaveChanges();
                return lote.LoteId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int ActualizarLote(Lote lote) {
            try {
                db.Entry(lote).State = EntityState.Modified;
                db.SaveChanges();
                return lote.LoteId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int BorrarLote(int IdLote) {
            try {
                Lote lote = db.Lotes.Find(IdLote);
                db.Lotes.Remove(lote);
                db.SaveChanges();
                return IdLote;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public Lote ObtenerLote(int IdLote) {
            try {
                Lote lote = db.Lotes.Find(IdLote);
                return lote;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public Lote ObtenerLote(string NumeroLote) {
            try {
                var query = from lot in db.Lotes where lot.NumeroLote == NumeroLote select lot;
                return query.ElementAt(0);
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public IList<Lote> ListarLotes() {
            IList<Lote> lotees = new List<Lote>();
            try {
                lotees = db.Lotes.ToList<Lote>();
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return lotees;
        }

        public IDictionary<string, int> ListarDicLotes() {
            IDictionary<string, int> dic = new Dictionary<string, int>();
            try {
                IList<Lote> lotes = db.Lotes.ToList<Lote>();
                for (int i = 0; i < lotes.Count; i++) {
                    dic.Add(lotes.ElementAt(i).NumeroLote, lotes.ElementAt(i).LoteId);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dic;
        }

        public IDictionary<string, int> ListarDicLotes(int IdSuministro) {
            IDictionary<string, int> dic = new Dictionary<string, int>();
            try {
                var query = from lot in db.Lotes where lot.SuministroLote.SuministroId == IdSuministro select lot;
                IList<Lote> lotes = query.ToList<Lote>();
                for (int i = 0; i < lotes.Count; i++) {
                    dic.Add(lotes.ElementAt(i).NumeroLote, lotes.ElementAt(i).LoteId);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dic;
        }

        public IDictionary<int, Lote> ListarDicLotesFull() {
            IDictionary<int, Lote> dic = new Dictionary<int, Lote>();
            try {
                IList<Lote> lotees = db.Lotes.ToList<Lote>();
                for (int i = 0; i < lotees.Count; i++) {
                    dic.Add(lotees.ElementAt(i).LoteId, lotees.ElementAt(i));
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dic;
        }

        public IDictionary<int, Lote> ListarDicLotesFull(int IdSuministro) {
            IDictionary<int, Lote> dic = new Dictionary<int, Lote>();
            try {
                var query = from lot in db.Lotes where lot.SuministroLote.SuministroId == IdSuministro select lot;
                IList<Lote> lotees = query.ToList<Lote>();
                for (int i = 0; i < lotees.Count; i++) {
                    dic.Add(lotees.ElementAt(i).LoteId, lotees.ElementAt(i));
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dic;
        }

        public int ExisteNumeroLoteSuministro(String NumeroLote, int IdSuministro) {
            try {
                var query = from lot in db.Lotes join sum in db.Suministros on lot.SuministroLote equals sum where lot.NumeroLote == NumeroLote where sum.SuministroId == IdSuministro select lot.LoteId;
                return query.ElementAt(0);
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return 0;
        }
    }
}