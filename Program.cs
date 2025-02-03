using Microsoft.EntityFrameworkCore;
using desafioBoiSaude.DataContext;
using desafioBoiSaude.Service.ProdutoService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProdutoInterface, ProdutoService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{   
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("ProdutosAPI", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("ProdutosAPI");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
