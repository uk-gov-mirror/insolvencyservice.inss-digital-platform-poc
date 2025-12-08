using GovUk.Frontend.AspNetCore;

namespace INSS.Forms.Analytics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient();

            builder.Services.AddSession();

            builder.Services.AddAuthenticationConfiguration(builder.Configuration);

            builder.Services.AddAnalyticsConfiguration(builder.Configuration);

            builder.Services.AddGovUkFrontend(options =>
            {
                options.Rebrand = true;
            });

            var app = builder.Build();

            app.UseGovUkFrontend();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
