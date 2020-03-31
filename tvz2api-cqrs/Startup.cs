using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using tvz2api_cqrs.Hubs;
using tvz2api_cqrs.Implementation.CommandHandlers;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Implementation.QueryHandlers;
using tvz2api_cqrs.Infrastructure.CommandHandlers;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Infrastructure.Messaging;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using tvz2api_cqrs.Middleware;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.QueryModels;

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
      services.AddSignalR();
      services.AddDbContext<tvz2Context>();
      services.AddControllers();
      services.AddAuthorization(options =>
      {
        var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
        defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
        options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
      });
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(
          builder =>
          {
            builder
              .WithOrigins(new string[] { "http://localhost:8080", "null" })
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetIsOriginAllowedToAllowWildcardSubdomains();
          });
      });
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "tvz2cqrs", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = "Enter Bearer [space] and then your token in the text input below.",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
          {
            new OpenApiSecurityScheme
            {
              Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header
            },
            new List<string>()
          }
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
      app.UseOptions();
      app.UseHttpsRedirection();
      app.UseWebSockets();
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "tvz2cqrs");
      });
      app.UseRouting();
      app.UseAuthorization();
      app.UseAuthentication();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapHub<VijestiHub>("/vijesti-hub");
        endpoints.MapHub<ChatHub>("/chat-hub");
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

      services.AddScoped<ICommandHandlerAsync<AuthenticationRegisterCommand>, AuthenticationCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<AuthenticationLoginCommand, LoginUserDTO>, AuthenticationCommandHandler>();

      services.AddScoped<IQueryHandlerAsync<StudentDetailsQuery, StudentQueryModel>, StudentQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<StudentPretplataQuery, List<int>>, StudentQueryHandler>();
      services.AddScoped<ICommandHandlerAsync<StudentSubscribeCommand>, StudentCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<StudentUnsubscribeCommand>, StudentCommandHandler>();

      services.AddScoped<IQueryHandlerAsync<EmployeeQuery, List<EmployeeQueryModel>>, EmployeeQueryHandler>();

      services.AddScoped<IQueryHandlerAsync<VijestQuery, List<VijestQueryModel>>, VijestQueryHandler>();
      services.AddScoped<ICommandHandlerAsync<CreateVijestCommand, VijestQueryModel>, VijestCommandHandler>();

      services.AddScoped<IQueryHandlerAsync<KorisnikQuery, List<KorisnikQueryModel>>, KorisnikQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<KorisnikTotalQuery, int>, KorisnikQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<KorisnikChatQuery, List<KorisnikChatQueryModel>>, KorisnikQueryHandler>();

      services.AddScoped<IQueryHandlerAsync<ChatDetailsQuery, ChatQueryModel>, ChatQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<ChatAvailableUsersQuery, List<KorisnikQueryModel>>, ChatQueryHandler>();

      services.AddScoped<ICommandHandlerAsync<SendMessageCommand, MessageDTO>, ChatCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<CreateNewChatCommand, NewChatDTO>, ChatCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<DeleteMessageCommand>, ChatCommandHandler>();
    }
  }
}