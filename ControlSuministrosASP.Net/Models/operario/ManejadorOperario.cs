using ControlSuministrosASP.Net.Models.ContextoBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.operario {
    public class ManejadorOperario {
        private static SuministrosDBContext db = SuministrosDBContext.getInstancia();
        private static ManejadorOperario Instancia;
        private ManejadorOperario() { }
        public static ManejadorOperario getInstancia() {
            if (Instancia == null) Instancia = new ManejadorOperario();
            return Instancia;
        }
        public int CrearOperario(Operario operario) {
            try {
                db.Operarios.Add(operario);
                db.SaveChanges();
                return operario.OperarioId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int ActualizarOperario(Operario operario) {
            try {
                db.Entry(operario).State = EntityState.Modified;
                db.SaveChanges();
                return operario.OperarioId;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public int BorrarOperario(int IdOperario) {
            try {
                Operario operario = db.Operarios.Find(IdOperario);
                db.Operarios.Remove(operario);
                db.SaveChanges();
                return IdOperario;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        public Operario ObtenerOperario(int IdOperario) {
            try {
                Operario operario = db.Operarios.Find(IdOperario);
                return operario;
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        public IList<Operario> ListarOperarios() {
            IList<Operario> operarios = new List<Operario>();
            try {
                operarios = db.Operarios.ToList<Operario>();
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return operarios;
        }
    }
}