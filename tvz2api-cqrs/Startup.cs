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
using Microsoft.AspNetCore.Identity;

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
      services.AddDbContext<lmsContext>();
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
        endpoints.MapHub<NotificationHub>("/notification-hub");
        endpoints.MapHub<ChatHub>("/chat-hub");
      });
    }

    public void ConfigureAdditionalServices(IServiceCollection services)
    {
      services.AddScoped<ICommandBus, CommandBus>();
      services.AddScoped<IEventBus, EventBus>();
      services.AddScoped<IQueryBus, QueryBus>();

      services.AddScoped<IQueryHandlerAsync<CourseQuery, List<CourseQueryModel>>, CourseQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<CourseTotalQuery, int>, CourseQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<UserCourseQuery, List<UserDTO>>, CourseQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<UserCourseTotalQuery, int>, CourseQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<CourseDetailsQuery, CourseDetailsQueryModel>, CourseQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<CourseSidebarQuery, List<SidebarContentDTO>>, CourseQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<CourseNotificationsQuery, List<NotificationQueryModel>>, CourseQueryHandler>();

      services.AddScoped<ICommandHandlerAsync<FileUploadCommand, List<int>>, FileCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<FileUploadSidebarCommand, List<int>>, FileCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<FileDeleteCommand>, FileCommandHandler>();
      services.AddScoped<IQueryHandlerAsync<FileQuery, List<FileQueryModel>>, FileQueryHandler>();

      services.AddScoped<ICommandHandlerAsync<AuthenticationRegisterCommand>, AuthenticationCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<AuthenticationLoginCommand, LoginUserDTO>, AuthenticationCommandHandler>();

      services.AddScoped<ICommandHandlerAsync<UserSubscribeCommand>, UserCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<UserUnsubscribeCommand>, UserCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<UserUpdateSettingsCommand>, UserCommandHandler>();

      services.AddScoped<IQueryHandlerAsync<NotificationQuery, List<NotificationQueryModel>>, NotificationQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<NotificationUserQuery, List<NotificationQueryModel>>, NotificationQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<NotificationUserTotalQuery, int>, NotificationQueryHandler>();

      services.AddScoped<ICommandHandlerAsync<NotificationCreateCommand, NotificationQueryModel>, NotificationCommandHandler>();

      services.AddScoped<IQueryHandlerAsync<UserQuery, List<UserQueryModel>>, UserQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<UserTotalQuery, int>, UserQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<UserChatQuery, List<UserChatQueryModel>>, UserQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<UserSettingsQuery, UserSettingsQueryModel>, UserQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<UserSubscriptionQuery, List<int>>, UserQueryHandler>();

      services.AddScoped<IQueryHandlerAsync<ChatDetailsQuery, ChatQueryModel>, ChatQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<ChatAvailableUsersQuery, List<UserQueryModel>>, ChatQueryHandler>();

      services.AddScoped<ICommandHandlerAsync<SendMessageCommand, MessageDTO>, ChatCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<CreateNewChatCommand, NewChatDTO>, ChatCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<DeleteMessageCommand>, ChatCommandHandler>();

      services.AddScoped<IQueryHandlerAsync<ExamInProgressDetailsQuery, ExamAttemptDetailsQueryModel>, ExamQueryHandler>();
      services.AddScoped<IQueryHandlerAsync<ExamInProgressQuery, List<ExamAttemptQueryModel>>, ExamQueryHandler>();

      services.AddScoped<ICommandHandlerAsync<ExamUpdateAttemptCommand>, ExamCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<ExamStartAttemptCommand>, ExamCommandHandler>();
      services.AddScoped<ICommandHandlerAsync<ExamCreateCommand>, ExamCommandHandler>();
    }
  }
}