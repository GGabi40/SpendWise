using System.Reflection;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SpendWise.Core.Interfaces;
using SpendWise.Infrastructure.Repositories;
using SpendWise.Web.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Configuration.AddConfiguration(configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();

builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<TransactionService>();

builder.Services.AddScoped<IUserRepository, UserRepository>(); // registra implementación
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<NoteService>();

builder.Services.Configure<CustomAuthenticationService.AutenticacionServiceOptions>(
    builder.Configuration.GetSection(CustomAuthenticationService.AutenticacionServiceOptions.SectionName)
);

var connection = new SqliteConnection("Data Source=WebApiSpendWise.db");
connection.Open();

using (var command = connection.CreateCommand())
{
    command.CommandText = "PRAGMA journal_mode = DELETE;";
    command.ExecuteNonQuery();
}

builder.Services.AddDbContext<ApplicationDbContext>(DbContextOptions => DbContextOptions.UseSqlite(connection));

#region Swagger custom token config
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("SpendWiseApiBearerAuth", new OpenApiSecurityScheme() // Esto permitw usar swagger con el token.
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Acá pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "SpendWiseApiBearerAuth" } //Tiene que coincidir con el id seteado arriba en la definición
                }, new List<string>() }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    setupAction.IncludeXmlComments(xmlPath);

});

var secretKey = builder.Configuration["AutenticationService:SecretForKey"];
Console.WriteLine($"Clave JWT leída: {secretKey ?? "NULL"}");
if(string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("La clave secreta JWT no está configurada");
}
builder.Services.AddAuthentication("Bearer") //"Bearer" es el tipo de auntenticación que tenemos que elegir después en PostMan para pasarle el token
    .AddJwtBearer(options => //Acá definimos la configuración de la autenticación. le decimos qué cosas queremos comprobar. La fecha de expiración se valida por defecto.
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AutenticacionService:Issuer"],
            ValidAudience = builder.Configuration["AutenticacionService:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
        };
    }
);
# endregion


/* App */
var app = builder.Build();

// Swagger para custom token
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();