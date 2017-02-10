using ControlSuministrosASP.Net.Models.ContextoBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.operario.permiso {
    public class ManejadorPermiso {
        private static SuministrosDBContext db = SuministrosDBContext.getInstancia();

        private static ManejadorPermiso Instancia;
        private ManejadorPermiso() { }
        public static ManejadorPermiso getInstancia() {
            if (Instancia == null) Instancia = new ManejadorPermiso();
            return Instancia;
        }

        public int CrearPermiso(Permiso permiso) {
            try {
                db.Permisos.Add(permiso);
                db.SaveChanges();
                return permiso.PermisoId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int ActualizarPermiso(Permiso permiso) {
            try {
                db.Entry(permiso).State = EntityState.Modified;
                db.SaveChanges();
                return permiso.PermisoId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int BorrarPermiso(int IdPermiso) {
            try {
                Permiso permiso = db.Permisos.Find(IdPermiso);
                db.Permisos.Remove(permiso);
                db.SaveChanges();
                return IdPermiso;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public Permiso ObtenerPermiso(int IdPermiso) {
            try {
                Permiso permiso = db.Permisos.Find(IdPermiso);
                return permiso;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public IList<Permiso> ListarPermisos() {
            IList<Permiso> permisos = new List<Permiso>();
            try {
                permisos = db.Permisos.ToList<Permiso>();
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return permisos;
        }
    }
}