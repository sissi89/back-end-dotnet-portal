using WebApi.Helpers;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// aggiungo i servzi per le chiamate
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    // libreria Ng
    services.AddSwaggerDocument();
    // aggiungo i controller
   services.AddScoped<IUserService, UserService>();

   services.AddScoped<ISinistriService, SinistriService>();
    services.AddHttpContextAccessor();
}

var app = builder.Build();
// prova 
app.MapGet("/silvana", () => "Hello World!");
// Il metodo MapControllerRoute viene utilizzato per configurare il routing
app.UseOpenApi();
app.UseSwaggerUi3();

app.UseDeveloperExceptionPage();
// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}
  
app.Run("http://localhost:4000");

