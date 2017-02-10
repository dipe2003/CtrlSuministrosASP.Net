using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.proveedor {
    public class BufferProveedor {
        private static BufferProveedor Instancia;
        private IDictionary<int, Proveedor> DicProveedores = new Dictionary<int, Proveedor>();

        private BufferProveedor() { }

        public static BufferProveedor getInstancia() {
            if (Instancia == null) Instancia = new BufferProveedor();
            return Instancia;
        }

        public Proveedor getProveedor(int IdProveedor) {
            return DicProveedores[IdProveedor];
        }

        public void putProveedor(Proveedor proveedor) {
            DicProveedores.Add(proveedor.ProveedorId, proveedor);
        }

        public void removeProveedor(Proveedor proveedor) {
            DicProveedores.Remove(proveedor.ProveedorId);
        }

        public void updateProveedor(Proveedor proveedor) {
            DicProveedores.Remove(proveedor.ProveedorId);
            DicProveedores.Add(proveedor.ProveedorId, proveedor);
        }

        public bool containsProveedor(int IdProveedor) {
            return DicProveedores.ContainsKey(IdProveedor);
        }

        public int bufferSize() {
            return DicProveedores.Count;
        }

        public IList<Proveedor> getListaProveedores() {
            IList<Proveedor> lista = new List<Proveedor>();
            foreach (var proveedor in DicProveedores.Values) {
                lista.Add(proveedor);
            }
            return lista;
        }


        public IDictionary<string, int> getDicNombreProveedores() {
            IDictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var proveedor in DicProveedores.Values) {
                dic.Add(proveedor.NombreProveedor, proveedor.ProveedorId);
            }
            return new SortedDictionary<string, int>(dic);
        }

        public IDictionary<int, Proveedor> getDicProveedores() {
            return DicProveedores;
        }
    }
}