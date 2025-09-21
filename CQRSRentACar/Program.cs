using CQRSRentACar.Context;
using CQRSRentACar.CQRSPattern.Handlers.AboutHandlers;
using CQRSRentACar.CQRSPattern.Handlers.AirportHandlers;
using CQRSRentACar.CQRSPattern.Handlers.CarHandlers;
using CQRSRentACar.CQRSPattern.Handlers.EmployeeHandlers;
using CQRSRentACar.CQRSPattern.Handlers.FeatureHandlers;
using CQRSRentACar.CQRSPattern.Handlers.ServiceHandlers;
using CQRSRentACar.CQRSPattern.Handlers.SliderHandlers;
using CQRSRentACar.CQRSPattern.Handlers.TestimonialHandlers;
using CQRSRentACar.CQRSPattern.Handlers.CarRentalHandlers;
using CQRSRentACar.CQRSPattern.Handlers.ContactMessageHandlers;
using CQRSRentACar.Services;
using System.Collections.Generic;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CQRSContext>();

builder.Services.AddScoped<GetAboutQueryHandler>();
builder.Services.AddScoped<GetAboutByIdQueryHandler>();
builder.Services.AddScoped<CreateAboutCommandHandler>();
builder.Services.AddScoped<UpdateAboutCommandHandler>();
builder.Services.AddScoped<RemoveAboutCommandHandler>();

builder.Services.AddScoped<GetEmployeeQueryHandler>();
builder.Services.AddScoped<GetEmployeeByIdQueryHandler>();
builder.Services.AddScoped<CreateEmployeeCommandHandler>();
builder.Services.AddScoped<UpdateEmployeeCommandHandler>();
builder.Services.AddScoped<RemoveEmployeeCommandHandler>();

builder.Services.AddScoped<GetFeatureQueryHandler>();
builder.Services.AddScoped<GetFeatureByIdQueryHandler>();
builder.Services.AddScoped<CreateFeatureCommandHandler>();
builder.Services.AddScoped<UpdateFeatureCommandHandler>();
builder.Services.AddScoped<RemoveFeatureCommandHandler>();

builder.Services.AddScoped<GetServiceQueryHandler>();
builder.Services.AddScoped<GetServiceByIdQueryHandler>();
builder.Services.AddScoped<CreateServiceCommandHandler>();
builder.Services.AddScoped<UpdateServiceCommandHandler>();
builder.Services.AddScoped<RemoveServiceCommandHandler>();

builder.Services.AddScoped<GetTestimonialQueryHandler>();
builder.Services.AddScoped<GetTestimonialByIdQueryHandler>();
builder.Services.AddScoped<CreateTestimonialCommandHandler>();
builder.Services.AddScoped<UpdateTestimonialCommandHandler>();
builder.Services.AddScoped<RemoveTestimonialCommandHandler>();

builder.Services.AddScoped<GetSliderQueryHandler>();
builder.Services.AddScoped<GetSliderByIdQueryHandler>();
builder.Services.AddScoped<CreateSliderCommandHandler>();
builder.Services.AddScoped<UpdateSliderCommandHandler>();
builder.Services.AddScoped<RemoveSliderCommandHandler>();

builder.Services.AddScoped<GetCarQueryHandler>();
builder.Services.AddScoped<GetCarByIdQueryHandler>();
builder.Services.AddScoped<CreateCarCommandHandler>();
builder.Services.AddScoped<UpdateCarCommandHandler>();
builder.Services.AddScoped<RemoveCarCommandHandler>();

builder.Services.AddScoped<GetAirportQueryHandler>();
builder.Services.AddScoped<GetAirportByIdQueryHandler>();
builder.Services.AddScoped<CreateAirportCommandHandler>();
builder.Services.AddScoped<UpdateAirportCommandHandler>();
builder.Services.AddScoped<RemoveAirportCommandHandler>();

builder.Services.AddScoped<GetRentedCarIdsQueryHandler>();
builder.Services.AddScoped<GetCarRentalQueryHandler>();
builder.Services.AddScoped<CreateCarRentalCommandHandler>();

builder.Services.AddScoped<CreateContactMessageCommandHandler>();
builder.Services.AddScoped<GetContactMessageQueryHandler>();
builder.Services.AddScoped<GetContactMessageByIdQueryHandler>();
builder.Services.AddScoped<RemoveContactMessageCommandHandler>();

builder.Services.AddScoped<IAirportService, AirportService>();
builder.Services.AddScoped<IAirportDistanceService, AirportDistanceService>();
builder.Services.AddScoped<IChatGptService, ChatGptService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFuelPriceService, FuelPriceService>();

builder.Services.AddHttpClient<IAirportService, AirportService>();
builder.Services.AddHttpClient<IAirportDistanceService, AirportDistanceService>();
builder.Services.AddHttpClient<IChatGptService, ChatGptService>();
builder.Services.AddHttpClient<IFuelPriceService, FuelPriceService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Default/NotFound");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "404",
    pattern: "{*url}",
    defaults: new { controller = "Default", action = "NotFound" });

var supportedCultures = new[] { new CultureInfo("tr-TR") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("tr-TR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.Run();