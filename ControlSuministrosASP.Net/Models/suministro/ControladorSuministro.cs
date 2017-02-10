using ControlSuministrosASP.Net.Models.proveedor;
using ControlSuministrosASP.Net.Models.suministro.stockminimo;
using ControlSuministrosASP.Net.Models.suministro.unidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro {
    public class ControladorSuministro {
        private static ControladorUnidad cUnidad = ControladorUnidad.getInstancia();
        private static ManejadorSuministro mSuministro = ManejadorSuministro.getInstancia();
        private static ControladorProveedor cProveedor = ControladorProveedor.getInstancia();
        private static BufferSuministro buffer = BufferSuministro.getInstancia();
        private static ControladorStockMinimo cStock = ControladorStockMinimo.getInstancia();

        private static ControladorSuministro Instancia;
        private ControladorSuministro() { }
        public static ControladorSuministro getInstancia() {
            if (Instancia == null) Instancia = new ControladorSuministro();
            return Instancia;
        }

        /// <summary>
        /// Crea un Crea un Reactivo Quimico en la base de datos.
        /// </summary>
        /// <param name="NombreSuministro"></param>
        /// <param name="DescripcionSuministro"></param>
        /// <param name="CodigoSAPSuministro"></param>
        /// <param name="IdUnidadSuministro"></param>
        /// <param name="IdProveedorSuministro"></param>
        /// <param name="TipoSuministro">Material, MedioEnsayo, ReactivoQuimico</param>
        /// <returns>Devuelve el Id del suministro creado. Devuelve -1 cuando no se guardo el suministro.</returns>
        public int CrearSuministro(string NombreSuministro, string DescripcionSuministro,
                string CodigoSAPSuministro, int IdUnidadSuministro, int IdProveedorSuministro, string TipoSuministro) {
            Suministro suministro = null;
            switch (TipoSuministro) {
                case "Material":
                    suministro = new Material(NombreSuministro, DescripcionSuministro, CodigoSAPSuministro,
                            cUnidad.BuscarUnidad(IdUnidadSuministro), cProveedor.BuscarProveedor(IdProveedorSuministro));
                    break;

                case "MedioEnsayo":
                    suministro = new MedioEnsayo(NombreSuministro, DescripcionSuministro, CodigoSAPSuministro,
                            cUnidad.BuscarUnidad(IdUnidadSuministro), cProveedor.BuscarProveedor(IdProveedorSuministro));
                    break;

                case "ReactivoQuimico":
                    suministro = new ReactivoQuimico(NombreSuministro, DescripcionSuministro, CodigoSAPSuministro,
                            cUnidad.BuscarUnidad(IdUnidadSuministro), cProveedor.BuscarProveedor(IdProveedorSuministro));
                    break;
            }
            int id = mSuministro.CrearSuministro(suministro);
            if (id != -1) {
                buffer.putSuministro(suministro);
            }
            return id;
        }

        /// <summary>
        /// Busca un suministro segun el id especificado.
        /// </summary>
        /// <param name="IdSuministro"></param>
        /// <returns></returns>
        public Suministro BuscarSuministro(int IdSuministro) {
            if (buffer.containsSuministro(IdSuministro)) return buffer.getSuministro(IdSuministro);
            return mSuministro.ObtenerSuministro(IdSuministro);
        }

        /// <summary>
        /// Devuelve todos los suministros registrados en la base de datos. Si no hay suministros devuelve una lista vacia.
        /// </summary>
        /// <param name="Vigente">True: indica si solo se devuelven los suministros en uso.</param>
        /// <returns></returns>
        public IList<Suministro> ListarSuministros(bool Vigente) {
            if (buffer.bufferSize() > 0) return buffer.getListaSuministros(Vigente);
            return mSuministro.ListarSuministros(Vigente);
        }

        /// <summary>
        /// Registrar Stock Minimo de Suministro
        /// </summary>
        /// <param name="CantidadStockMinimo">cantidad de suministro</param>
        /// <param name="FechaVigenteStockMinimo">fecha de entrada en vigencia</param>
        /// <param name="IdSuministro"></param>
        /// <returns></returns>
        public int RegistrarStockMinimoSuministro(float CantidadStockMinimo, DateTime FechaVigenteStockMinimo, int IdSuministro) {
            StockMinimo stockMinimo = cStock.CrearStockMinimo(CantidadStockMinimo, FechaVigenteStockMinimo);
            Suministro suministro = mSuministro.ObtenerSuministro(IdSuministro);
            suministro.AddStockMinimoSuministro(stockMinimo);
            int id = mSuministro.ActualizarSuministro(suministro);
            if (id != -1) {
                buffer.updateSuministro(suministro);
            }
            return id;
        }

        /// <summary>
        /// Devuelve los suministros del proveedore especificado por su id.
        /// </summary>
        /// <param name="IdProveedor"></param>
        /// <returns>Retorna un map con el nombre de los suministros (key) y sus id (value). Retorna un map vacio si no hay suministros registrados.</returns>
        public IDictionary<string, int> ListarSuministrosProveedor(int IdProveedor) {
            if (buffer.bufferSize() > 0) return buffer.getDicSuministrosPorProveedor(IdProveedor);
            return mSuministro.ListarSuministrosProveedor(IdProveedor);
        }

        /// <summary>
        /// Devuelve los suministros registrados en la base de datos. Solo los suministros vigentes.
        /// </summary>
        /// <param name="Vigente"></param>
        /// <returns>Retorna un map con el nombre de los suministros (key) y sus id (value). Retorna un map vacio si no hay suministros registrados.</returns>
        public IDictionary<string, int> ListarDicSuministros(bool Vigente) {
            if (buffer.bufferSize() > 0) return buffer.getDicNombreSuministros(Vigente);
            return mSuministro.ListarDicSuministros(Vigente);
        }

        /// <summary>
        /// Devuelve los suministros registrados en la base de datos. Solo los suministros vigentes.
        /// </summary>
        /// <returns>Retorna un map con los ids de suministros (key) y los suministros (value). Retorna un map vacio si no hay suministros registrados.</returns>
        public IDictionary<int, Suministro> ListarMapSuministrosFull() {
            if (buffer.bufferSize() > 0) return buffer.getDicSuministros();
            return mSuministro.ListarDicSuministrosFull();
        }

        /// <summary>
        /// Calcula la cantidad de suministros que están por debajo de su stock minimo.
        /// Solo se toman en cuenta aquellos suministros que tengan un stock minimo definido (cantidad mayor a 0) y esten vigentes.
        /// </summary>
        /// <returns>Retorna array[0] = total de suministros y array[1]= total de suministros debajo de stock minimo</returns>
        public int[] GetTotalSuministrosDebajoStockMinimo() {
            IList<Suministro> suministros;
            if (buffer.bufferSize() > 0) {
                suministros = buffer.getListaSuministros(true);
            }
            else {
                suministros = mSuministro.ListarSuministros(true);
            }
            int cantidad = 0;
            foreach (var suministro in suministros) {
                if (suministro.getStock() < suministro.getStockMinimoSuministro().CantidadStockMinimo &&
                        suministro.getStockMinimoSuministro().CantidadStockMinimo > 0) {
                    cantidad++;
                }
            }
            return new int[] { suministros.Count, cantidad };
        }

        /// <summary>
        /// Devuelve los suministros que están por debajo de su stock minimo.
        /// Solo se toman en cuenta aquellos suministros que tengan un stock minimo definido (cantidad mayor a 0) y esten vigentes.
        /// </summary>
        /// <returns>Retorna una lista con los ids de suministros debajo de stock minimo, retorna una lista vacia si no los hay.</returns>
        public IList<int> GetIdsSuministrosDebajoStockMinimo() {
            IList<Suministro> suministros;
            if (buffer.bufferSize() > 0) {
                suministros = buffer.getListaSuministros(true);
            }
            else {
                suministros = mSuministro.ListarSuministros(true);
            }
            IList<int> lista = new List<int>();
            foreach (var suministro in suministros) {
                try {
                    if (suministro.getStock() < suministro.getStockMinimoSuministro().CantidadStockMinimo &&
                            suministro.getStockMinimoSuministro().CantidadStockMinimo > 0) {
                        lista.Add(suministro.SuministroId);
                    }
                }
                catch (NullReferenceException ex) {
                    Console.WriteLine("Error: " + ex.Message + " en " + suministro.NombreSuministro + suministro.SuministroId);
                }
            }
            return lista;
        }

        /// <summary>
        /// Devuelve los suministros con lotes vencidos y que estén vigentes.
        /// </summary>
        /// <param name="ConStock"><b>True:</b> para obtener solo los que tengan stock. <b>False:</b> para obtener todos.</param>
        /// <returns>Map: key: idSuministros, value: nombreSuministro</returns>
        public IDictionary<string, int> getDicSuministrosConLotesVencidos(bool ConStock) {
            IList<Suministro> suministros;
            if (buffer.bufferSize() > 0) {
                suministros = buffer.getListaSuministros(true);
            }
            else {
                suministros = mSuministro.ListarSuministros(true);
            }
            IDictionary<string, int> dic = new Dictionary<string, int>();
            if (ConStock) {
                foreach (var suministro in suministros) {
                    if (suministro.getLotesVencidosEnStock().Count > 0) dic.Add(suministro.NombreSuministro, suministro.SuministroId);
                }
                return new SortedDictionary<string, int>(dic);
            }
            foreach (var suministro in suministros) {
                if (suministro.getLotesVencidos().Count > 0) dic.Add(suministro.NombreSuministro, suministro.SuministroId);
            }
            return new SortedDictionary<string, int>(dic);
        }

        /// <summary>
        /// Devuelve los suministros con lotes vencidos y que estén vigentes.
        /// </summary>
        /// <param name="ConStock"><b>True:</b> para obtener solo los que tengan stock. <b>False:</b> para obtener todos.</param>
        /// <returns>Retorna una lista con los ids de los suministros.</returns>
        public IList<int> getIdsSuministrosConLotesVencidos(bool ConStock) {
            IList<Suministro> suministros;
            if (buffer.bufferSize() > 0) {
                suministros = buffer.getListaSuministros(true);
            }
            else {
                suministros = mSuministro.ListarSuministros(true);
            }
            IList<int> lista = new List<int>();
            if (ConStock) {
                foreach (var suministro in suministros) {
                    if (suministro.getLotesVencidosEnStock().Count > 0) lista.Add(suministro.SuministroId);
                }
                return lista;
            }
            foreach (var suministro in suministros) {
                if (suministro.getLotesVencidos().Count > 0) lista.Add(suministro.SuministroId);
            }
            return lista;
        }

        /// <summary>
        /// Devuelve los suministros con lotes vencidos y que estén vigentes.
        /// </summary>
        /// <param name="ConStock"><b>True:</b> para obtener solo los que tengan stock. <b>False:</b> para obtener todos.</param>
        /// <returns>Retorna una lista con los suministros. Retorna una lista vacia si no existen lotes vencidos en stock</returns>
        public IList<Suministro> getSuministrosConLotesVencidos(bool ConStock) {
            IList<Suministro> suministros;
            if (buffer.bufferSize() > 0) {
                suministros = buffer.getListaSuministros(true);
            }
            else {
                suministros = mSuministro.ListarSuministros(true);
            }
            IList<Suministro> lista = new List<Suministro>();
            if (ConStock) {
                foreach (var suministro in suministros) {
                    if (suministro.getLotesVencidosEnStock().Count > 0) lista.Add(suministro);
                }
                return lista;
            }
            foreach (var suministro in suministros) {
                if (suministro.getLotesVencidos().Count > 0) lista.Add(suministro);
            }
            return lista;
        }

        /// <summary>
        /// Devuelve los suministros con un stock actual debajo del minimo definido.
        /// </summary>
        /// <param name="vigentes"></param>
        /// <returns></returns>
        public IList<Suministro> GetSuministrosDebajoDeStock(bool vigentes) {
            IList<Suministro> suministros = new List<Suministro>();
            IList<Suministro> tmp = new List<Suministro>();
            tmp = mSuministro.ListarSuministros(vigentes);
            foreach(Suministro item in tmp) {
                if(item.DebajoStockMinimo) suministros.Add(item);
            }
            return suministros;
        }

        /// <summary>
        /// Actualiza un suministro en la base de datos.
        /// Compara unidad y proveedor y actualiza si es necesario.
        /// Compara el stockminimo y si es diferente se crea con la fecha actual del sistema como vigencia.
        /// </summary>
        /// <param name="suministro"></param>
        /// <param name="IdProveedor"></param>
        /// <param name="IdUnidad"></param>
        /// <param name="StockMinimoSuministro"></param>
        /// <returns>Retorna el id de suministro actualizado. Si no se creo retorna -1.</returns>
        public int ActualizarSuministro(Suministro suministro, int IdProveedor, int IdUnidad, float StockMinimoSuministro) {
            Suministro sumBD;
            int id = -1;
            if (buffer.containsSuministro(suministro.SuministroId)) {
                sumBD = buffer.getSuministro(suministro.SuministroId);
            }
            else {
                sumBD = mSuministro.ObtenerSuministro(suministro.SuministroId);
            }
            try {
                sumBD.CodigoSAPSuministro = suministro.CodigoSAPSuministro;
                sumBD.DescripcionSuministro = suministro.DescripcionSuministro;
                sumBD.NombreSuministro = suministro.NombreSuministro;
                sumBD.Vigente = suministro.Vigente;
                if (sumBD.UnidadSuministro.UnidadId != IdUnidad) {
                    sumBD.UnidadSuministro = cUnidad.BuscarUnidad(IdUnidad);
                }
                if (sumBD.ProveedorSuministro.ProveedorId != IdProveedor) {
                    sumBD.ProveedorSuministro = cProveedor.BuscarProveedor(IdProveedor);
                }
                if (sumBD.getStockMinimoSuministro().CantidadStockMinimo != StockMinimoSuministro) {
                    DateTime hoy = new DateTime();
                    StockMinimo stock = cStock.CrearStockMinimo(StockMinimoSuministro, hoy.ToLocalTime());
                    sumBD.AddStockMinimoSuministro(stock);
                }

                id = mSuministro.ActualizarSuministro(sumBD);
                if (id != -1) buffer.updateSuministro(sumBD);
            }
            catch (NullReferenceException ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return id;
        }
    }
}