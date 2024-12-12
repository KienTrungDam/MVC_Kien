
using Microsoft.EntityFrameworkCore;
using MVC.DataAccess.Data;
using MVC.DataAccess.Repository;
using MVC.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using MVC.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//lay connectionstring in appsetting
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//tu dong generate khi add identity
builder.Services.AddRazorPages();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
//khi user truy cap vao url ko dc phep se chuyen den cac url nay
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

//Stripe lay du lieu tu appsetting truyen vao class stripesettings
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<String>();

app.UseRouting();

app.UseAuthentication(); // kiem tra tk mk co dung ko
app.UseAuthorization();  // dung thi moi duoc uy quyen(vd la admin hay user)
app.MapRazorPages(); // dieu huong den cac razor page trong folder identity

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
 