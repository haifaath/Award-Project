using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IAUResearcherAwardForm.Pages
{
    /// <summary>
    /// Program class that configures and runs the web application
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Add anti-forgery token validation
            builder.Services.AddAntiforgery(options => 
            {
                options.HeaderName = "X-CSRF-TOKEN";
                options.Cookie.Name = "CSRF-TOKEN";
                options.FormFieldName = "__RequestVerificationToken";
            });

            // Configure logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
