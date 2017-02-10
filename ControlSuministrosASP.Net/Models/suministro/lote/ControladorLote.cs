using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.lote {
    public class ControladorLote {
        private static ManejadorLote mLote = ManejadorLote.getInstancia();
        private static ControladorSuministro cSuministro = ControladorSuministro.getInstancia();
        private static BufferSuministro buffer = BufferSuministro.getInstancia();

        private static ControladorLote Instancia;
        private ControladorLote() { }
        public static ControladorLote getInstancia() {
            if (Instancia == null) Instancia = new ControladorLote();
            return Instancia;
        }

        /// <summary>
        /// Crea un lote en la base de datos.
        /// </summary>
        /// <param name="VencimientoLote"></param>
        /// <param name="NumeroLote"></param>
        /// <param name="IdSuministro"></param>
        /// <returns>Retorna el id del lote creado. Retorna -1 si no se creo.</returns>
        public int CrearLote(DateTime VencimientoLote, string NumeroLote, int IdSuministro) {
            bool Perecedero = true;
            if (VencimientoLote == DateTime.MinValue) Perecedero = false;
            Lote lote = new Lote(VencimientoLote, NumeroLote, Perecedero);
            Suministro suministro = cSuministro.BuscarSuministro(IdSuministro);
            suministro.AddLote(lote);
            return mLote.CrearLote(lote);
        }

        /// <summary>
        /// Busca un lote por su IdLote en la base de datos.
        /// </summary>
        /// <param name="IdLote"></param>
        /// <returns></returns>
        public Lote BuscarLote(int IdLote) {
            return mLote.ObtenerLote(IdLote);
        }

        /// <summary>
        /// Busca un lote por su NumeroLote en la base de datos.
        /// </summary>
        /// <param name="NumeroLote"></param>
        /// <returns></returns>
        public Lote BuscarLotePorNumeroLote(string NumeroLote) {
            return mLote.ObtenerLote(NumeroLote);
        }
        /// <summary>
        /// Devuelve una lista con todos los lotes registrados en la base de datos. Si no hay lotes registrados devuelve una lista vacia.
        /// </summary>
        /// <returns></returns>
        public IList<Lote> ListarLotes() {
            return mLote.ListarLotes();
        }


        /// <summary>
        /// Agrega un lote ya creado al suministro especificado por su id.
        /// </summary>
        /// <param name="IdLote"></param>
        /// <param name="IdSuministro"></param>
        /// <returns>Retorna el id del lote. Retorna -1 si no se agrego.</returns>
        public int AgregarLoteSuministro(int IdLote, int IdSuministro) {
            Lote lote = mLote.ObtenerLote(IdLote);
            lote.SuministroLote = cSuministro.BuscarSuministro(IdSuministro);
            int id = mLote.ActualizarLote(lote);
            if (id != -1) {
                buffer.updateSuministro(cSuministro.BuscarSuministro(IdSuministro));
            }
            return id;
        }

        /// <summary>
        /// Comprueba la existencia del numero de lote para el suministro especificado.
        /// </summary>
        /// <param name="NumeroLote"></param>
        /// <param name="IdSuministro"></param>
        /// <returns>Retorna el id del suministro. Retorna 0 si no existe.</returns>
        public int ExisteLoteSuministro(string NumeroLote, int IdSuministro) {
            if (NumeroLote.Equals("")) return 0;
            return mLote.ExisteNumeroLoteSuministro(NumeroLote, IdSuministro);
        }

        /// <summary>
        /// Devuelve un Diccionario con los lotes registrados de un suministro en el sistema.
        /// </summary>
        /// <param name="IdSuministro"></param>
        /// <returns>Retorna un Diccionario con el numero de lote (key) e id (value)</returns>
        public IDictionary<string, int> ListarDicLotes(int IdSuministro) {
            return mLote.ListarDicLotes(IdSuministro);
        }


        /// <summary>
        /// Devuelve un Diccionario con los lotes con stock registrados de un suministro en el sistema.
        /// </summary>
        /// <param name="IdSuministro"></param>
        /// <param name="Vencimiento">True para anexar la fecha de vencimiento al numero de lote.</param>
        /// <returns>Retorna un Diccionario con el numero de lote (key) e id (value)</returns>
        public IDictionary<string, int> ListarDicLotesStock(int IdSuministro, bool Vencimiento) {
            IDictionary<string, int> strLotes = new Dictionary<string, int>();
            IDictionary<int, Lote> lotes = mLote.ListarDicLotesFull(IdSuministro);
            foreach (var lote in lotes.Values) {
                if (lote.getCantidadStock() != 0) {
                    if (Vencimiento) {
                        string fecha = string.Empty;
                        if(!lote.Perecedero) {
                            fecha = "No Perecedero";
                        }
                        else{
                            fecha = lote.VencimientoLote.Value.ToShortDateString();
                        }
                        strLotes.Add(lote.NumeroLote + " (Vto: " + fecha + ")", lote.LoteId);
                    }
                    else {
                        strLotes.Add(lote.NumeroLote, lote.LoteId);
                    }
                }
            }
            return strLotes;
        }
    }
}