using Microsoft.EntityFrameworkCore;
using PaparaDotnetBootcampApi.Business.Services.Abstract;
using PaparaDotnetBootcampApi.Business.Services.Concrete;
using PaparaDotnetBootcampApi.DataAccess.Context;
using PaparaDotnetBootcampApi.DataAccess.Repositories.GenericRepository;
using PaparaDotnetBootcampApi.DataAccess.UnitOfWork;
using PaparaDotnetBootcampApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; // Opsiyonel, null deðerleri ignore etmek için
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICardService, CardService>();


builder.Services.AddDbContext<PatikaCohortContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
