using App.AutoFunctions;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Repository.GeneratedModels;
using Services.Appointments;
using Services.Contacts;
using Services.Employees;
using Services.Epilations;
using Services.EpilationTreatments;
using Services.LaserTreatments;
using Services.Lesers;
using Services.Payments;
using Services.Rooms;
using Services.Schedule;
using Services.TreatmentsType;
using Services.Waitings;
using Services.WorkHours;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration["ClinicDBConnectionString"];
builder.Services.AddDbContext<ClinicDBContext>(options => options.UseNpgsql(cs));

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddScoped<IContactsData, ContactsData>();
builder.Services.AddScoped<IEmployeesData, EmployeesData>();
builder.Services.AddScoped<IRoomsData, RoomsData>();
builder.Services.AddScoped<IAppointmentsData, AppointmentsData>();
builder.Services.AddScoped<IWorkHoursData, WorkHoursData>();
builder.Services.AddScoped<IScheduleData, ScheduleData>();
builder.Services.AddScoped<IWaitingsData, WaitingsData>();
builder.Services.AddScoped<IEpilationData, EpilationData>();
builder.Services.AddScoped<ILeserData, LeserData>();
builder.Services.AddScoped<ITreatmentsTypeData, TreatmentsTypeData>();
builder.Services.AddScoped<ILaserTreatmentData, LaserTreatmentData>();
builder.Services.AddScoped<IEpilationTreatmentData, EpilationTreatmentData>();
builder.Services.AddScoped<IPymentsData, PymentsData>();


var schedulerFactory = new StdSchedulerFactory();
var scheduler = await schedulerFactory.GetScheduler();
await scheduler.Start();

var job = JobBuilder.Create<AutoFunctions>()
    .WithIdentity("job1", "group1")
    .Build();

var trigger = TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInMinutes(5)
        .RepeatForever())
    .Build();


await scheduler.ScheduleJob(job, trigger);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


/*.WithIntervalInHours(1)*/ // הגדר כאן את התדירות שבה תרצה שהפונקציה תופעל (פעם ביום)