using ControlSuministrosASP.Net.Models.ContextoBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.proveedor {
    public class ManejadorProveedor {
        private static SuministrosDBContext db = SuministrosDBContext.getInstancia();

        private static ManejadorProveedor Instancia;
        private ManejadorProveedor() { }
        public static ManejadorProveedor getInstancia() {
            if (Instancia == null) Instancia = new ManejadorProveedor();
            return Instancia;
        }

        public int CrearProveedor(Proveedor proveedor) {
            try {
                db.Proveedores.Add(proveedor);
                db.SaveChanges();
                return proveedor.ProveedorId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int ActualizarProveedor(Proveedor proveedor) {
            try {
                db.Entry(proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return proveedor.ProveedorId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int BorrarProveedor(int IdProveedor) {
            try {
                Proveedor proveedor = db.Proveedores.Find(IdProveedor);
                db.Proveedores.Remove(proveedor);
                db.SaveChanges();
                return IdProveedor;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public Proveedor ObtenerProveedor(int IdProveedor) {
            try {
                Proveedor proveedor = db.Proveedores.Find(IdProveedor);
                return proveedor;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public IList<Proveedor> ListarProveedores() {
            IList<Proveedor> proveedores = new List<Proveedor>();
            try {
                proveedores = db.Proveedores.ToList<Proveedor>();
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return proveedores;
        }

        public IDictionary<string, int> ListarDicProveedores() {
            IDictionary<string, int> dic = new Dictionary<string, int>();
            try {
                IList<Proveedor> proveedores = db.Proveedores.ToList<Proveedor>();
                for (int i = 0; i < proveedores.Count; i++) {
                    dic.Add(proveedores.ElementAt(i).NombreProveedor, proveedores.ElementAt(i).ProveedorId);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dic;
        }


        public IDictionary<int, Proveedor> ListarDicProveedoresFull() {
            IDictionary<int, Proveedor> dic = new Dictionary<int, Proveedor>();
            try {
                IList<Proveedor> proveedores = db.Proveedores.ToList<Proveedor>();
                for (int i = 0; i < proveedores.Count; i++) {
                    dic.Add(proveedores.ElementAt(i).ProveedorId, proveedores.ElementAt(i));
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dic;
        }
    }
}