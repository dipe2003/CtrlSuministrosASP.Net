using ControlSuministrosASP.Net.Models.operario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.lote {
    public class ControladorIngresoSalida {
        private static ManejadorIngresoSalida mInSal = ManejadorIngresoSalida.getInstancia();
        private static ControladorLote cLote = ControladorLote.getInstancia();
        private static ControladorOperario cOp = ControladorOperario.getInstancia();
        private static ManejadorSuministro mSum = ManejadorSuministro.getInstancia();
        private static BufferSuministro buffer = BufferSuministro.getInstancia();

        private static ControladorIngresoSalida Instancia;
        private ControladorIngresoSalida() { }
        public static ControladorIngresoSalida getInstancia() {
            if (Instancia == null) Instancia = new ControladorIngresoSalida();
            return Instancia; 
        }

        /// <summary>
        /// Crea un ingreso en la base de datos.
        /// </summary>
        /// <param name="FechaIngreso"></param>
        /// <param name="CantidadIngreso"></param>
        /// <param name="NumeroFactura"></param>
        /// <param name="IdLoteIngreso"></param>
        /// <param name="IdOperarioIngreso"></param>
        /// <param name="ObservacionesIngreso"></param>
        /// <param name="IdSuministro"></param>
        /// <returns>Retorna el id del ingreso creado. Retorna -1 si no se crea.</returns>
        public int CrearIngreso(DateTime FechaIngreso, float CantidadIngreso, string NumeroFactura, int IdLoteIngreso, int IdOperarioIngreso, string ObservacionesIngreso, int IdSuministro) {
            int id = -1;
            try {
                Operario operario = cOp.BuscarOperario(IdOperarioIngreso);
                Ingreso ingreso = new Ingreso(FechaIngreso, CantidadIngreso, NumeroFactura, ObservacionesIngreso);
                ingreso.LoteIngreso = cLote.BuscarLote(IdLoteIngreso);
                ingreso.OperarioIngresoSuministro = operario;
                id = mInSal.CrearIngreso(ingreso);
                if (id != -1) {
                    if (id != -1) buffer.updateSuministro(mSum.ObtenerSuministro(IdSuministro));
                }
            }
            catch (NullReferenceException ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return id;
        }

        /// <summary>
        /// Busca un ingreso en la base de datos.
        /// </summary>
        /// <param name="IdIngreso"></param>
        /// <returns></returns>
        public Ingreso BuscarIngreso(int IdIngreso) {
            return mInSal.ObtenerIngreso(IdIngreso);
        }

        /// <summary>
        /// Devuelve una lista con todos los ingresos registrados en la base de datos.
        ///Si no hay ingresos registrados devuelve una lista vacia.
        /// </summary>
        /// <returns></returns>
        public IList<Ingreso> ListarIngresos() {
            return mInSal.ListarIngresos();
        }

        /*
        *  Salidas
        */

        /// <summary>
        /// Crea un salida en la base de datos.
        /// </summary>
        /// <param name="FechaSalida"></param>
        /// <param name="CantidadSalida"></param>
        /// <param name="IdLoteSalida"></param>
        /// <param name="IdOperarioSalida"></param>
        /// <param name="ObservacionesSalida"></param>
        /// <returns>Retorna el id de la salida creada. Retorna -1 si no se crea.</returns>
        public int CrearSalida(DateTime FechaSalida, float CantidadSalida, int IdLoteSalida, int IdOperarioSalida, string ObservacionesSalida) {
            int id = -1;
            try {
                Operario operario = cOp.BuscarOperario(IdOperarioSalida);
                Salida salida = new Salida(FechaSalida, CantidadSalida, ObservacionesSalida);
                salida.LoteSalida = cLote.BuscarLote(IdLoteSalida);
                salida.OperarioSalidaSuministro = operario;
                id = mInSal.CrearSalida(salida);
                if (id != -1) buffer.updateSuministro(salida.LoteSalida.SuministroLote);
            }
            catch (NullReferenceException ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return id;
        }

        /// <summary>
        /// Busca un salida en la base de datos.
        /// </summary>
        /// <param name="IdSalida"></param>
        /// <returns></returns>
        public Salida BuscarSalida(int IdSalida) {
            return mInSal.ObtenerSalida(IdSalida);
        }
        /// <summary>
        /// Devuelve una lista con todos los salidas registrados en la base de datos.
        /// Si no hay salidas registrados devuelve una lista vacia.
        /// </summary>
        /// <returns></returns>
        public IList<Salida> ListarSalidas() {
            return mInSal.ListarSalidas();
        }
    }
}