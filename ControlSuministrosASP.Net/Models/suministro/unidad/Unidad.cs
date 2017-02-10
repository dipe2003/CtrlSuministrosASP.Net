using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro.unidad {
    [Table("Unidades")]
    public class Unidad {
        public int UnidadId { get; set; }
        public String NombreUnidad { get; set; }

        public virtual IList<Suministro> SuministrosUnidad { get; set; }

        //	Constructores
        public Unidad() { }
        public Unidad(String NombreUnidad) { this.NombreUnidad = NombreUnidad; }

        public void addSuministroUnidad(Suministro SuministroUnidad) {
            this.SuministrosUnidad.Add(SuministroUnidad);
            if(SuministroUnidad.UnidadSuministro == null || !SuministroUnidad.UnidadSuministro.Equals(this)) {
                SuministroUnidad.UnidadSuministro = this;
            }
        }
        public void removeSuministroUnidad(Suministro SuministroUnidad) {
            this.SuministrosUnidad.Remove(SuministroUnidad);
            if(SuministroUnidad.UnidadSuministro !=null && SuministroUnidad.UnidadSuministro.Equals(this)) {
                SuministroUnidad.UnidadSuministro = this;
            }
        }
    }
}