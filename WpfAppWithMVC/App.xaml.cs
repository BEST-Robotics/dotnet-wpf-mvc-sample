using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;

namespace WpfAppWithMVC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            SetupHosting();
            base.OnStartup(e);
        }

        private static void SetupHosting()
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                ContentRootPath = Directory.GetCurrentDirectory()
            });

            builder.WebHost.UseUrls("http://*:9268;");
            builder.Services.AddControllersWithViews();
            builder.Services.AddMvc().AddRazorRuntimeCompilation();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var staticFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var opts = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFileDirectory)
            };
            app.UseStaticFiles(opts);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Start();
        }

    }

}
