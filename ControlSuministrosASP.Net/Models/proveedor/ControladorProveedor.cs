using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.proveedor {
    public class ControladorProveedor {
        private static ManejadorProveedor mProveedor = ManejadorProveedor.getInstancia();
        private static BufferProveedor buffer = BufferProveedor.getInstancia();

        private static ControladorProveedor Instancia;
        private ControladorProveedor() { }
        public static ControladorProveedor getInstancia() {
            if (Instancia == null) Instancia = new ControladorProveedor();
            return Instancia;
        }

        /// <summary>
        /// Crea un proveedor en la base de datos.
        /// </summary>
        /// <param name="NombreProveedor"></param>
        /// <param name="ContactoProveedor"></param>
        /// <returns>etorna el id del proveedor creado. Si no se creo retorna -1.</returns>
        public int CrearProveedor(String NombreProveedor, String ContactoProveedor) {
            Proveedor proveedor = new Proveedor(NombreProveedor, ContactoProveedor);
            int id = mProveedor.CrearProveedor(proveedor);
            if (id != -1) {
                buffer.putProveedor(proveedor);
            }
            return id;
        }

        /// <summary>
        /// Busca un proveedor en la base de datos.
        /// </summary>
        /// <param name="IdProveedor"></param>
        /// <returns></returns>
        public Proveedor BuscarProveedor(int IdProveedor) {
            if (buffer.containsProveedor(IdProveedor)) return buffer.getProveedor(IdProveedor);
            return mProveedor.ObtenerProveedor(IdProveedor);
        }

        /// <summary>
        /// Devuelve una lista con todos los proveedores registrados en la base de datos.
        /// Si no hay proveedores registrados devuelve una lista vacia.
        /// </summary>
        /// <returns></returns>
        public IList<Proveedor> ListarProveedores() {
            if (buffer.bufferSize() > 0) return buffer.getListaProveedores();
            return mProveedor.ListarProveedores();
        }

        /// <summary>
        /// Actualiza los datos de un usuario especificado por su id.
        /// </summary>
        /// <param name="IdProveedor"></param>
        /// <param name="NombreProveedor"></param>
        /// <param name="ContactoProveedor"></param>
        /// <returns>Retorna el id del proveedor. Retorna -1 si no se pudo actualizar.</returns>
        public int ModificarDatosProveedor(int IdProveedor, String NombreProveedor, String ContactoProveedor) {
            Proveedor proveedor = mProveedor.ObtenerProveedor(IdProveedor);
            proveedor.NombreProveedor = NombreProveedor;
            proveedor.ContactoProveedor = ContactoProveedor;
            int id = mProveedor.ActualizarProveedor(proveedor);
            if (id != -1) {
                buffer.updateProveedor(proveedor);
            }
            return id;
        }

        /// <summary>
        /// Devuelve un Diccionario con los proveedores registrados en el sistema.
        /// </summary>
        /// <returns>Retorna un Diccionario con los Nombres de los proveedores (key) y los id (value)</returns>
        public IDictionary<string, int> ListarDicProveedores() {
            if (buffer.bufferSize() > 0) return buffer.getDicNombreProveedores();
            return mProveedor.ListarDicProveedores();
        }


        /// <summary>
        /// Crea y devuelve un diccionario con el proveedor del suministro.
        /// </summary>
        /// <param name="IdSuministro"></param>
        /// <returns>Retorna un Diccionario con los nombres de los proveedores (key) y sus id(values).</returns>
        public IDictionary<string, int> DicProveedorSuministro(int IdSuministro) {
            IList<Proveedor> proveedores;
            if (buffer.bufferSize() >= 0) {
                proveedores = buffer.getListaProveedores();
            }
            else {
                proveedores = mProveedor.ListarProveedores();
            }
            IDictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var proveedor in proveedores) {
                if (proveedor.esProveedorSuministro(IdSuministro)) dic.Add(proveedor.NombreProveedor, proveedor.ProveedorId);
            }
            return new SortedDictionary<string, int>(dic);
        }
    }
}