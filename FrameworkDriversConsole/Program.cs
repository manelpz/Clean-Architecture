using ApplicationBusinessLayer;
using EnterpriseBusinessLayer;
using InterfaceAdapterRepository;
using InterfaceAdpaterPresenter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


var configuration = builder.Build();

var container = new ServiceCollection()
    .AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped)
    .AddScoped<IRepository<Beer>, Repository>()
    .AddScoped<IPresenter<Beer,BeerViewModel>, BeerPresenter>()
    .AddScoped<IPresenter<Beer,BeerDetailViewModel>, BeerDetailPresenter>()
    .AddScoped<GetBeerUseCase<Beer, BeerViewModel>>()
    .AddScoped<GetBeerUseCase<Beer, BeerDetailViewModel>>()
    .BuildServiceProvider();

    var beerService = container.GetService<GetBeerUseCase<Beer, BeerDetailViewModel>>();

var beers = await    beerService.ExecuteAsync();

foreach (var beer in beers)
{
    Console.WriteLine(beer.Name +" " + beer.Alcohol+" "+beer.Message);
}