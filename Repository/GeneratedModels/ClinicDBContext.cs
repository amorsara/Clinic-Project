using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository.GeneratedModels;

public partial class ClinicDBContext : DbContext
{
    public ClinicDBContext()
    {
    }

    public ClinicDBContext(DbContextOptions<ClinicDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aggregatedcounter> Aggregatedcounters { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Closeroom> Closerooms { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Epilationarea> Epilationareas { get; set; }

    public virtual DbSet<Epilationmedicaltype> Epilationmedicaltypes { get; set; }

    public virtual DbSet<Epilationtreatment> Epilationtreatments { get; set; }

    public virtual DbSet<Hash> Hashes { get; set; }

    public virtual DbSet<Inquiry> Inquiries { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Jobparameter> Jobparameters { get; set; }

    public virtual DbSet<Jobqueue> Jobqueues { get; set; }

    public virtual DbSet<Lasermedicaltype> Lasermedicaltypes { get; set; }

    public virtual DbSet<Lasertreatment> Lasertreatments { get; set; }

    public virtual DbSet<Leserarea> Leserareas { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<Lock> Locks { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Schema> Schemas { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Tempcloseemployee> Tempcloseemployees { get; set; }

    public virtual DbSet<Tempworkhour> Tempworkhours { get; set; }

    public virtual DbSet<Treatmentstype> Treatmentstypes { get; set; }

    public virtual DbSet<Waiting> Waitings { get; set; }

    public virtual DbSet<Waxingtype> Waxingtypes { get; set; }

    public virtual DbSet<Workhour> Workhours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("ClinicDBConnectionString");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aggregatedcounter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("aggregatedcounter_pkey");

            entity.ToTable("aggregatedcounter", "hangfire");

            entity.HasIndex(e => e.Key, "aggregatedcounter_key_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Expireat).HasColumnName("expireat");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Idappointment).HasName("appointments_pkey");

            entity.ToTable("appointments");

            entity.Property(e => e.Idappointment).HasColumnName("idappointment");
            entity.Property(e => e.Area)
                .HasColumnType("character varying")
                .HasColumnName("area");
            entity.Property(e => e.Cancle).HasColumnName("cancle");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Idcontact).HasColumnName("idcontact");
            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.Idroom).HasColumnName("idroom");
            entity.Property(e => e.Ispay).HasColumnName("ispay");
            entity.Property(e => e.Isr).HasColumnName("isr");
            entity.Property(e => e.Isremaind).HasColumnName("isremaind");
            entity.Property(e => e.Remark)
                .HasColumnType("character varying")
                .HasColumnName("remark");
            entity.Property(e => e.Timeend).HasColumnName("timeend");
            entity.Property(e => e.Timestart).HasColumnName("timestart");
            entity.Property(e => e.Treatmentname)
                .HasColumnType("character varying")
                .HasColumnName("treatmentname");

            entity.HasOne(d => d.IdcontactNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Idcontact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Appointments_contact_id_fkey");

            entity.HasOne(d => d.IdemployeeNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Idemployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Appointments_employee_id_fkey");

            entity.HasOne(d => d.IdroomNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Idroom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Appointments_room_id_fkey");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Idattendance).HasName("attendance_pkey");

            entity.ToTable("attendance");

            entity.Property(e => e.Idattendance).HasColumnName("idattendance");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.R).HasColumnName("r");
            entity.Property(e => e.Timeenter).HasColumnName("timeenter");
            entity.Property(e => e.Timeexit).HasColumnName("timeexit");

            entity.HasOne(d => d.IdemployeeNavigation).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.Idemployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Attendance_employee_id_fkey");
        });

        modelBuilder.Entity<Closeroom>(entity =>
        {
            entity.HasKey(e => e.Idcloseroom).HasName("closerooms_pkey");

            entity.ToTable("closerooms");

            entity.Property(e => e.Idcloseroom).HasColumnName("idcloseroom");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Idrooms)
                .HasColumnType("character varying")
                .HasColumnName("idrooms");
            entity.Property(e => e.Reason)
                .HasColumnType("character varying")
                .HasColumnName("reason");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Idcontact).HasName("contacts_pkey");

            entity.ToTable("contacts");

            entity.Property(e => e.Idcontact).HasColumnName("idcontact");
            entity.Property(e => e.Credit).HasColumnName("credit");
            entity.Property(e => e.Electrolysis).HasColumnName("electrolysis");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Howcomeus)
                .HasColumnType("character varying")
                .HasColumnName("howcomeus");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Isshow).HasColumnName("isshow");
            entity.Property(e => e.Laser).HasColumnName("laser");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Medicalepilationlist)
                .HasColumnType("character varying")
                .HasColumnName("medicalepilationlist");
            entity.Property(e => e.Medicallaserlist)
                .HasColumnType("character varying")
                .HasColumnName("medicallaserlist");
            entity.Property(e => e.Phonenumber1)
                .HasMaxLength(11)
                .HasColumnName("phonenumber1");
            entity.Property(e => e.Phonenumber2)
                .HasMaxLength(11)
                .HasColumnName("phonenumber2");
            entity.Property(e => e.Phonenumber3)
                .HasMaxLength(11)
                .HasColumnName("phonenumber3");
            entity.Property(e => e.Pre).HasColumnName("pre");
            entity.Property(e => e.Remark)
                .HasColumnType("character varying")
                .HasColumnName("remark");
            entity.Property(e => e.Remarkelecr)
                .HasColumnType("character varying")
                .HasColumnName("remarkelecr");
            entity.Property(e => e.Remarklaser)
                .HasColumnType("character varying")
                .HasColumnName("remarklaser");
            entity.Property(e => e.Sem).HasColumnName("sem");
            entity.Property(e => e.Urlfile)
                .HasColumnType("character varying")
                .HasColumnName("urlfile");
            entity.Property(e => e.Waxing).HasColumnName("waxing");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("counter_pkey");

            entity.ToTable("counter", "hangfire");

            entity.HasIndex(e => e.Expireat, "ix_hangfire_counter_expireat");

            entity.HasIndex(e => e.Key, "ix_hangfire_counter_key");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Expireat).HasColumnName("expireat");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Idemployee).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.Isshow).HasColumnName("isshow");
            entity.Property(e => e.Lastinquiry).HasColumnName("lastinquiry");
            entity.Property(e => e.Lastmessageread).HasColumnName("lastmessageread");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password1)
                .HasMaxLength(50)
                .HasColumnName("password1");
            entity.Property(e => e.Password2)
                .HasColumnType("character varying")
                .HasColumnName("password2");
            entity.Property(e => e.Treatmentstype)
                .HasColumnType("character varying")
                .HasColumnName("treatmentstype");
        });

        modelBuilder.Entity<Epilationarea>(entity =>
        {
            entity.HasKey(e => e.Idepilationarea).HasName("epilationareas_pkey");

            entity.ToTable("epilationareas");

            entity.Property(e => e.Idepilationarea).HasColumnName("idepilationarea");
            entity.Property(e => e.Namearea)
                .HasColumnType("character varying")
                .HasColumnName("namearea");
        });

        modelBuilder.Entity<Epilationmedicaltype>(entity =>
        {
            entity.HasKey(e => e.Idepilationmedicaltype).HasName("epilationmedicaltypes_pkey");

            entity.ToTable("epilationmedicaltypes");

            entity.Property(e => e.Idepilationmedicaltype).HasColumnName("idepilationmedicaltype");
            entity.Property(e => e.Ischeck).HasColumnName("ischeck");
            entity.Property(e => e.Nametype)
                .HasColumnType("character varying")
                .HasColumnName("nametype");
            entity.Property(e => e.Note)
                .HasColumnType("character varying")
                .HasColumnName("note");
        });

        modelBuilder.Entity<Epilationtreatment>(entity =>
        {
            entity.HasKey(e => e.Idepilationtreatment).HasName("epilationtreatments_pkey");

            entity.ToTable("epilationtreatments");

            entity.Property(e => e.Idepilationtreatment).HasColumnName("idepilationtreatment");
            entity.Property(e => e.Area)
                .HasColumnType("character varying")
                .HasColumnName("area");
            entity.Property(e => e.Coloremployee)
                .HasColumnType("character varying")
                .HasColumnName("coloremployee");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Idcontact).HasColumnName("idcontact");
            entity.Property(e => e.Machine)
                .HasColumnType("character varying")
                .HasColumnName("machine");
            entity.Property(e => e.Results)
                .HasColumnType("character varying")
                .HasColumnName("results");
            entity.Property(e => e.Techniqe)
                .HasColumnType("character varying")
                .HasColumnName("techniqe");
            entity.Property(e => e.Time).HasColumnName("time");

            entity.HasOne(d => d.IdcontactNavigation).WithMany(p => p.Epilationtreatments)
                .HasForeignKey(d => d.Idcontact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EpilationTreatments_contact_id_fkey");
        });

        modelBuilder.Entity<Hash>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hash_pkey");

            entity.ToTable("hash", "hangfire");

            entity.HasIndex(e => new { e.Key, e.Field }, "hash_key_field_key").IsUnique();

            entity.HasIndex(e => e.Expireat, "ix_hangfire_hash_expireat");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Expireat).HasColumnName("expireat");
            entity.Property(e => e.Field).HasColumnName("field");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Updatecount).HasColumnName("updatecount");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Inquiry>(entity =>
        {
            entity.HasKey(e => e.Idinquirie).HasName("inquiries_pkey");

            entity.ToTable("inquiries");

            entity.Property(e => e.Idinquirie).HasColumnName("idinquirie");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Doinquirie).HasColumnName("doinquirie");
            entity.Property(e => e.Fullname)
                .HasColumnType("character varying")
                .HasColumnName("fullname");
            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.Phon)
                .HasColumnType("character varying")
                .HasColumnName("phon");
            entity.Property(e => e.Remark)
                .HasColumnType("character varying")
                .HasColumnName("remark");
            entity.Property(e => e.Response)
                .HasColumnType("character varying")
                .HasColumnName("response");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Sum)
                .HasColumnType("character varying")
                .HasColumnName("sum");
            entity.Property(e => e.Time).HasColumnName("time");

            entity.HasOne(d => d.IdemployeeNavigation).WithMany(p => p.Inquiries)
                .HasForeignKey(d => d.Idemployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Inquiries_employee_id_fkey");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("job_pkey");

            entity.ToTable("job", "hangfire");

            entity.HasIndex(e => e.Expireat, "ix_hangfire_job_expireat");

            entity.HasIndex(e => e.Statename, "ix_hangfire_job_statename");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Arguments)
                .HasColumnType("jsonb")
                .HasColumnName("arguments");
            entity.Property(e => e.Createdat).HasColumnName("createdat");
            entity.Property(e => e.Expireat).HasColumnName("expireat");
            entity.Property(e => e.Invocationdata)
                .HasColumnType("jsonb")
                .HasColumnName("invocationdata");
            entity.Property(e => e.Stateid).HasColumnName("stateid");
            entity.Property(e => e.Statename).HasColumnName("statename");
            entity.Property(e => e.Updatecount).HasColumnName("updatecount");
        });

        modelBuilder.Entity<Jobparameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jobparameter_pkey");

            entity.ToTable("jobparameter", "hangfire");

            entity.HasIndex(e => new { e.Jobid, e.Name }, "ix_hangfire_jobparameter_jobidandname");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Jobid).HasColumnName("jobid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Updatecount).HasColumnName("updatecount");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.Job).WithMany(p => p.Jobparameters)
                .HasForeignKey(d => d.Jobid)
                .HasConstraintName("jobparameter_jobid_fkey");
        });

        modelBuilder.Entity<Jobqueue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jobqueue_pkey");

            entity.ToTable("jobqueue", "hangfire");

            entity.HasIndex(e => new { e.Jobid, e.Queue }, "ix_hangfire_jobqueue_jobidandqueue");

            entity.HasIndex(e => new { e.Queue, e.Fetchedat }, "ix_hangfire_jobqueue_queueandfetchedat");

            entity.HasIndex(e => new { e.Queue, e.Fetchedat, e.Jobid }, "jobqueue_queue_fetchat_jobid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fetchedat).HasColumnName("fetchedat");
            entity.Property(e => e.Jobid).HasColumnName("jobid");
            entity.Property(e => e.Queue).HasColumnName("queue");
            entity.Property(e => e.Updatecount).HasColumnName("updatecount");
        });

        modelBuilder.Entity<Lasermedicaltype>(entity =>
        {
            entity.HasKey(e => e.Idlasermedicaltype).HasName("lasermedicaltypes_pkey");

            entity.ToTable("lasermedicaltypes");

            entity.Property(e => e.Idlasermedicaltype).HasColumnName("idlasermedicaltype");
            entity.Property(e => e.Ischeck).HasColumnName("ischeck");
            entity.Property(e => e.Nametype)
                .HasColumnType("character varying")
                .HasColumnName("nametype");
            entity.Property(e => e.Note)
                .HasColumnType("character varying")
                .HasColumnName("note");
        });

        modelBuilder.Entity<Lasertreatment>(entity =>
        {
            entity.HasKey(e => e.Idlasertreatment).HasName("lasertreatments_pkey");

            entity.ToTable("lasertreatments");

            entity.Property(e => e.Idlasertreatment).HasColumnName("idlasertreatment");
            entity.Property(e => e.Area)
                .HasColumnType("character varying")
                .HasColumnName("area");
            entity.Property(e => e.Coloremployee)
                .HasColumnType("character varying")
                .HasColumnName("coloremployee");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Energy)
                .HasColumnType("character varying")
                .HasColumnName("energy");
            entity.Property(e => e.Idcontact).HasColumnName("idcontact");
            entity.Property(e => e.Ms)
                .HasColumnType("character varying")
                .HasColumnName("ms");
            entity.Property(e => e.Results)
                .HasColumnType("character varying")
                .HasColumnName("results");
            entity.Property(e => e.Spotsize)
                .HasColumnType("character varying")
                .HasColumnName("spotsize");

            entity.HasOne(d => d.IdcontactNavigation).WithMany(p => p.Lasertreatments)
                .HasForeignKey(d => d.Idcontact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LaserTreatments_contact_id_fkey");
        });

        modelBuilder.Entity<Leserarea>(entity =>
        {
            entity.HasKey(e => e.Idleserarea).HasName("leserarea_pkey");

            entity.ToTable("leserareas");

            entity.Property(e => e.Idleserarea).HasColumnName("idleserarea");
            entity.Property(e => e.Namearea)
                .HasColumnType("character varying")
                .HasColumnName("namearea");
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("list_pkey");

            entity.ToTable("list", "hangfire");

            entity.HasIndex(e => e.Expireat, "ix_hangfire_list_expireat");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Expireat).HasColumnName("expireat");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Updatecount).HasColumnName("updatecount");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Lock>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("lock", "hangfire");

            entity.HasIndex(e => e.Resource, "lock_resource_key").IsUnique();

            entity.Property(e => e.Acquired).HasColumnName("acquired");
            entity.Property(e => e.Resource).HasColumnName("resource");
            entity.Property(e => e.Updatecount).HasColumnName("updatecount");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Idmessage).HasName("messages_pkey");

            entity.ToTable("messages");

            entity.Property(e => e.Idmessage).HasColumnName("idmessage");
            entity.Property(e => e.Answer)
                .HasColumnType("character varying")
                .HasColumnName("answer");
            entity.Property(e => e.Idfrom).HasColumnName("idfrom");
            entity.Property(e => e.Idto)
                .HasColumnType("character varying")
                .HasColumnName("idto");
            entity.Property(e => e.Question)
                .HasColumnType("character varying")
                .HasColumnName("question");

            entity.HasOne(d => d.IdfromNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.Idfrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Messages_employee1_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Idpayment).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.Idpayment).HasColumnName("idpayment");
            entity.Property(e => e.Advanced)
                .HasColumnType("character varying")
                .HasColumnName("advanced");
            entity.Property(e => e.Credit).HasColumnName("credit");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Datepayment)
                .HasColumnType("character varying")
                .HasColumnName("datepayment");
            entity.Property(e => e.Electrolysis)
                .HasColumnType("character varying")
                .HasColumnName("electrolysis");
            entity.Property(e => e.Employee)
                .HasColumnType("character varying")
                .HasColumnName("employee");
            entity.Property(e => e.Idcontact).HasColumnName("idcontact");
            entity.Property(e => e.Laser)
                .HasColumnType("character varying")
                .HasColumnName("laser");
            entity.Property(e => e.Owes).HasColumnName("owes");
            entity.Property(e => e.Pay).HasColumnName("pay");
            entity.Property(e => e.R).HasColumnName("r");
            entity.Property(e => e.Remark)
                .HasColumnType("character varying")
                .HasColumnName("remark");
            entity.Property(e => e.Treatment)
                .HasColumnType("character varying")
                .HasColumnName("treatment");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");
            entity.Property(e => e.Waxing)
                .HasColumnType("character varying")
                .HasColumnName("waxing");

            entity.HasOne(d => d.IdcontactNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Idcontact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Payments_contact_id_fkey");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Idroom).HasName("rooms_pkey");

            entity.ToTable("rooms");

            entity.Property(e => e.Idroom).HasColumnName("idroom");
            entity.Property(e => e.Isshow).HasColumnName("isshow");
            entity.Property(e => e.Nameroom)
                .HasMaxLength(50)
                .HasColumnName("nameroom");
            entity.Property(e => e.Treatmentstype)
                .HasColumnType("character varying")
                .HasColumnName("treatmentstype");
        });

        modelBuilder.Entity<Schema>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("schema_pkey");

            entity.ToTable("schema", "hangfire");

            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasColumnName("version");
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("server_pkey");

            entity.ToTable("server", "hangfire");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.Lastheartbeat).HasColumnName("lastheartbeat");
            entity.Property(e => e.Updatecount).HasColumnName("updatecount");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("set_pkey");

            entity.ToTable("set", "hangfire");

            entity.HasIndex(e => e.Expireat, "ix_hangfire_set_expireat");

            entity.HasIndex(e => new { e.Key, e.Score }, "ix_hangfire_set_key_score");

            entity.HasIndex(e => new { e.Key, e.Value }, "set_key_value_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Expireat).HasColumnName("expireat");
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Updatecount).HasColumnName("updatecount");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("state_pkey");

            entity.ToTable("state", "hangfire");

            entity.HasIndex(e => e.Jobid, "ix_hangfire_state_jobid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat).HasColumnName("createdat");
            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName("data");
            entity.Property(e => e.Jobid).HasColumnName("jobid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Updatecount).HasColumnName("updatecount");

            entity.HasOne(d => d.Job).WithMany(p => p.States)
                .HasForeignKey(d => d.Jobid)
                .HasConstraintName("state_jobid_fkey");
        });

        modelBuilder.Entity<Tempcloseemployee>(entity =>
        {
            entity.HasKey(e => e.Idtempcloseemployee).HasName("tempcloseemployees_pkey");

            entity.ToTable("tempcloseemployees");

            entity.Property(e => e.Idtempcloseemployee).HasColumnName("idtempcloseemployee");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.Reason)
                .HasColumnType("character varying")
                .HasColumnName("reason");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdemployeeNavigation).WithMany(p => p.Tempcloseemployees)
                .HasForeignKey(d => d.Idemployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TempCloseEmployees_employee_id_fkey");
        });

        modelBuilder.Entity<Tempworkhour>(entity =>
        {
            entity.HasKey(e => e.Idtempworkhour).HasName("tempworkhours_pkey");

            entity.ToTable("tempworkhours");

            entity.Property(e => e.Idtempworkhour).HasColumnName("idtempworkhour");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.Idroom).HasColumnName("idroom");
            entity.Property(e => e.Starthouer).HasColumnName("starthouer");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdemployeeNavigation).WithMany(p => p.Tempworkhours)
                .HasForeignKey(d => d.Idemployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TempWorkHours_id_fkey");

            entity.HasOne(d => d.IdroomNavigation).WithMany(p => p.Tempworkhours)
                .HasForeignKey(d => d.Idroom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TempWorkHours_room_id_fkey");
        });

        modelBuilder.Entity<Treatmentstype>(entity =>
        {
            entity.HasKey(e => e.Idtreatmenttype).HasName("treatmentstype_pkey");

            entity.ToTable("treatmentstype");

            entity.Property(e => e.Idtreatmenttype).HasColumnName("idtreatmenttype");
            entity.Property(e => e.Nametreatment)
                .HasColumnType("character varying")
                .HasColumnName("nametreatment");
        });

        modelBuilder.Entity<Waiting>(entity =>
        {
            entity.HasKey(e => e.Idwaiting).HasName("waiting_pkey");

            entity.ToTable("waiting");

            entity.Property(e => e.Idwaiting).HasColumnName("idwaiting");
            entity.Property(e => e.Fullname)
                .HasColumnType("character varying")
                .HasColumnName("fullname");
            entity.Property(e => e.Phone1)
                .HasColumnType("character varying")
                .HasColumnName("phone1");
            entity.Property(e => e.Phone2)
                .HasColumnType("character varying")
                .HasColumnName("phone2");
            entity.Property(e => e.Remark)
                .HasColumnType("character varying")
                .HasColumnName("remark");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .HasColumnName("type");
            entity.Property(e => e.Untildate).HasColumnName("untildate");
        });

        modelBuilder.Entity<Waxingtype>(entity =>
        {
            entity.HasKey(e => e.Idwaxingtype).HasName("waxingtypes_pkey");

            entity.ToTable("waxingtypes");

            entity.Property(e => e.Idwaxingtype).HasColumnName("idwaxingtype");
            entity.Property(e => e.Ischecked).HasColumnName("ischecked");
            entity.Property(e => e.Nametype)
                .HasColumnType("character varying")
                .HasColumnName("nametype");
        });

        modelBuilder.Entity<Workhour>(entity =>
        {
            entity.HasKey(e => e.Idworkhour).HasName("workhours_pkey");

            entity.ToTable("workhours");

            entity.Property(e => e.Idworkhour).HasColumnName("idworkhour");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.Endhour).HasColumnName("endhour");
            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.Idroom).HasColumnName("idroom");
            entity.Property(e => e.Regularwork).HasColumnName("regularwork");
            entity.Property(e => e.Shift)
                .HasMaxLength(1)
                .HasColumnName("shift");
            entity.Property(e => e.Starthour).HasColumnName("starthour");

            entity.HasOne(d => d.IdemployeeNavigation).WithMany(p => p.Workhours)
                .HasForeignKey(d => d.Idemployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("WorkHours_id_fkey");

            entity.HasOne(d => d.IdroomNavigation).WithMany(p => p.Workhours)
                .HasForeignKey(d => d.Idroom)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("workhours_room_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
