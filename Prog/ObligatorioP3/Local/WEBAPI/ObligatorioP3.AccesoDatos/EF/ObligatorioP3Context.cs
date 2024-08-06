using Libreria.LogicaNegocio.Entidades.ParametrosConfigurables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Entidades.AssosiationClasses;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.AccesoDatos.EF
{
    public class ObligatorioP3Context:DbContext
    {
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoComun> PedidosComunes { get; set; }
        public DbSet<PedidoExpress> PedidosExpress { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<TipoDeMovimiento>TiposDeMovimiento { get; set; }
        public DbSet<MovimientoStock> MovimientosDeStock { get; set; }

        public ObligatorioP3Context() { }
        public ObligatorioP3Context(DbContextOptions<ObligatorioP3Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pedido>().OwnsMany(pedido => pedido.Lineas);

            #region ConfiguracionUsuarios
            //Convertimos el Email para que pueda ser un indice y unique
            var emailConverter = new ValueConverter<Email,string>(e => e.ValorEmail, e=> new Email(e));
            modelBuilder.Entity<Usuario>().Property(e => e.Email).HasConversion(emailConverter);
            modelBuilder.Entity<Usuario>().HasIndex(d => d.Email).IsUnique();
            #endregion

            #region ConfiguracionArticulos
            //Colocamos como indice y unique codigo y nombre de cada articulo
            modelBuilder.Entity<Articulo>().HasIndex(d => d.Codigo).IsUnique();
            modelBuilder.Entity<Articulo>().HasIndex(d => d.Nombre).IsUnique();
            #endregion

            #region ConfiguracionClientes
            modelBuilder.Entity<Cliente>().HasIndex(d => d.RUT).IsUnique();
            #endregion
        }
    }
}
