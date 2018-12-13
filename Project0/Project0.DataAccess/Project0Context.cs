using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project0.DataAccess
{
    public partial class Project0Context : DbContext
    {
        public Project0Context()
        {
        }

        public Project0Context(DbContextOptions<Project0Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Content> Content { get; set; }
        public virtual DbSet<ContentIngredient> ContentIngredient { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Locationingredient> Locationingredient { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderContent> OrderContent { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Content>(entity =>
            {
                entity.ToTable("Content", "PZ");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<ContentIngredient>(entity =>
            {
                entity.HasKey(e => new { e.ContentId, e.IngredientId });

                entity.ToTable("ContentIngredient", "PZ");

                entity.HasOne(d => d.Content)
                    .WithMany(p => p.ContentIngredient)
                    .HasForeignKey(d => d.ContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContentIngredientContent");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.ContentIngredient)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContentIngredientIngredient");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient", "PZ");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "PZ");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Locationingredient>(entity =>
            {
                entity.HasKey(e => new { e.IngredientId, e.LocationId })
                    .HasName("PK_LocationIngredient");

                entity.ToTable("Locationingredient", "PZ");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.Locationingredient)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationIngredientIngredient");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Locationingredient)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationIngredientLocation");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "PZ");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_OrderLocation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_OrderUser");
            });

            modelBuilder.Entity<OrderContent>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ContentId });

                entity.ToTable("OrderContent", "PZ");

                entity.HasOne(d => d.Content)
                    .WithMany(p => p.OrderContent)
                    .HasForeignKey(d => d.ContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderContentContent");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderContent)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderContentOrder");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "PZ");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.HasOne(d => d.DefaultLocation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.DefaultLocationId)
                    .HasConstraintName("FK_UserLocation");
            });
        }
    }
}
