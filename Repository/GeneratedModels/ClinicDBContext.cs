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

    public virtual DbSet<Room> Rooms { get; set; }

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
            entity.Property(e => e.Cancle).HasColumnName("cancle");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Idcontact).HasColumnName("idcontact");
            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.Remainder).HasColumnName("remainder");
            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .HasColumnName("remark");
            entity.Property(e => e.Timeend).HasColumnName("timeend");
            entity.Property(e => e.Timestart).HasColumnName("timestart");
            entity.Property(e => e.Treatmentname)
                .HasMaxLength(100)
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
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Idcontact).HasName("contacts_pkey");

            entity.ToTable("contacts");

            entity.Property(e => e.Idcontact).HasColumnName("idcontact");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Homephone)
                .HasMaxLength(11)
                .HasColumnName("homephone");
            entity.Property(e => e.Howcomeus)
                .HasMaxLength(200)
                .HasColumnName("howcomeus");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Mobilephone)
                .HasMaxLength(11)
                .HasColumnName("mobilephone");
            entity.Property(e => e.Numberphone)
                .HasMaxLength(11)
                .HasColumnName("numberphone");
            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .HasColumnName("remark");
            entity.Property(e => e.Sem).HasColumnName("sem");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("url");
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

        modelBuilder.Entity<Workhour>(entity =>
        {
            entity.HasKey(e => e.Idworkhour).HasName("workhours_pkey");

            entity.ToTable("workhours");

            entity.Property(e => e.Idworkhour).HasColumnName("idworkhour");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.Endhour).HasColumnName("endhour");
            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.Regularwork).HasColumnName("regularwork");
            entity.Property(e => e.Shift).HasColumnName("shift");
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
