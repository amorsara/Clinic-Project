using Microsoft.EntityFrameworkCore;
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



builder.Services.AddControllers();
builder.Services.AddSignalR();

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

