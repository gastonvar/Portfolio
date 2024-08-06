using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligatorioP3.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RUT = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Direccion_Calle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion_Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion_Distancia = table.Column<double>(type: "float", nullable: false),
                    Direccion_Numero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parametros",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametros", x => x.Nombre);
                });

            migrationBuilder.CreateTable(
                name: "TiposDeMovimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Aumento = table.Column<bool>(type: "bit", nullable: false),
                    Coeficiente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDeMovimiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasena_ContrasenaEncriptada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contrasena_ContrasenaNoEncriptada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCompleto_Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCompleto_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Anulado = table.Column<bool>(type: "bit", nullable: false),
                    Recargo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    PlazoExpress = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosDeStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    IdTipo = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosDeStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosDeStock_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosDeStock_TiposDeMovimiento_IdTipo",
                        column: x => x.IdTipo,
                        principalTable: "TiposDeMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosDeStock_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticulosPedido",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    Unidades = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticulosPedido", x => new { x.PedidoId, x.Id });
                    table.ForeignKey(
                        name: "FK_ArticulosPedido_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticulosPedido_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_Codigo",
                table: "Articulos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_Nombre",
                table: "Articulos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticulosPedido_ArticuloId",
                table: "ArticulosPedido",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_RUT",
                table: "Clientes",
                column: "RUT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosDeStock_IdArticulo",
                table: "MovimientosDeStock",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosDeStock_IdTipo",
                table: "MovimientosDeStock",
                column: "IdTipo");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosDeStock_IdUsuario",
                table: "MovimientosDeStock",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposDeMovimiento_Nombre",
                table: "TiposDeMovimiento",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticulosPedido");

            migrationBuilder.DropTable(
                name: "MovimientosDeStock");

            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "TiposDeMovimiento");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
