using ControlSuministrosASP.Net.Models.suministro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.proveedor {
    [Table("Proveedores")]
    public class Proveedor {
        public int ProveedorId { get; set; }
        public string NombreProveedor { get; set; }
        public string ContactoProveedor { get; set; }

        public virtual IList<Suministro> SuministrosProveedor { get; set; }

        //	Constructores
        public Proveedor() { }
        public Proveedor(string NombreProveedor, string ContactoProveedor) {
            this.NombreProveedor = NombreProveedor;
            this.ContactoProveedor = ContactoProveedor;
            this.SuministrosProveedor = new List<Suministro>();
        }

        //	SuministrosProveedor
        public void addSuministroProveedor(Suministro SuministroProveedor) {
            this.SuministrosProveedor.Add(SuministroProveedor);
            if(SuministroProveedor.ProveedorSuministro == null || !SuministroProveedor.ProveedorSuministro.Equals(this)) {
                SuministroProveedor.ProveedorSuministro = this;
            }
        }
        public void removeSuministroProveedor(Suministro SuministroProveedor) {
            this.SuministrosProveedor.Remove(SuministroProveedor);
            if(SuministroProveedor.ProveedorSuministro != null && SuministroProveedor.ProveedorSuministro.Equals(this)) {
                SuministroProveedor.ProveedorSuministro = null;
            }
        }
        public bool esProveedorSuministro(int IdSuministro) {
            for (int i = 0; i < SuministrosProveedor.Count; i++) {
                if (SuministrosProveedor.ElementAt(i).SuministroId == IdSuministro) {
                    return true;
                }
            }
            return false;
        }
    }
}