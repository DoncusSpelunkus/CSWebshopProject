
using System.Text;
using AutoMapper;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;
using PetShop.Infastructure;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetShop.Application;
using PetShop.Application.Interfaces;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions()
    {
      Path = "/", HttpOnly = false, IsEssential = true, //<- there
      Expires = DateTime.Now.AddMonths(1), 
    };
var config = new MapperConfiguration(conf =>
{
    conf.CreateMap<ProdDTO, Product>();
    conf.CreateMap<ratingDTO, Rating>();
    conf.CreateMap<SpecDTO, Specs>();
    conf.CreateMap<MainCatDTO, MainCategory>();
    conf.CreateMap<SubCatDTO, SubCategory>();
    conf.CreateMap<SpecDescDTO, SpecsDescription>();
    conf.CreateMap<BrandDto, Brand>();
    conf.CreateMap<UserDTO, User>();
    conf.CreateMap<UserLoginDTO, User>();
});

var mapper = config.CreateMapper();


builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<DBContext>(options => options.UseSqlite("Data source=db.db"));
builder.Services.AddScoped<IProductService , ProductService>();
builder.Services.AddScoped<ISpecService , SpecService>();
builder.Services.AddScoped<ICatService , CatService>();
builder.Services.AddScoped<IBrandService , BrandService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICatRepo, CatRepo>();
builder.Services.AddScoped<IShopRepo, ShopRepo>();
builder.Services.AddScoped<ISpecRepo , SpecsRepo>();
builder.Services.AddScoped<IBrandRepo, BrandRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();


builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

PetShop.Application.DependencyResolver.DependencyResolverService.RegisterApplicationLayer(builder.Services);
PetShop.Infastructure.DependencyResolver.DependencyResolverService.RegisterInfrastructureLayer(builder.Services);

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options => {
        options.AllowAnyOrigin();
        options.AllowAnyHeader();
        options.AllowAnyMethod();
    });
    
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
