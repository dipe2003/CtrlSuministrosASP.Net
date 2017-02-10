using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ControlSuministrosASP.Net.Models.suministro;
using ControlSuministrosASP.Net.Models.operario;
using ControlSuministrosASP.Net.Models.operario.permiso;
using ControlSuministrosASP.Net.Models.proveedor;
using ControlSuministrosASP.Net.Models.suministro.lote;
using ControlSuministrosASP.Net.Models.suministro.stockminimo;
using ControlSuministrosASP.Net.Models.suministro.unidad;
using ControlSuministrosASP.Net.Models.notificaciones;

namespace ControlSuministrosASP.Net.Models.ContextoBD {
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class SuministrosDBContext : DbContext {

        public SuministrosDBContext() : base("name=mysqlCtrlSuministrosDBconn") {
            Database.SetInitializer<SuministrosDBContext>(new CreateDatabaseIfNotExists<SuministrosDBContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SuministrosDBContext, ControlSuministrosASP.Net.Migrations.Configuration>("mysqlCtrlSuministrosDBconn"));
        }
        private static SuministrosDBContext Instancia;
        public static SuministrosDBContext getInstancia() {
            if (Instancia == null) Instancia = new SuministrosDBContext();
            return Instancia;
        }

        public DbSet<Suministro> Suministros { get; set; }
        public DbSet<Operario> Operarios { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<Salida> Salidas { get; set; }
        public DbSet<StockMinimo> StocksMinimos { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
    }
}