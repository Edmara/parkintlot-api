using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ParkingLotApi.Config;
using ParkingLotApi.Database;
using ParkingLotApi.Repositories;
using ParkingLotApi.Services;

namespace ParkingLotApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new ServerConfig();
            Configuration.Bind(config);
            var parkingLotContext = new ParkingLotContext(config.MongoDB);

            var lotRepository = new LotRepository(parkingLotContext);
            services.AddSingleton<ILotRepository>(lotRepository);

            var paymentRepository = new PaymentRepository(parkingLotContext);
            services.AddSingleton<IPaymentRepository>(paymentRepository);

            var ticketRepository = new TicketRepository(parkingLotContext);
            services.AddSingleton<ITicketRepository>(ticketRepository);

            var parkingRepository = new ParkingRepository(parkingLotContext);
            services.AddSingleton<IParkingRepository>(parkingRepository);

            services.AddSingleton<ILotService, LotService>();
            services.AddSingleton<IParkingService, ParkingService>();
            services.AddSingleton<IPaymentService, PaymentService>();
            services.AddSingleton<ITicketService, TicketService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
                                    {
                                        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                                        {
                                            Title = "ParkingLot API",
                                            Version = "v1",
                                            Description = "ParkingLot API tutorial using MongoDB",
                                        });


                                        //OpenApiSecurityScheme scheme = new OpenApiSecurityScheme
                                        //{
                                        //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                                        //    Name = "Authorization",
                                        //    In = ParameterLocation.Header,
                                        //    Type = SecuritySchemeType.ApiKey
                                        //};

                                        //c.AddSecurityDefinition("Bearer", scheme);

                                        //OpenApiSecurityRequirement requirements = new OpenApiSecurityRequirement();
                                        //requirements.Add(scheme, new string[] { });

                                        //c.AddSecurityRequirement(requirements);
                                        
                                    });

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = "Audience";
                paramsValidation.ValidIssuer = "Issuer";

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
