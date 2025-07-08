using API_Shop_Online.Data;
using API_Shop_Online.Mapper;
using API_Shop_Online.Services.Customers;
using API_Shop_Online.Services.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

//Add services
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IStoreService, StoreService>();

// Add automapper
builder.Services.AddAutoMapper(typeof(StoreMapper));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors
builder.Services.Configure<string[]>(builder.Configuration.GetSection("AddCors"));

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var addCors = builder.Configuration.GetSection("AddCors").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
                .WithOrigins(addCors)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
