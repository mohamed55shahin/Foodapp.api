using Autofac.Extensions.DependencyInjection;
using Autofac;
using FoodApp.Api.Data;
using Microsoft.EntityFrameworkCore;
using FoodApp.Api.config;
using Autofac.Core;
using FoodApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FoodApp.Api.ViewModle.authVIewModel;
using FoodApp.Api.FoodApp.Service;
using AutoMapper;
using FoodApp.Api.FoodApp.Service.RecipeService;
using FoodApp.Api.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<CoustemAurhorizeFilter>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Context>();
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IRecpieService , RecipeService>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule(new autofac());
});


var app = builder.Build();

AutomapperService.mapper = app.Services.GetService<IMapper>(); 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandling>();
app.MapControllers();

app.Run();
