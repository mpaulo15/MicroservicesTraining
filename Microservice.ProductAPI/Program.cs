using AutoMapper;
using Microservice.ProductAPI.Configs;
using Microservice.ProductAPI.Models.Contexts;
using Microservice.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- CÓDIGO A SER ADICIONADO ABAIXO ---
// 1. Obter a string de conexão do arquivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Adicionar o DbContext com o provedor SQL Server
builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseSqlServer(
        connectionString,
        options => options.EnableRetryOnFailure() // Opcional: para resiliência de conexão
    )
);

//// 2. Adicionar o DbContext com o provedor MySQL
//builder.Services.AddDbContext<MySQLContext>(options =>
//    options.UseMySql(
//        connectionString,
//        ServerVersion.AutoDetect(connectionString) // Pomelo detecta a versão do MySQL
//                                                   //options => options.EnableRetryOnFailure() // Opcional: para resiliência de conexão
//    )
//);

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
