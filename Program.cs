var builder = WebApplication.CreateBuilder(args);

// Add CORS - This allows React to call the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "http://localhost:5174",
                "https://bangladesh-tourism.netlify.app"
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Use CORS
app.UseCors("AllowReactApp");

// API Endpoints
app.MapGet("/", () => "Bangladesh Tourism API is running!");

app.MapGet("/api/places", () => new[]
{
    new { id = 1, name = "Cox's Bazar", type = "Beach", description = "World's longest sea beach" },
    new { id = 2, name = "Sylhet", type = "Tea Gardens", description = "Beautiful tea estates" },
    new { id = 3, name = "Bandarban", type = "Hill Tracts", description = "Amazing mountain views" }
});

app.MapGet("/api/places/{id}", (int id) =>
{
    var places = new[]
    {
        new { id = 1, name = "Cox's Bazar", type = "Beach", description = "World's longest sea beach" },
        new { id = 2, name = "Sylhet", type = "Tea Gardens", description = "Beautiful tea estates" },
        new { id = 3, name = "Bandarban", type = "Hill Tracts", description = "Amazing mountain views" }
    };
    return places.FirstOrDefault(p => p.id == id);
});

app.Run();
