using ControlSuministrosASP.Net.Models.ContextoBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro {
    public class ManejadorSuministro {
        private static SuministrosDBContext db = SuministrosDBContext.getInstancia();

        private static ManejadorSuministro Instancia;
        private ManejadorSuministro() { }
        public static ManejadorSuministro getInstancia() {
            if (Instancia == null) Instancia = new ManejadorSuministro();
            return Instancia;
        }

        public int CrearSuministro(Suministro suministro) {
            try {
                db.Suministros.Add(suministro);
                db.SaveChanges();
                return suministro.SuministroId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int ActualizarSuministro(Suministro suministro) {
            try {
                db.Entry(suministro).State = EntityState.Modified;
                db.SaveChanges();
                return suministro.SuministroId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int BorrarSuministro(int IdSuministro) {
            try {
                Suministro suministro = db.Suministros.Find(IdSuministro);
                db.Suministros.Remove(suministro);
                db.SaveChanges();
                return IdSuministro;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public Suministro ObtenerSuministro(int IdSuministro) {
            try {
                Suministro suministro = db.Suministros.Find(IdSuministro);
                return suministro;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public IList<Suministro> ListarSuministros() {
            IList<Suministro> suministroes = new List<Suministro>();
            try {
                suministroes = db.Suministros.ToList<Suministro>();
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return suministroes;
        }

        public IDictionary<string, int> ListarDicSuministros(bool Vigente) {
            IDictionary<string, int> dic = new Dictionary<string, int>();
            try {
                IList<Suministro> suministros = db.Suministros.ToList<Suministro>();
                for (int i = 0; i < suministros.Count; i++) {
                    if (Vigente) {
                        if (suministros.ElementAt(i).Vigente) dic.Add(suministros.ElementAt(i).NombreSuministro + "(" + suministros.ElementAt(i).ProveedorSuministro.NombreProveedor + ")", suministros.ElementAt(i).SuministroId);
                    }
                    else {
                        dic.Add(suministros.ElementAt(i).NombreSuministro + "(" + suministros.ElementAt(i).ProveedorSuministro.NombreProveedor + ")", suministros.ElementAt(i).SuministroId);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dic;
        }


        public IDictionary<int, Suministro> ListarDicSuministrosFull() {
            IDictionary<int, Suministro> dic = new Dictionary<int, Suministro>();
            try {
                IList<Suministro> suministros = db.Suministros.ToList<Suministro>();
                for (int i = 0; i < suministros.Count; i++) {
                    dic.Add(suministros.ElementAt(i).SuministroId, suministros.ElementAt(i));
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dic;
        }

        public IList<Suministro> ListarSuministros(bool Vigencia) {
            IList<Suministro> suministros = new List<Suministro>();
            var query = from sum in db.Suministros where sum.Vigente == Vigencia select sum;
            try {
                foreach (var sum in query) {
                    suministros.Add(sum);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return suministros;
        }


        public IDictionary<string, int> ListarSuministrosProveedor(int IdProveedor) {
            IDictionary<string, int> dic = new Dictionary<string, int>();
            if (IdProveedor > 0) {
                var query = from sum in db.Suministros join prov in db.Proveedores on sum.ProveedorSuministro equals prov select sum;

                foreach (var sum in query) {
                    dic.Add(sum.NombreSuministro, sum.SuministroId);
                }
            }
            return dic;
        }
    }
}