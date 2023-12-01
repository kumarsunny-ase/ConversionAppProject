using ConvertionToWordsApp.Data;
using ConvertionToWordsApp.Repository.Implentation;
using ConvertionToWordsApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace ConvertionToWordsApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        //builder.Services.AddEndpointsApiExplorer();;
        builder.Services.AddControllersWithViews();
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ConvertionToWordsApp",
                Version = "v1",
                Description = "Description of your API"
            });
        });

        builder.Services.AddScoped<IinputRepository, InputRepository>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (!app.Environment.IsDevelopment())
        //{
        //    app.UseExceptionHandler("/Home/Error");
        //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //    app.UseHsts();
        //}
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConvertionToWordsApp v1");
            });
        }

        app.UseHttpsRedirection();
        app.UseCors(options =>
        {
            options.AllowAnyHeader();
            options.AllowAnyMethod();
            options.AllowAnyOrigin();
        });
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}

