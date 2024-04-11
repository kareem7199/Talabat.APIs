
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			#region Configure Services

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies();
			});

			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			#endregion

			var app = builder.Build();

			using var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;

			var _dbContext = services.GetRequiredService<StoreContext>(); // ask clr for creating object from DbContext Explicitly
		
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				await _dbContext.Database.MigrateAsync();
				await StoreContextSeed.SeedAsync(_dbContext);
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex , "an error has been occured during apply the migration");
			}

			#region Configure Kestrel Middlwares
			
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.MapControllers();

			#endregion

			app.Run();
		}
	}
}
