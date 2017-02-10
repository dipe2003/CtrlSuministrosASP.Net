using ControlSuministrosASP.Net.Models.ContextoBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.unidad {
    public class ManejadorUnidad {
        private static SuministrosDBContext db = SuministrosDBContext.getInstancia();

        private static ManejadorUnidad Instancia;
        private ManejadorUnidad() { }
        public static ManejadorUnidad getInstancia() {
            if (Instancia == null) Instancia = new ManejadorUnidad();
            return Instancia;
        }

        public int CrearUnidad(Unidad unidad) {
            try {
                db.Unidades.Add(unidad);
                db.SaveChanges();
                return unidad.UnidadId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int ActualizarUnidad(Unidad unidad) {
            try {
                db.Entry(unidad).State = EntityState.Modified;
                db.SaveChanges();
                return unidad.UnidadId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int BorrarUnidad(int IdUnidad) {
            try {
                Unidad unidad = db.Unidades.Find(IdUnidad);
                db.Unidades.Remove(unidad);
                db.SaveChanges();
                return IdUnidad;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public Unidad ObtenerUnidad(int IdUnidad) {
            try {
                Unidad unidad = db.Unidades.Find(IdUnidad);
                return unidad;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public IList<Unidad> ListarUnidades() {
            IList<Unidad> unidades = new List<Unidad>();
            try {
                unidades = db.Unidades.ToList<Unidad>();
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return unidades;
        }

        public Unidad ObtenerUnidadSuministro(int IdSuministro) {
            try {
                var query = from sum in db.Suministros join un in db.Unidades on sum.UnidadSuministro equals un where sum.SuministroId == IdSuministro select un;
                return query.ElementAt(0);
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }
    }
}