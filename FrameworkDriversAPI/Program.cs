


using ApplicationBusinessLayer;
using EnterpriseBusinessLayer;
using FluentValidation;
using FluentValidation.AspNetCore;
using FrameworkDriversAPI.Middlewares;
using FrameworkDriversAPI.Validators;
using FrameworkDriversExternalServices;
using InterfaceAdaptarModels;
using InterfaceAdapterAdapters;
using InterfaceAdapterAdapters.DTO;
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
builder.Services.AddScoped<IRepository<Sale>, SaleRepository>();
builder.Services.AddScoped<IPresenter<Beer, BeerViewModel>, BeerPresenter>();
 
//presentar detail
builder.Services.AddScoped<IPresenter<Beer, BeerDetailViewModel>, BeerDetailPresenter>();
builder.Services.AddScoped<IExternalService<PostServiceDTO>, PostService>();
builder.Services.AddScoped<IExternalServiceAdapter<Post>, PostExternalServiceAdapter>();
//use case injection
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerViewModel>>();
builder.Services.AddScoped<GetBeerUseCase<Beer, BeerDetailViewModel>>();
builder.Services.AddScoped<GetPostUseCase>();
builder.Services.AddScoped<GenerateSaleUseCase<SaleRequestDTO>>();
builder.Services.AddScoped<GetSaleUseCase>();

builder.Services.AddScoped<AddBeerUseCase<BeerRequestDTO>>();
builder.Services.AddScoped<IMapper<BeerRequestDTO, Beer>, BeerMapper>();
builder.Services.AddScoped<IMapper<SaleRequestDTO, Sale>, SaleMapper>();

//validators
builder.Services.AddValidatorsFromAssemblyContaining<BeerValidator>();
builder.Services.AddFluentValidation();
builder.Services.AddHttpClient<IExternalService<PostServiceDTO>, PostService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPost"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//adding middleware for handling exceptions
app.UseMiddleware<ExceptionMiddleware>();

app.MapGet("/beer", async (GetBeerUseCase<Beer, BeerViewModel> beerUseCase) =>
{
    //return "hola mundo";
    return await beerUseCase.ExecuteAsync();
})
    .WithName("beers")
    .WithOpenApi();

app.MapGet("/beerDetail", async (GetBeerUseCase<Beer, BeerDetailViewModel> beerUseCaseDetail) =>
    {
        return await beerUseCaseDetail.ExecuteAsync();
    })
    .WithName("beerDetail")
    .WithOpenApi();

app.MapGet("/post", async (GetPostUseCase postUseCase) =>
    {
        return await postUseCase.ExecuteAsync();
    })
    .WithName("post")
    .WithOpenApi();

app.MapPost("/beer", async (AddBeerUseCase<BeerRequestDTO> beerUseCase, BeerRequestDTO beerRequest,
    IValidator<BeerRequestDTO> validator) =>
        {
            var result = await validator.ValidateAsync(beerRequest);

            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    )
                    
                    );
            }
            
            await beerUseCase.ExecuteAsync(beerRequest);
            return Results.Created($"/beer/{beerRequest.Id}", beerRequest);
        }
    ).WithName("addBeer")
    .WithOpenApi();

    app.MapPost("/CreateSale", async (SaleRequestDTO salesRequest, GenerateSaleUseCase<SaleRequestDTO> saleUseCase) =>
    {
        await saleUseCase.ExecuteAsync(salesRequest);
        return Results.Ok(salesRequest);
    })
    .WithName("CreateSale")
    .WithOpenApi();


    app.MapGet("/sale", async (GetSaleUseCase saleUseCase) =>
        {
            return await saleUseCase.ExecuteAsync();
        })
        .WithName("GetSale")
        .WithOpenApi();


app.Run();