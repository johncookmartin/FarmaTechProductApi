using FarmaTechData.Data;
using ProductApi.Features.BlobStorage.Access;
using ProductApi.Features.BlobStorage.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddScoped<IProductData, ProductData>();

builder.Services.AddScoped<IBlobServiceFactory, BlobServiceFactory>();

builder.Services.AddScoped<IProductFileAccess>(sp =>
{
    var factory = sp.GetRequiredService<IBlobServiceFactory>();
    var db = sp.GetRequiredService<IProductData>();
    var blobService = factory.Create("Products");

    return new ProductFileAccess(blobService, db);
});

builder.Services.AddScoped<IFlyFileAccess>(sp =>
{
    var factory = sp.GetRequiredService<IBlobServiceFactory>();
    var db = sp.GetRequiredService<IProductData>();
    var blobService = factory.Create("Flies");

    return new FlyFileAccess(blobService, db);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
