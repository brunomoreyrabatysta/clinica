using Clinica.Api.Endpoints;
using Clinica.Api.Common.Api;
using Clinica.Api;

var builder = WebApplication.CreateBuilder(args);

builder.AdicionarConfiguracao();
builder.AdicionarSeguranca();
builder.AdicionarContextos();
builder.AddCrossOrigin();
builder.AdicionarDocumentacao();
builder.AdicionarServicos();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfiguracaoDevEnvironment();

app.UseCors(ApiConfiguracao.CorsPolicyName); ;
app.UtilizacaoSeguranca();
app.MapEndpoints();

app.Run();
