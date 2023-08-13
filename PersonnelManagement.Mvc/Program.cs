using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data.Abstract;
using PersonnelManagement.Data.Concrete;
using PersonnelManagement.Data.Concrete.Contexts;
using PersonnelManagement.Entities.Concrete;
using PersonnelManagement.Mvc.Helpers.Abstract;
using PersonnelManagement.Mvc.Helpers.Concrete;
using PersonnelManagement.Services.Abstract;
using PersonnelManagement.Services.Concrete;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<PersonnelManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PersonnelManager")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDepartmentService, DepartmentManager>();
builder.Services.AddScoped<IScheduleShiftService, ScheduleShiftManager>();
builder.Services.AddScoped<IEmployeeService, EmployeeManager>();
builder.Services.AddScoped<IPositionService, PositionManager>();
builder.Services.AddScoped<IShiftTypeService, ShiftTypeManager>();
builder.Services.AddScoped<IShiftService,ShiftManager>();
builder.Services.AddScoped<IPhoneCodesHelper, PhoneCodesHelper>();
builder.Services.AddScoped<IEnumHelper, EnumHelper>();
//builder.Services.AddScoped<IThemeColorHelper, ThemeColorHelper>();



//builder.Services.AddDbContext<PersonnelManagerContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString(@"Server=(localdb)\mssqllocaldb;Database=PersonnelManager;Integrated Security=true")));



//builder.Services.AddScoped<PersonnelManagerContext>(provider =>
//{
//    var optionsBuilder = new DbContextOptionsBuilder<PersonnelManagerContext>();
//    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("PersonnelManager"));
//    return new PersonnelManagerContext(optionsBuilder.Options);
//});


builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false; // şifrede rakam bulunsun mu?
    options.Password.RequiredLength = 5; // şifre en az kaç karakterden oluþsun?
    options.Password.RequiredUniqueChars = 0; // unique karakterlerden kaç tane olsun
    options.Password.RequireNonAlphanumeric = false;// küçük karakterler zorunlu kýlýnsýn mý?
    options.Password.RequireUppercase = false;// büyük karakterler zorunlu kýlýnsýn mý?

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
    options.User.RequireUniqueEmail = true; // tüm emailler eþsiz olsun mu?

}).AddEntityFrameworkStores<PersonnelManagerContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/User/Login");
    options.Cookie = new CookieBuilder
    {
        Name = "PersonnelManagementCookie",
        HttpOnly = true,//kullanıcının js ile bizim cookie bilgilerimizi görmesini engelliyoruz
        SameSite = SameSiteMode.Strict, //cookie bilgileri sadece kendi sitemizden geldiğinde işlensin
        SecurePolicy = CookieSecurePolicy.SameAsRequest //always olmalı 
    };
    options.SlidingExpiration = true; //kullanıcı sitemize girdiğinde süre tanınıyor
    options.ExpireTimeSpan = System.TimeSpan.FromMinutes(15); // 15 dakikatekrar giriş gerekmeyecek
    options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied"); //yetkisiz erişim
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            


app.Run();
