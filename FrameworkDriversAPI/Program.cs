


using ApplicationBusinessLayer;
using EnterpriseBusinessLayer;
using InterfaceAdaptarModels;
using InterfaceAdapterMappers;
using InterfaceAdapterMappers.DTO.Request;
//using InterfaceAdapter;
using InterfaceAdapterRepository;
using InterfaceAdpaterPresenter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependencies

//injection entityframework
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IRepository<Beer>, Repository>();
builder.Services.AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>();
    
//use case injection
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerViewModel>>();
builder.Services.AddScoped<AddBeerUseCase<BeerRequestDTO>>();
builder.Services.AddScoped<IMapper<BeerRequestDTO, Beer>, BeerMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
  
app.MapGet("/beer", async (GetBeerUseCase<Beer, BeerViewModel> beerUseCase) =>
{
    //return "hola mundo";
    return await beerUseCase.ExecuteAsync();
})
    .WithName("beers")
    .WithOpenApi();


app.MapPost("/beer", async (BeerRequestDTO beerRequest, AddBeerUseCase<BeerRequestDTO> beerUseCase) =>
        {
            await beerUseCase.ExecuteAsync(beerRequest);
            return Results.Created($"/beer/{beerRequest.Id}", beerRequest);
        }
    ).WithName("addBeer")
    .WithOpenApi();

app.Run();