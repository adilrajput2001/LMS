using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LMS.Models;

public partial class NeondbContext : DbContext
{
    public NeondbContext()
    {
    }

    public NeondbContext(DbContextOptions<NeondbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Laundryorder> Laundryorders { get; set; }

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-restless-frost-a4s7vds0-pooler.us-east-1.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_Zz8tsnqSHey2");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.Complaintid).HasName("complaint_pkey");

            entity.ToTable("complaint");

            entity.Property(e => e.Complaintid)
                .ValueGeneratedNever()
                .HasColumnName("complaintid");
            entity.Property(e => e.Complaintdate).HasColumnName("complaintdate");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Customer).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("complaint_customerid_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("complaint_orderid_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.Property(e => e.Customerid)
                .ValueGeneratedNever()
                .HasColumnName("customerid");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.Deliveryid).HasName("delivery_pkey");

            entity.ToTable("delivery");

            entity.Property(e => e.Deliveryid)
                .ValueGeneratedNever()
                .HasColumnName("deliveryid");
            entity.Property(e => e.Deliverystatus)
                .HasMaxLength(50)
                .HasColumnName("deliverystatus");
            entity.Property(e => e.Deliverytime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deliverytime");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Staffid).HasColumnName("staffid");

            entity.HasOne(d => d.Order).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("delivery_orderid_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.Staffid)
                .HasConstraintName("delivery_staffid_fkey");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Feedbackid).HasName("feedback_pkey");

            entity.ToTable("feedback");

            entity.Property(e => e.Feedbackid)
                .ValueGeneratedNever()
                .HasColumnName("feedbackid");
            entity.Property(e => e.Comments).HasColumnName("comments");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Feedbackdate).HasColumnName("feedbackdate");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Customer).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("feedback_customerid_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("feedback_orderid_fkey");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Itemid).HasName("inventory_pkey");

            entity.ToTable("inventory");

            entity.Property(e => e.Itemid)
                .ValueGeneratedNever()
                .HasColumnName("itemid");
            entity.Property(e => e.Itemname)
                .HasMaxLength(100)
                .HasColumnName("itemname");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Reorderlevel).HasColumnName("reorderlevel");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasColumnName("unit");
        });

        modelBuilder.Entity<Laundryorder>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("laundryorder_pkey");

            entity.ToTable("laundryorder");

            entity.Property(e => e.Orderid)
                .ValueGeneratedNever()
                .HasColumnName("orderid");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Deliverydate).HasColumnName("deliverydate");
            entity.Property(e => e.Orderdate).HasColumnName("orderdate");
            entity.Property(e => e.Pickupdate).HasColumnName("pickupdate");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Totalamount)
                .HasPrecision(10, 2)
                .HasColumnName("totalamount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Laundryorders)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("laundryorder_customerid_fkey");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.HasKey(e => e.Orderdetailid).HasName("orderdetails_pkey");

            entity.ToTable("orderdetails");

            entity.Property(e => e.Orderdetailid)
                .ValueGeneratedNever()
                .HasColumnName("orderdetailid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Serviceid).HasColumnName("serviceid");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Weightinkg)
                .HasPrecision(5, 2)
                .HasColumnName("weightinkg");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("orderdetails_orderid_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Serviceid)
                .HasConstraintName("orderdetails_serviceid_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("payment_pkey");

            entity.ToTable("payment");

            entity.Property(e => e.Paymentid)
                .ValueGeneratedNever()
                .HasColumnName("paymentid");
            entity.Property(e => e.Amountpaid)
                .HasPrecision(10, 2)
                .HasColumnName("amountpaid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Paymentdate).HasColumnName("paymentdate");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(50)
                .HasColumnName("paymentmethod");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("payment_orderid_fkey");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Serviceid).HasName("service_pkey");

            entity.ToTable("service");

            entity.Property(e => e.Serviceid)
                .ValueGeneratedNever()
                .HasColumnName("serviceid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Rateperkg)
                .HasPrecision(10, 2)
                .HasColumnName("rateperkg");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Staffid).HasName("staff_pkey");

            entity.ToTable("staff");

            entity.Property(e => e.Staffid)
                .ValueGeneratedNever()
                .HasColumnName("staffid");
            entity.Property(e => e.Hiredate).HasColumnName("hiredate");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
