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

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Epilationarea> Epilationareas { get; set; }

    public virtual DbSet<Leserarea> Leserareas { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Waiting> Waitings { get; set; }

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
            entity.Property(e => e.Isremaind).HasColumnName("isremaind");
            entity.Property(e => e.Remark)
                .HasColumnType("character varying")
                .HasColumnName("remark");
            entity.Property(e => e.Timeend).HasColumnName("timeend");
            entity.Property(e => e.Timestart).HasColumnName("timestart");
            entity.Property(e => e.Treatmentname)
                .HasMaxLength(1)
                .HasColumnName("treatmentname");
            entity.Property(e => e.Wait).HasColumnName("wait");

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

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Idcontact).HasName("contacts_pkey");

            entity.ToTable("contacts");

            entity.Property(e => e.Idcontact).HasColumnName("idcontact");
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
            entity.Property(e => e.Laser).HasColumnName("laser");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Phonenumber1)
                .HasMaxLength(11)
                .HasColumnName("phonenumber1");
            entity.Property(e => e.Phonenumber2)
                .HasMaxLength(11)
                .HasColumnName("phonenumber2");
            entity.Property(e => e.Phonenumber3)
                .HasMaxLength(11)
                .HasColumnName("phonenumber3");
            entity.Property(e => e.Remark)
                .HasColumnType("character varying")
                .HasColumnName("remark");
            entity.Property(e => e.Sem).HasColumnName("sem");
            entity.Property(e => e.Urlfile)
                .HasColumnType("character varying")
                .HasColumnName("urlfile");
            entity.Property(e => e.Waxing).HasColumnName("waxing");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Idemployee).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.Advancedelectrolysis).HasColumnName("advancedelectrolysis");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.Electrolysis).HasColumnName("electrolysis");
            entity.Property(e => e.Laser).HasColumnName("laser");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Permission).HasColumnName("permission");
            entity.Property(e => e.Waxing).HasColumnName("waxing");
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

        modelBuilder.Entity<Leserarea>(entity =>
        {
            entity.HasKey(e => e.Idleserarea).HasName("leserarea_pkey");

            entity.ToTable("leserareas");

            entity.Property(e => e.Idleserarea).HasColumnName("idleserarea");
            entity.Property(e => e.Namearea)
                .HasColumnType("character varying")
                .HasColumnName("namearea");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Idroom).HasName("rooms_pkey");

            entity.ToTable("rooms");

            entity.Property(e => e.Idroom).HasColumnName("idroom");
            entity.Property(e => e.Advancedelectrolysis).HasColumnName("advancedelectrolysis");
            entity.Property(e => e.Electrolysis).HasColumnName("electrolysis");
            entity.Property(e => e.Laser).HasColumnName("laser");
            entity.Property(e => e.Nameroom)
                .HasMaxLength(50)
                .HasColumnName("nameroom");
            entity.Property(e => e.Waxing).HasColumnName("waxing");
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

        modelBuilder.Entity<Workhour>(entity =>
        {
            entity.HasKey(e => e.Idworkhour).HasName("workhours_pkey");

            entity.ToTable("workhours");

            entity.Property(e => e.Idworkhour).HasColumnName("idworkhour");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.Endhour).HasColumnName("endhour");
            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.Regularwork).HasColumnName("regularwork");
            entity.Property(e => e.Shift)
                .HasMaxLength(1)
                .HasColumnName("shift");
            entity.Property(e => e.Starthour).HasColumnName("starthour");

            entity.HasOne(d => d.IdemployeeNavigation).WithMany(p => p.Workhours)
                .HasForeignKey(d => d.Idemployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("WorkHours_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
