using Application.Interfaces;
using Application.UseCase;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Query;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<RetailContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Retail")));

builder.Services.AddScoped<ICategoryQuery, CategoryQuery>();
builder.Services.AddScoped<ICategoryCommands, CategoryCommands>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();

builder.Services.AddScoped<IProductQuery, ProductQuery>();
builder.Services.AddScoped<IProductCommands, ProductCommands>();
builder.Services.AddScoped<IProductServices, ProductServices>();

builder.Services.AddScoped<ISaleQuery, SaleQuery>();
builder.Services.AddScoped<ISaleCommands, SaleCommands>();
builder.Services.AddScoped<ISaleServices, SaleServices>();

builder.Services.AddScoped<ISaleProductQuery, SaleProductQuery>();
builder.Services.AddScoped<ISaleProductCommands, SaleProductCommands>();
builder.Services.AddScoped<ISaleProductServices, SaleProductServices>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

