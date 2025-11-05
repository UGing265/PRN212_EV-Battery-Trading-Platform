using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class EvbatterySwapDbContext : DbContext
{
    public EvbatterySwapDbContext()
    {
    }

    public EvbatterySwapDbContext(DbContextOptions<EvbatterySwapDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Battery> Batteries { get; set; }

    public virtual DbSet<BatteryModel> BatteryModels { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<OtpToken> OtpTokens { get; set; }

    public virtual DbSet<PackagePlan> PackagePlans { get; set; }

    public virtual DbSet<RevokedToken> RevokedTokens { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Battery>(entity =>
        {
            entity.HasKey(e => e.BatteryId).HasName("PK__Battery__5710803E2034035B");

            entity.ToTable("Battery");

            entity.Property(e => e.BatteryId).HasColumnName("BatteryID");
            entity.Property(e => e.BatteryName).HasMaxLength(100);
            entity.Property(e => e.DetailInformation).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<BatteryModel>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__BatteryM__E8D7A1CC4103F9FE");

            entity.ToTable("BatteryModel");

            entity.Property(e => e.ModelId)
                .ValueGeneratedNever()
                .HasColumnName("ModelID");
            entity.Property(e => e.ModelName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACDC25CC5A5");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BatteryId).HasColumnName("BatteryID");
            entity.Property(e => e.CancelReason).HasMaxLength(200);
            entity.Property(e => e.CanceledAt).HasColumnType("datetime");
            entity.Property(e => e.DepositAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DepositStatus).HasMaxLength(20);
            entity.Property(e => e.DepositTxnId).HasColumnName("DepositTxnID");
            entity.Property(e => e.EstimatedPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HoldUntil).HasColumnType("datetime");
            entity.Property(e => e.NewUnitId).HasColumnName("NewUnitID");
            entity.Property(e => e.OldUnitId).HasColumnName("OldUnitID");
            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("BOOKED");
            entity.Property(e => e.TimeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Battery).WithMany(p => p.BookingBatteries)
                .HasForeignKey(d => d.BatteryId)
                .HasConstraintName("FK_Booking_Battery");

            entity.HasOne(d => d.DepositTxn).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.DepositTxnId)
                .HasConstraintName("FK_Booking_DepositTxn");

            entity.HasOne(d => d.NewUnit).WithMany(p => p.BookingNewUnits)
                .HasForeignKey(d => d.NewUnitId)
                .HasConstraintName("FK_Booking_NewBattery");

            entity.HasOne(d => d.OldUnit).WithMany(p => p.BookingOldUnits)
                .HasForeignKey(d => d.OldUnitId)
                .HasConstraintName("FK_Booking_OldBattery");

            entity.HasOne(d => d.Station).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__Station__5AEE82B9");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__UserID__5BE2A6F2");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__Vehicle__5CD6CB2B");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF6AB6B57C4");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.Comment).HasMaxLength(300);
            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Station).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__Statio__6A30C649");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__UserID__693CA210");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6D3264C976F");

            entity.ToTable("Inventory");

            entity.HasIndex(e => new { e.StationId, e.BatteryId }, "UQ_Inventory_Station_Battery").IsUnique();

            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.BatteryId).HasColumnName("BatteryID");
            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Battery).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.BatteryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__Batte__5812160E");

            entity.HasOne(d => d.Station).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventory__Stati__571DF1D5");
        });

        modelBuilder.Entity<OtpToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__otp_toke__3213E83FF98F9ED6");

            entity.ToTable("otp_tokens");

            entity.HasIndex(e => new { e.Email, e.Type }, "idx_otp_email_type");

            entity.HasIndex(e => e.ExpiresAt, "idx_otp_expires");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Attempts).HasColumnName("attempts");
            entity.Property(e => e.Code)
                .HasMaxLength(6)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .HasColumnName("email");
            entity.Property(e => e.ExpiresAt).HasColumnName("expiresAt");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Used).HasColumnName("used");
        });

        modelBuilder.Entity<PackagePlan>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__PackageP__322035EC1E38CB71");

            entity.HasIndex(e => e.PackageName, "UQ__PackageP__73856F7A2CAB53B8").IsUnique();

            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.PackageName).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<RevokedToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RevokedT__3213E83F7EBAE82A");

            entity.ToTable("RevokedToken");

            entity.HasIndex(e => e.TokenHash, "idx_revoked_token_hash");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpiresAt).HasColumnName("expiresAt");
            entity.Property(e => e.TokenHash)
                .HasMaxLength(64)
                .HasColumnName("tokenHash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Station__E0D8A6DDD8BF51F8");

            entity.ToTable("Station");

            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Contact).HasMaxLength(50);
            entity.Property(e => e.StationName).HasMaxLength(100);
            entity.Property(e => e.StationStatus).HasMaxLength(20);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4B8F1549B3");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Record).HasMaxLength(200);
            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("PENDING");
            entity.Property(e => e.TimeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionRef).HasMaxLength(100);
            entity.Property(e => e.TransactionType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_Transactions_Booking");

            entity.HasOne(d => d.Package).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__Transacti__Packa__656C112C");

            entity.HasOne(d => d.Station).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Stati__6477ECF3");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__UserI__6383C8BA");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC019D7D0F");

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456F7A84BDB").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Station).WithMany(p => p.Users)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK_Users_Station");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicle__476B54B2036752B8");

            entity.ToTable("Vehicle");

            entity.HasIndex(e => e.Vin, "UQ__Vehicle__C5DF234C631F60D4").IsUnique();

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.BatteryType).HasMaxLength(50);
            entity.Property(e => e.RegisterInformation).HasMaxLength(200);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VehicleModel).HasMaxLength(100);
            entity.Property(e => e.Vin)
                .HasMaxLength(50)
                .HasColumnName("VIN");

            entity.HasOne(d => d.User).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehicle__UserID__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
