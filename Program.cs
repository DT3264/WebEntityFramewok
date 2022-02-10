using ContosoPizza.Services;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the PizzaContext
// builder.Services.AddSqlite<PizzaContext>("Data Source=ContosoPizza.db");
var conn = "server=localhost;port=3306;database=PizzaDB;uid=root;password=0000";

builder.Services.AddEntityFrameworkMySql().AddDbContext<PizzaContext>(
    o => o.UseMySQL(conn)
);
// Add the PromotionsContext

builder.Services.AddScoped<PizzaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add the CreateDbInNotExists method call
app.CreateDbIfNotExists();

app.Run();
