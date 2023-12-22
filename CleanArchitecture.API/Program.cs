using Carter;
using CleanArchitecture.API;
using CleanArchitecture.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//----------------------------------------------------
builder.Services.AddApi();
//----------------------------------------------------

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    new DatabaseSeed().SeedData(dataContext);
}
//------------------------------------------------------

app.Run();
