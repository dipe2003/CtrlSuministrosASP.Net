using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.operario.permiso {
    public class ControladorPermiso {
        private static ManejadorPermiso mPermiso = ManejadorPermiso.getInstancia();
        private static ControladorPermiso Instancia;
        private ControladorPermiso() { }
        public static ControladorPermiso getInstancia() {
            if (Instancia == null) Instancia = new ControladorPermiso();
            return Instancia;
        }

        /// <summary>
        /// Crea un permiso en la base de datos.
        /// </summary>
        /// <param name="NombrePermiso"></param>
        /// <returns>Retorna el id del permiso creado. Si no se creo retorna -1.</returns>
        public int CrearPermiso(String NombrePermiso) {
            Permiso permiso = new Permiso(NombrePermiso);
            return mPermiso.CrearPermiso(permiso);
        }

        /// <summary>
        /// Busca un permiso en la base de datos.
        /// </summary>
        /// <param name="IdPermiso"></param>
        /// <returns></returns>
        public Permiso BuscarPermiso(int IdPermiso) {
            return mPermiso.ObtenerPermiso(IdPermiso);
        }

        /// <summary>
        /// Devuelve una lista con todos los permisos registrados en la base de datos.
        /// Si no hay permisos registrados devuelve una lista vacia.
        /// </summary>
        /// <returns></returns>
        public IList<Permiso> ListarPermisos() {
            return mPermiso.ListarPermisos();
        }
    }
}