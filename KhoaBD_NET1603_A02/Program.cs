using Application.Configuration;
using Domain.Models;
using FluentValidation.AspNetCore;
using RazorPage.Configuration;
using System.Reflection;

namespace KhoaBD_NET1603_A02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Configuration.BindingAppSettings();

            builder.Services.AddServices();

            builder.Services.RegisterMapster();

            builder.Services.AddRazorPages();

            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(Admin))));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}