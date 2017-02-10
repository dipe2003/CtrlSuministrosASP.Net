using ControlSuministrosASP.Net.Models.ContextoBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.lote {
    public class ManejadorIngresoSalida {
        private static SuministrosDBContext db = SuministrosDBContext.getInstancia();

        private static ManejadorIngresoSalida Instancia;
        private ManejadorIngresoSalida() { }
        public static ManejadorIngresoSalida getInstancia() {
            if (Instancia == null) Instancia = new ManejadorIngresoSalida();
            return Instancia;
        }

        public int CrearIngreso(Ingreso ingreso) {
            try {
                db.Ingresos.Add(ingreso);
                db.SaveChanges();
                return ingreso.IngresoId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int ActualizarIngreso(Ingreso ingreso) {
            try {
                db.Entry(ingreso).State = EntityState.Modified;
                db.SaveChanges();
                return ingreso.IngresoId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int BorrarIngreso(int IdIngreso) {
            try {
                Ingreso ingreso = db.Ingresos.Find(IdIngreso);
                db.Ingresos.Remove(ingreso);
                db.SaveChanges();
                return IdIngreso;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public Ingreso ObtenerIngreso(int IdIngreso) {
            try {
                Ingreso ingreso = db.Ingresos.Find(IdIngreso);
                return ingreso;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public IList<Ingreso> ListarIngresos() {
            IList<Ingreso> ingresos = new List<Ingreso>();
            try {
                ingresos = db.Ingresos.ToList<Ingreso>();
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return ingresos;
        }

        /*
        *   SALIDAS
        */
        public int CrearSalida(Salida salida) {
            try {
                db.Salidas.Add(salida);
                db.SaveChanges();
                return salida.SalidaId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int ActualizarSalida(Salida salida) {
            try {
                db.Entry(salida).State = EntityState.Modified;
                db.SaveChanges();
                return salida.SalidaId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int BorrarSalida(int IdSalida) {
            try {
                Salida salida = db.Salidas.Find(IdSalida);
                db.Salidas.Remove(salida);
                db.SaveChanges();
                return IdSalida;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public Salida ObtenerSalida(int IdSalida) {
            try {
                Salida salida = db.Salidas.Find(IdSalida);
                return salida;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public IList<Salida> ListarSalidas() {
            IList<Salida> salidas = new List<Salida>();
            try {
                salidas = db.Salidas.ToList<Salida>();
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return salidas;
        }
    }
}