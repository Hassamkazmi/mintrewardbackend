using Microsoft.OpenApi.Models;
using MintRewards.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);
builder.Services.AddControllersWithViews().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MintRewards", Version = "v1" });
        //c.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName) && api.GroupName == "PUBLIC");
        c.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));
        c.TagActionsBy(api => api.GroupName);
    });

 builder.Services.AddScoped<IUsersService, UsersService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
