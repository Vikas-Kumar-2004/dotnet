using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebMinimalExample.Data;
using WebMinimalExample.Endpoints;
using WebMinimalExample.Models;
using WebMinimalExample.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidation();
builder.Services.AddProblemDetails();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<CategoryCreateDTO, Category>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.AddedDate, opt => opt.Ignore());

    cfg.CreateMap<CategoryUpdateDTO, Category>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.AddedDate, opt => opt.Ignore());

    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
    
});


var app = builder.Build();

// Configure the HTTP request pipeline.

    app.MapOpenApi();
    app.MapScalarApiReference();

//1.Route without ID
app.MapGet("/helloworld", () =>
{
    return Results.Ok("Hello World");
});

//2.Route with ID
app.MapGet("/helloworld/{id}", (int id) =>
{
    return Results.Ok($"Hello World: {id}");
});
//------------------Categories API------------------------

//app.MapGet("/api/categories", (ApplicationDbContext db) => // without asyncronous 
//{
//    var categories = db.Categories.ToList();
//    return Results.Ok(categories);
//});


app.MapCategoryEndpoints();



app.UseHttpsRedirection();


app.Run();

