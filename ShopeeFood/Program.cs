using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopeeFood.Interfaces;
using ShopeeFood.Models;
using ShopeeFood.Services;
using System.Text;
using System.Text.Json.Serialization;
using TodoApi.Constants;

var policyName = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
	s.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopee", Version = "v1" });
	//configure Swagger JWT
	s.AddSecurityDefinition(JwtAuthenticationDefaults.BearerPrefix, new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme.",
		Name = "Authorization", // Authorization
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
	});
	s.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new List<string>()
		}
	});
});
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
 {
	 options.SaveToken = true;
	 options.RequireHttpsMetadata = false;
	 options.TokenValidationParameters = new TokenValidationParameters()
	 {
		 ValidateIssuer = true,
		 ValidateAudience = true,
		 ValidAudience = configuration["JWT:ValidAudience"],
		 ValidIssuer = configuration["JWT:ValidIssuer"],
		 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
	 };
 });


builder.Services.AddCors(options =>
{
	options.AddPolicy(name: policyName, builder =>
					  {
						  builder
							.WithOrigins("http://localhost:3000") // specifying the allowed origin
							.WithMethods("GET", "POST", "PUT", "DELETE") // defining the allowed HTTP method
							.AllowAnyHeader(); // allowing any header to be sent
					  });
});

builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<ICartItem, CartItemService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IOrder, OrderService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.Cookie.Name = "CartId";
	options.IdleTimeout = TimeSpan.FromMinutes(1);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<todoAPIContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));
var app = builder.Build();

app.UseSession();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
