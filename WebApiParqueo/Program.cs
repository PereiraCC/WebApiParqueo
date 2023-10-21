var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Interfaces DataAccess
builder.Services.AddScoped<DataAccess.Interfaces.IEmpleadosDA, DataAccess.Class.EmpleadosDA>();
builder.Services.AddScoped<DataAccess.Interfaces.IParqueoDA, DataAccess.Class.ParqueoDA>();
builder.Services.AddScoped<DataAccess.Interfaces.ITiqueteDA, DataAccess.Class.TiqueteDA>();

// Interfaces BusinessLogic
builder.Services.AddScoped<BusinessLogic.Interfaces.IEmpleadosBL, BusinessLogic.Class.EmpleadosBL>();
builder.Services.AddScoped<BusinessLogic.Interfaces.IParqueoBL, BusinessLogic.Class.ParqueoBL>();
builder.Services.AddScoped<BusinessLogic.Interfaces.ITiqueteBL, BusinessLogic.Class.TiqueteBL>();

// Se realiza la inicializacion de los objetos
Models.General.GlobalVariables.Empleados = new List<Models.Empleados.Empleados>();
Models.General.GlobalVariables.EmpleadosFiltrados = new List<Models.Empleados.Empleados>();
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

// Se realiza la inicializacion de los objetos
Models.General.GlobalVariables.Parqueos = new List<Models.Parqueos.Parqueo>();
Models.General.GlobalVariables.ParqueosFiltrados = new List<Models.Parqueos.Parqueo>();
Models.General.GlobalVariables.Parqueos.Add(new Models.Parqueos.Parqueo()
{
    idParqueo = 1,
    Nombre = "Premium",
    CantidadMaximaVehiculos = 50,
    HoraApertura = DateTime.Now,
    HoraCierre = DateTime.Now,
    TarifaHora = 1000,
    TarifaMediaHora = 500,
});

// Se realiza la inicializacion de los objetos
Models.General.GlobalVariables.Tiquetes = new List<Models.Tiquetes.Tiquete>();
Models.General.GlobalVariables.TiquetesFiltrados = new List<Models.Tiquetes.Tiquete>();
Models.General.GlobalVariables.Tiquetes.Add(new Models.Tiquetes.Tiquete()
{
    idTiquete = 1,
    idParqueo = 1,
    idEmpleado = 1,
    fechaIngreso = DateTime.Now,
    fechaSalida = DateTime.Now,
    placa = "ASD123",
    tiempoConsumido = "03:00:00",
    montoPagar = 3000
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
