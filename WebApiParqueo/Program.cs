var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Interfaces DataAccess
builder.Services.AddScoped<DataAccess.Interfaces.IEmpleadosDA, DataAccess.Class.EmpleadosDA>();

// Interfaces BusinessLogic
builder.Services.AddScoped<BusinessLogic.Interfaces.IEmpleadosBL, BusinessLogic.Class.EmpleadosBL>();

// Se realiza la inicializacion de los objetos
Models.General.GlobalVariables.Empleados = new List<Models.Empleados.Empleados>();
Models.General.GlobalVariables.Empleados.Add(new Models.Empleados.Empleados(){
    IdEmpleado = 1,
    idParqueo = 1,
    NumeroEmpleado = "1",
    FechaIngreso = DateTime.Now,
    PrimerNombre = "Carlos",
    SegundoNombre = "Jesus",
    PrimerApellido = "Pereira",
    SegundoApellido = "Coto",
    FechaNacimiento = DateTime.Now,
    Identificacion = "301230556",
    Direccion = "Cartago",
    CorreoElectronico = "carlos@test.com",
    Telefono = "89885718",
    PersonaContacto = "Carlos"
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "Web Api Parqueo";
});

app.Run();
