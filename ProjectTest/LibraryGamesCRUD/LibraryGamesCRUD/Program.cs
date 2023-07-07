using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using LibraryGamesCRUD.Models;

var builder = WebApplication.CreateBuilder(args);

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Games>("Games");
    builder.EntitySet<GamesToGenres>("GamesToGenres");
    builder.EntitySet<GenresGames>("GenresGames");
    builder.EntitySet<StudioName>("StudioName");
    return builder.GetEdmModel();
}
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("LibraryGames", GetEdmModel()).Select().Filter().Count().SetMaxTop(25));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
