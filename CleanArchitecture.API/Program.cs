using Carter;
using CleanArchitecture.Infrastructure;

namespace CleanArchitecture.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //----------------------------------------------------
        builder.Services.AddApi();
        //----------------------------------------------------

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        //------------------------------------------------------
        app.MapCarter(); // Carter will take care of mapping all API routes that are specified in Services

        if (app.Environment.EnvironmentName != "Test")
        {
            using var scope = app.Services.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            DatabaseSeed.SeedData(dataContext);
        }
        //------------------------------------------------------

        app.Run();
    }
}
