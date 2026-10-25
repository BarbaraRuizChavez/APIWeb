using Microsoft.EntityFrameworkCore;
using APIWeb.Data;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextoBD>(options => options.UseMySQL(builder.Configuration.GetConnectionString("ConexionMySQL")));



// Add services to the container.
builder.Services.AddControllers();
// Indica que vamos a trabajar con swagger  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "cliente")),
    RequestPath = "/cliente"
});
app.UseCors("PermitirTodo");
app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.Run();