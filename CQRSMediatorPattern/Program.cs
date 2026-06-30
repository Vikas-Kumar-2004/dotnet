using CQRSMediatorPattern.Application.Orders.Dto;
using CQRSMediatorPattern.Application.Orders.Query;
using CQRSMediatorPattern.Application.Orders.Command;
using CQRSMediatorPattern.Application.Orders.Validator;
using CQRSMediatorPattern.Domain.Entities;
using CQRSMediatorPattern.Persistence.DbContexts;
using CQRSMediatorPattern.Api.Endpoints;
using CQRSMediatorPattern.Abstractions;
using CQRSMediatorPattern.Handlers;
using CQRSMediatorPattern.Dispatching;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ─── Services ────────────────────────────────────────────────────────────────
builder.Services.AddSingleton<IDispatcher, Dispatcher>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Scan(scan =>
{
    scan.FromAssemblyOf<CQRSMediatorPattern.Application.ApplicationAssembly>()
        .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()

        .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()

        .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime();
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderDtoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();



// ─── Scalar API UI ───────────────────────────────────────────────────────────
app.MapOpenApi();
app.MapScalarApiReference();

// ─── Endpoints ───────────────────────────────────────────────────────────────

app.MapOrderEndpoints();
app.MapProductEndpoints();
app.MapGet("/", () => Results.Redirect("/scalar/v1"));

app.Run();