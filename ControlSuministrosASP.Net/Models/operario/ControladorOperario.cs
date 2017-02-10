using ControlSuministrosASP.Net.Models.operario.permiso;
using ControlSuministrosASP.Net.Models.operario.seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.operario {
    public class ControladorOperario {
        private static ManejadorOperario mOperario = ManejadorOperario.getInstancia();
        private static ControladorSeguridad cSeg = new ControladorSeguridad();
        private static ControladorPermiso cPermiso = ControladorPermiso.getInstancia();

        private static ControladorOperario Instancia;
        private ControladorOperario() { }
        public static ControladorOperario getInstancia() {
            if (Instancia == null) Instancia = new ControladorOperario();
            return Instancia;
        }

        /// <summary>
        /// Crea un operario en la base de datos.
        /// </summary>
        /// <param name="IdOperario"></param>
        /// <param name="NombreOperario"></param>
        /// <param name="ApellidoOperario"></param>
        /// <param name="PasswordOperario"></param>
        /// <param name="CorreoOperario"></param>
        /// <returns>Retorna el id del operario creado. Si no se creo retorna -1.</returns>
        public int CrearOperario(int IdOperario, string NombreOperario, string ApellidoOperario, string PasswordOperario, string CorreoOperario) {
            string hashedPassword = ControladorSeguridad.getPasswordSeguro(PasswordOperario);
            Operario operario = new Operario(IdOperario, NombreOperario, ApellidoOperario, CorreoOperario, hashedPassword);
            return mOperario.CrearOperario(operario);
        }

        ///<summary
        ///Busca un operario en la base de datos.
        ///<param name="IdOperario">Id unido de operario</param>
        ///<returns>El operario buscado por su id. Null si no existe</returns>
        public Operario BuscarOperario(int IdOperario) {
            return mOperario.ObtenerOperario(IdOperario);
        }

        /// <summary>
        /// Devuelve una lista con todos los operarios registrados en la base de datos.
        /// Si no hay operarios registrados devuelve una lista vacia.
        /// </summary>
        /// <returns></returns>
        public IList<Operario> ListarOperarios() {
            return mOperario.ListarOperarios();
        }

        /// <summary>
        /// Comprueba si el password para el operario especificado es correcto.
        /// El operario debe existir en la base de datos.
        /// </summary>
        /// <param name="IdOperario"></param>
        /// <param name="Password"></param>
        /// <returns>True si es valido. False si no es valido</returns>
        public bool ValidarOperario(int IdOperario, string Password) {
            try {
                Operario operario = mOperario.ObtenerOperario(IdOperario);
                return ControladorSeguridad.ValidatePassword(Password, operario.PasswordOperario);
            }
            catch (NullReferenceException ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Agrega el permiso indicado al usuario especificado por su id.
        /// </summary>
        /// <param name="IdOperario"></param>
        /// <param name="PermisoOperario"></param>
        /// <returns>retorna el id del operario si se agergo, sino retorna -1.</returns>
        public int AgregarPermiso(int IdOperario, Permiso PermisoOperario) {
            try {
                Operario operario = mOperario.ObtenerOperario(IdOperario);
                operario.PermisoOperario = PermisoOperario;
                return mOperario.ActualizarOperario(operario);
            }
            catch (NullReferenceException ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Actualiza los datos de un usuario especificado por su id.
        /// </summary>
        /// <param name="IdOperario"></param>
        /// <param name="NombreOperario"></param>
        /// <param name="ApellidoOperario"></param>
        /// <param name="CorreoOperario"></param>
        /// <returns>Retorna el id del operario. Retorna -1 si no se pudo actualizar.</returns>
        public int ModificarDatosOperario(int IdOperario, string NombreOperario, string ApellidoOperario, string CorreoOperario, bool RecibeAlertas) {
            Operario operario = mOperario.ObtenerOperario(IdOperario);
            operario.NombreOperario = NombreOperario;
            operario.ApellidoOperario = ApellidoOperario;
            operario.CorreoOperario = CorreoOperario;
            operario.RecibeAlertas = RecibeAlertas;
            return mOperario.ActualizarOperario(operario);
        }

        /// <summary>
        /// Modifica el password del usuario.
        /// </summary>
        /// <param name="IdOperario"></param>
        /// <param name="PasswordNuevo"></param>
        /// <returns>Retorna el id del operario se se actualizo. Retorna -1 si no se pudo actualizar.</returns>
        public int ModificarPassword(int IdOperario, string PasswordNuevo) {
            try {
                Operario operario = mOperario.ObtenerOperario(IdOperario);
                string pass = ControladorSeguridad.getPasswordSeguro(PasswordNuevo);
                operario.PasswordOperario = pass;
                return mOperario.ActualizarOperario(operario);
            }
            catch (ArgumentException ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Devuelve un diccionario con los nombres de los operarios y sus id.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, int> GetMapNombresOperarios() {
            IDictionary<string, int> nombres = new Dictionary<string, int>();
            IList<Operario> operarios = mOperario.ListarOperarios();
            for (int i = 0; i < operarios.Count; i++) {
                nombres.Add(operarios.ElementAt(i).NombreOperario + " " + operarios.ElementAt(i).ApellidoOperario, operarios.ElementAt(i).OperarioId);
            }
            return nombres;
        }
    }
}