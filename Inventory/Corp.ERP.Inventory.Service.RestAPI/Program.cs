using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Infrastructure.Configurations;
using Corp.ERP.Inventory.Persistence;
using Corp.ERP.Inventory.Persistence.Repositories;

namespace Corp.ERP.Inventory.Service.RestAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var conf = builder.Configuration;

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        AddConfiguration(builder.Services, conf);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

    private static void AddConfiguration(IServiceCollection services, IConfiguration conf)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(Application.Application).Assembly)
        );
        services.AddScoped<IEquipmentRepositoryService, EquipmentRepositoryService>();
        services.AddDbContext<InventoryContext>();

        services.AddSingleton<DbConfiguration>(conf.GetSection("DbConfiguration").Get<DbConfiguration>());
            
    }
}