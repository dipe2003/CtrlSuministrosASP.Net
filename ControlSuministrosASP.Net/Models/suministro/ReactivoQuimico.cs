using ControlSuministrosASP.Net.Models.proveedor;
using ControlSuministrosASP.Net.Models.suministro.unidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlSuministrosASP.Net.Models.suministro {
    public class ReactivoQuimico : Suministro {
        public ReactivoQuimico() : base() { }
        public ReactivoQuimico(String NombreSuministro, String DescripcionSuministro, String CodigoSAPSuministro,
                Unidad UnidadSuministro, Proveedor ProveedorSuministro) : base(NombreSuministro, DescripcionSuministro, CodigoSAPSuministro,
                UnidadSuministro, ProveedorSuministro) { }
    }
}
