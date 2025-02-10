using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Identity.Web;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  //  .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


builder.Services.AddDbContext<AngularDoodle.Server.ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("KemibrugV2Database")));



// Add controllers to the container
builder.Services.AddControllers();
// Add endpoint routing services to the container
builder.Services.AddEndpointsApiExplorer();
// Add swagger services to the container
builder.Services.AddSwaggerGen();
// add CORS services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("https://localhost:50015")  
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
        }
     );
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
