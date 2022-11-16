using BlogApp.MVCUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMVCServices(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<UserClaimsMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
