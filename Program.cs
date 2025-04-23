using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Text;
using THUC_HANH_3.Entities;
using THUC_HANH_3.Repository;
using THUC_HANH_3.Service.Category;
using THUC_HANH_3.Service.User;
using Todoapp.Data;
using Todoapp.Service;
using Todoapp.Service.Category;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //builder.WebHost.ConfigureKestrel(options =>
        //{
        //    options.ListenAnyIP(5000); // HTTP
        //    options.ListenAnyIP(5001, listenOptions =>
        //    {
        //        listenOptions.UseHttps(); // HTTPS
        //    });
        //});

        // Add services 
        builder.Services.AddScoped<ITaskService, TaskService>();
        //??ng kí DI Cho Service
        builder.Services.AddScoped<IcategoryService, CategoryService>();
        // ADD Generic REPONSITORY 
        builder.Services.AddScoped(typeof(IResponsitory<>), typeof(Repository<>));
        //Đăng kí DI cho User Service
        builder.Services.AddScoped<IUserService,UserServicecs>();
        //PASSWORD
        builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

        builder.Services.AddHttpContextAccessor();

        



        //AutoMapper => H? th?ng có th? g?i và s? d?ng.
        builder.Services.AddAutoMapper(typeof(Program));

        //JWT config
        var jwtSetting = builder.Configuration.GetSection("JwtSettings");  //Goi config cua jwt
        var key = Encoding.ASCII.GetBytes(jwtSetting["Secret"]);

        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSetting["Issuer"],
                ValidAudience = jwtSetting["Audience"],
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key)

            };
        });

        builder.Services.AddAuthorization();

        //Cau hinh swagger UI DE NO Nhan duoc JWT
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "ToDoApp",
                Version = "v1"
            }
            );
            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Nhap 'Bearer {Token}'",
            });

            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
                }
            });
        });








        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();



        // Connect Db
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Dan phia duoi DefaultConnection
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var app = builder.Build();
        //MIDDLEWARE 
        app.UseMiddleware<AuthenticationMiddleware>();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        // Dan phia duoi app.Environment.IsDevelopment()
        app.UseCors("AllowAll");

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

        
    }
}