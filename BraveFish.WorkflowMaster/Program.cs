using BraveFish.Base;
using BraveFish.WorkflowMaster.Application;
using BraveFish.WorkflowMaster.Contexts;
using BraveFish.WorkflowMaster.EntityFramework;
using BraveFish.WorkflowMaster.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiToBaseBehaviour(
    new ApiBaseBehaviourConfigBuilder()
    .SetInvalidModelStateResponseStatus(AppConstants.ErrorStatus.BAD_REQUEST)
    .Build());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRabbitContext();

var connectionString = builder.Configuration.GetConnectionString("NpgSql");

builder.Services.AddDbContext<WorkflowDbContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IPipelineService, PipelineService>();
builder.Services.AddScoped<ITransitionService, TransitionService>();

builder.Logging.AddBaseConsoleLogger();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WorkflowDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAppExceptionHandler();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
