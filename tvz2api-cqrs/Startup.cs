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
using tvz2api_cqrs.Infrastructure.CommandHandlers;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Infrastructure.Messaging;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Implementation.QueryHandlers;
using tvz2api_cqrs.Implementation.CommandHandlers;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.QueryModels;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

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
      ConfigureAdditionalServices(services);
      services.AddDbContext<tvz2Context>();
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "tvz2cqrs", Version = "v1" });
      });
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(
        builder =>
        {
          builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseCors();
      app.UseHttpsRedirection();
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "tvz2cqrs");
      });
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

      services.AddScoped<IQueryHandlerAsync<KolegijQuery, List<KolegijQueryModel>>, KolegijQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<KolegijTotalQuery, int>, KolegijQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<StudentKolegijQuery, List<StudentQueryModel>>, KolegijQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<StudentKolegijTotalQuery, int>, KolegijQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<KolegijDetailsQuery, KolegijDetailsQueryModel>, KolegijQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<KolegijSidebarQuery, List<SidebarContentDTO>>, KolegijQueryHandler>();

      services.AddScoped<ICommandHandlerAsync<FileUploadCommand, List<int>>, FileCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<FileUploadSidebarCommand, List<int>>, FileCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<FileDeleteCommand>, FileCommandHandler>();

      services.AddScoped<IQueryHandlerAsync<FileQuery, List<FileQueryModel>>, FileQueryHandler>();
    }
  }
}