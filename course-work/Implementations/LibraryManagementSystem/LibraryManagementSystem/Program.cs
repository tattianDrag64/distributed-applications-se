using BaseLibrary;  
using BaseLibrary.Entities;  
using Microsoft.AspNetCore.Identity; // Keep this one  
using Microsoft.EntityFrameworkCore;  
using ServerLibrary.Data;  
using ServerLibrary.Repositories.Implementations;  
using ServerLibrary.Repositories.Interfaces;  
using ServerLibrary.Services.Implementations;  
using ServerLibrary.Services.Interfaces;  
// Remove the duplicate below  
// using Microsoft.AspNetCore.Identity;  
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
        policy.WithOrigins("https://localhost:7033")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
    );
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<AuthorRepository>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
// Регистрация на репозиторита и услугите  
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddIdentity<User, IdentityRole<int>>()
  .AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowBlazorClient");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
