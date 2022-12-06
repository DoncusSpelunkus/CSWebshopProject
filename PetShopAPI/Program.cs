
using AutoMapper;
using Factory.Domain;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;
using PetShop.Infastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetShop.Application;
using PetShop.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var config = new MapperConfiguration(conf =>
{
    conf.CreateMap<ProdDTO, Product>();
    conf.CreateMap<SpecDTO, Specs>();
    conf.CreateMap<MainCatDTO, MainCategory>();
    conf.CreateMap<SubCatDTO, SubCategory>();
    conf.CreateMap<SpecDescDTO, SpecsDescription>();
});

var mapper = config.CreateMapper();


builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBContext>(options => options.UseSqlite("Data source=db.db"));
builder.Services.AddScoped<IShopService , ShopService>();
builder.Services.AddScoped<ISpecService , SpecService>();
builder.Services.AddScoped<ICatService , CatService>();
builder.Services.AddScoped<IShopRepo, ShopRepo>();
builder.Services.AddScoped<ISpecRepo , SpecsRepo>();
builder.Services.AddScoped<ICatService , CatService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
