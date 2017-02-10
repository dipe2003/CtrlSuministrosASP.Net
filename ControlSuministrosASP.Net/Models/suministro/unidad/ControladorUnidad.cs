using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.unidad {
    public class ControladorUnidad {
        private static ManejadorUnidad mUnidad = ManejadorUnidad.getInstancia();

        private static ControladorUnidad Instancia;
        private ControladorUnidad() { }
        public static ControladorUnidad getInstancia() {
            if (Instancia == null) Instancia = new ControladorUnidad();
            return Instancia;
        }

        public int CrearUnidad(String NombreUnidad) {
            Unidad unidad = new Unidad(NombreUnidad);
            return mUnidad.CrearUnidad(unidad);
        }

        public Unidad BuscarUnidad(int IdUnidad) {
            return mUnidad.ObtenerUnidad(IdUnidad);
        }

        public IList<Unidad> ListarUnidades() {
            return mUnidad.ListarUnidades();
        }

        public Unidad BuscarUnidadSuministro(int IdSuministro) {
            return mUnidad.ObtenerUnidadSuministro(IdSuministro);
        }
    }
}