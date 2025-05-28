//using BaseLibrary;  
//using BaseLibrary.Entities;  
//using Microsoft.AspNetCore.Identity; // Keep this one  
//using Microsoft.EntityFrameworkCore;  
//using ServerLibrary.Data;  
//using ServerLibrary.Repositories.Implementations;  
//using ServerLibrary.Repositories.Interfaces;  
//using ServerLibrary.Services.Implementations;  
//using ServerLibrary.Services.Interfaces;  
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using Microsoft.AspNetCore.Authentication.JwtBearer;


//var builder = WebApplication.CreateBuilder(args);

//// Configure CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowBlazorClient", policy =>
//            policy.WithOrigins("https://localhost:7033")
//                  .AllowAnyHeader()
//                  .AllowAnyMethod()
//                  .AllowCredentials());
//});

//ConfigurationManager configuration = builder.Configuration; // nytt
//// Add services to the container.

//builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
//builder.Services.AddRazorPages();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Add services to the container
//builder.Services.AddControllers();
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//var connectionStringAuth = builder.Configuration.GetConnectionString("AuthConnection");
//builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionStringAuth));

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IAuthorService, AuthorService>();
//builder.Services.AddScoped<IEventService, EventService>();
//builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
//builder.Services.AddScoped<IBookRepository, BookRepository>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IBookService, BookService>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IEventRepository, EventRepository>();
//builder.Services.AddScoped<IEventService, EventService>();

//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IUserService, UserService>();

//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//builder.Services.AddAutoMapper(typeof(MappingProfiles));
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<AuthDbContext>()
//    .AddDefaultTokenProviders();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        options.SaveToken = true;
//        options.RequireHttpsMetadata = false;
//        options.TokenValidationParameters = new TokenValidationParameters()
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidAudience = configuration["JWT:ValidAudience"],
//            ValidIssuer = configuration["JWT:ValidIssuer"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
//        };
//    });

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireDigit = false;
//});

////// Configure Identity
////builder.Services.AddIdentity<User, IdentityRole<int>>()
////   .AddEntityFrameworkStores<ApplicationDbContext>()
////   .AddDefaultTokenProviders();

//var app = builder.Build();
//app.UseSwaggerUI();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseWebAssemblyDebugging();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//app.UseSwagger();
//app.UseHttpsRedirection();

//app.UseBlazorFrameworkFiles();
//app.UseStaticFiles();

//app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapRazorPages();
//app.MapControllers();
//app.MapFallbackToFile("index.html");
//app.Run();



using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Server.Services.Implementations;
using Server.Services.Interfaces;
using System;
using BaseLibrary;
using ServerLibrary.Data.AppDbCon;
using ServerLibrary.Data.AuthDbCon;
using ServerLibrary.Services.Implementations;
using ServerLibrary.Repositories.Implementations;
using ServerLibrary.Services.Interfaces;
using ServerLibrary.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; // nytt
// Add services to the container.

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString2 = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString2));

var connectionStringAuth = builder.Configuration.GetConnectionString("AuthConnection");
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionStringAuth));

builder.Services.AddIdentity<MyApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
        };
    });

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
});

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));



var app = builder.Build();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles(); 
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.UseDefaultFiles(); // ищет index.html
 
app.MapFallbackToFile("index.html");

app.Run();
