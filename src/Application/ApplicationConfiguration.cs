namespace CarRentalSystem.Application;

using System;
using System.Reflection;
using Behaviors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .Configure<ApplicationSettings>(
                configuration.GetSection(nameof(ApplicationSettings)),
                options => options.BindNonPublicProperties = true)
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddMediatR(new Action<MediatRServiceConfiguration>(mediatRServiceConfiguration
                => mediatRServiceConfiguration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
}