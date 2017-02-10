using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro {
    public class BufferSuministro {
        private static ManejadorSuministro mSuministro = ManejadorSuministro.getInstancia();
        private static BufferSuministro Instancia;
        private static IDictionary<int, Suministro> DicSuministros = new Dictionary<int, Suministro>();
        private BufferSuministro() {
            DicSuministros = mSuministro.ListarDicSuministrosFull();
        }
        public static BufferSuministro getInstancia() {
            if (Instancia == null) Instancia = new BufferSuministro();
            return Instancia;
        }

        public Suministro getSuministro(int IdSuministro) {
            return DicSuministros[IdSuministro];
        }

        public void putSuministro(Suministro suministro) {
            DicSuministros.Add(suministro.SuministroId, suministro);
        }

        public void removeSuministro(Suministro suministro) {
            DicSuministros.Remove(suministro.SuministroId);
        }

        public void updateSuministro(Suministro suministro) {
            DicSuministros.Remove(suministro.SuministroId);
            DicSuministros.Add(suministro.SuministroId, suministro);
        }

        public bool containsSuministro(int IdSuministro) {
            return DicSuministros.ContainsKey(IdSuministro);
        }

        public int bufferSize() {
            return DicSuministros.Count;
        }

        public IList<Suministro> getListaSuministros(bool Vigente) {
            IList<Suministro> lista = new List<Suministro>();
            foreach (var suministro in DicSuministros.Values) {
                if (Vigente) {
                    if (suministro.Vigente) lista.Add(suministro);
                }
                else {
                    lista.Add(suministro);
                }
            }
            return lista;
        }

        public IDictionary<string, int> getDicSuministrosPorProveedor(int IdProveedor) {
            IDictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var suministro in DicSuministros.Values) {
                if (suministro.ProveedorSuministro.ProveedorId == IdProveedor) dic.Add(suministro.ProveedorSuministro.NombreProveedor, suministro.ProveedorSuministro.ProveedorId);
            }

            return new SortedDictionary<string, int>(dic);
        }

        public IDictionary<string, int> getDicNombreSuministros(bool Vigente) {
            IDictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var suministro in DicSuministros.Values) {
                if (suministro.Vigente) dic.Add(suministro.NombreSuministro + " (" + suministro.ProveedorSuministro.NombreProveedor + ") ", suministro.SuministroId);
            }
            return new SortedDictionary<string, int>(dic);
        }

        public IDictionary<int, Suministro> getDicSuministros() {
            return new SortedDictionary<int, Suministro>(DicSuministros);
        }
    }
}