using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StudentAppMvc.Data;
using StudentAppMvc.Repository;
using StudentAppMvc.Services;
using StudentAppMvc.Services.ValidationRule;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");;

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));;

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//Using Preflight
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                policy =>
                {
                    policy.WithOrigins("http://example.com",
                        "http://www.contoso.com",
                        "https://cors1.azurewebsites.net",
                        "https://cors3.azurewebsites.net",
                        "https://localhost:44398",
                        "https://localhost:1911");
                            //.WithMethods("PUT", "DELETE", "GET");
                });
});

builder.Services.AddControllers();
// end
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISubjectService, DefaultSubjectService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

//builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidationRule, NameValidationRule>());
builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidationRule, DescriptionValidationRule>());
builder.Services.AddSingleton<ValidationService, ValidationService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
