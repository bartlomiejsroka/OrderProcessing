using Microsoft.AspNetCore.Authentication;
using OrderProcessing.WebApi.Auth;
using OrderProcessing.WebApi.Services;
using OrderProcessing.WebApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services
builder.Services.AddScoped<IFileConverterService, FileConverterService>();
builder.Services.AddScoped<IRowTypeValidator, RowTypeValidator>();
builder.Services.AddScoped<IDocumentInputValidator, DocumentInputValidator>();
builder.Services.AddScoped<IInvoiceDocumentBuilderService, InvoiceDocumentBuilderService>();
builder.Services.AddScoped<IArticleInputValidator, ArticleInputValidator>();
builder.Services.AddScoped<IArticleBuilderService, ArticleBuilderService>();

//auth
builder.Services.AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(BasicAuthenticationDefaults.AuthenticationScheme, null);

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
