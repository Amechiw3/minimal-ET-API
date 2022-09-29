using ET_ASP;
using ET_ASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ET_ASP.Routes;

var builder = WebApplication.CreateBuilder(args);

//Crear linea de conexion en memoria
//builder.Services.AddDbContext<DataModel>(db => db.UseInMemoryDatabase("TareasDB"));

builder.Services.AddSqlServer<DataModel>(builder.Configuration.GetConnectionString("cntareas"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", ([FromServices] DataModel dbcontext) => {
    dbcontext.Database.EnsureCreated();
    //return Results.Ok($"Base de datos en Memoria: {dbcontext.Database.IsInMemory()}");
    return Results.Ok($"Base de datos en Memoria: {dbcontext.Database.IsSqlServer()}");
});

app.MaptaskEndpoints();
app.MapcategoryEndpoints();

app.Run();
