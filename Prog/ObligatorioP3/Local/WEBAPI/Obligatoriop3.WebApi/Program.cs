using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ObligatorioP3.AccesoDatos.EF;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Articulos;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.MovimientosStock;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Pedidos;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Usuarios;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Articulos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.TiposDeMovimiento;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Usuarios;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.MovimientosStock;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System.Reflection;
using System.Text;
using Microsoft.OpenApi.Models;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using Libreria.AccesoDatos.EF;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ObligatorioP3.Api.xml");
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Introduce 'Bearer' [espacio] y luego tu token JWT en el campo de texto.",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new string[] {}
        }
    });
    options.IncludeXmlComments(xmlPath);

    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ObligatorioP3API",
        Version = "v1",
        Description = "Api de manejo de movimientos de stock",
    });
});
//Agregamos DBContext
builder.Services.AddDbContext<ObligatorioP3Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionOBLP3")));


//Inyectamos CU de tipo de movimiento
builder.Services.AddScoped<IAltaMovimiento, AltaTipoDeMovimiento>();
builder.Services.AddScoped<IBorrarTipoDeMovimiento, BorrarTipoDeMovimiento>();
builder.Services.AddScoped<IEditarTipoDeMovimiento, EditarTipoDeMovimiento>();
builder.Services.AddScoped<IGetTipoDeMovimiento, GetTipoDeMovimiento>();

//Inyectamos CU de movimientos
builder.Services.AddScoped<IAltaMovimientoStock, AltaMovimiento>();
builder.Services.AddScoped<IListarResumenMovimientos, ListarResumenMovimientos>();
builder.Services.AddScoped<IFiltrarMovimientos, FiltrarMovimientos>();

//Inyecto CU de articulo (Obligatorio 1)
builder.Services.AddScoped<IGetAllArticulos, GetAllArticulos>();
builder.Services.AddScoped<IGetArticulosConMovimientosSegunFechas,GetArticulosConMovimientosSegunFechas>();

//Inyecto CU de pedidos (Obligatorio 1)
builder.Services.AddScoped<IRepositorioPedido, RepositorioPedidoEF>();
builder.Services.AddScoped<IGetAllPedidosAnulados, GetAllPedidosAnulados>();

//Inyectamos CU de usuario
builder.Services.AddScoped<ILoginAPI, LoginAPI>();

//Inyectamos repositorios
builder.Services.AddScoped<IRepositorioMovimiento, RepositorioMovimientoEF>();
builder.Services.AddScoped<IRepositorioTipoDeMovimientoEF,RepositorioTipoDeMovimientoEF>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioEF>();
builder.Services.AddScoped<IRepositorioArticulo, RepositorioArticuloEF>();
builder.Services.AddScoped<IRepositorioParametro, RepositorioParametroEF>();
//Inyectamos los servicios necesarios para la autenticacion
var claveDificil = "GastonSecreto_MatiasSecreto_GastonSecreto_MatiasSecreto_GastonSecreto_MatiasSecreto";
var claveDificilEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));

#region Registro de servicios JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        //Definir las verificaciones a realizar
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = claveDificilEncriptada
    };

});
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI();
app.UseSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
