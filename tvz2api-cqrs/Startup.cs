using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using tvz2api_cqrs.Implementation.CommandHandlers;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Implementation.Messaging;
using tvz2api_cqrs.Implementation.QueryHandlers;

namespace tvz2api_cqrs
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      ConfigureAdditionalServices(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    public void ConfigureAdditionalServices(IServiceCollection services)
    {
      services.AddScoped<ICommandBus, CommandBus>();
      services.AddScoped<IEventBus, EventBus>();
      services.AddScoped<IQueryBus, QueryBus>();
      services.AddScoped<IQueryHandlerAsync<TestQuery, List<TestModel>>, TestQueryHandler>();
    }
  }
}
