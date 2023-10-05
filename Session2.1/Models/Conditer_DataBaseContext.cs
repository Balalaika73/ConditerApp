using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Session2.Models
{
    public partial class Conditer_DataBaseContext : DbContext
    {
        public Conditer_DataBaseContext()
        {
        }

        public Conditer_DataBaseContext(DbContextOptions<Conditer_DataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CakeDecorationSpecification> CakeDecorationSpecifications { get; set; } = null!;
        public virtual DbSet<Decoration> Decorations { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<Ingridient> Ingridients { get; set; } = null!;
        public virtual DbSet<IngridientsSpecification> IngridientsSpecifications { get; set; } = null!;
        public virtual DbSet<OperationSpecification> OperationSpecifications { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<SemimanufacturesSpecification> SemimanufacturesSpecifications { get; set; } = null!;
        public virtual DbSet<Supplyer> Supplyers { get; set; } = null!;
        public virtual DbSet<TypeEquipment> TypeEquipments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-3IGPDRB\\SQLEXPRESS;Initial Catalog=Conditer_DataBase;User ID=sa;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CakeDecorationSpecification>(entity =>
            {
                entity.HasKey(e => new { e.NameProductDec, e.CodeDec })
                    .HasName("PK_Cake_Decoration_Specification_Specification");

                entity.ToTable("Cake_Decoration_Specification");

                entity.Property(e => e.NameProductDec)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Product_Dec");

                entity.Property(e => e.CodeDec)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Code_Dec");

                entity.Property(e => e.DecSpQuantity).HasColumnName("Dec_Sp_Quantity");

                entity.HasOne(d => d.CodeDecNavigation)
                    .WithMany(p => p.CakeDecorationSpecifications)
                    .HasForeignKey(d => d.CodeDec)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Name_Dec_Sp");

                entity.HasOne(d => d.NameProductDecNavigation)
                    .WithMany(p => p.CakeDecorationSpecifications)
                    .HasForeignKey(d => d.NameProductDec)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Name_Product_Dec_Sp");
            });

            modelBuilder.Entity<Decoration>(entity =>
            {
                entity.HasKey(e => e.DecVendorCode)
                    .HasName("PK__Decorati__759FCB7B9BA33E20");

                entity.ToTable("Decoration");

                entity.Property(e => e.DecVendorCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Dec_Vendor_Code");

                entity.Property(e => e.DecPicture)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Dec_Picture");

                entity.Property(e => e.DecPurchasePrice).HasColumnName("Dec_Purchase_Price");

                entity.Property(e => e.DecQuantity).HasColumnName("Dec_Quantity");

                entity.Property(e => e.DecUnit)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Dec_Unit");

                entity.Property(e => e.DecWeight)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Dec_Weight");

                entity.Property(e => e.MainSupplyer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Main_Supplyer");

                entity.Property(e => e.NameDec)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Name_Dec");

                entity.Property(e => e.TypeDec)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Type_Dec");

                entity.HasOne(d => d.MainSupplyerNavigation)
                    .WithMany(p => p.Decorations)
                    .HasForeignKey(d => d.MainSupplyer)
                    .HasConstraintName("FK_Main_Supplyer");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.Marking)
                    .HasName("PK__Equipmen__37333F4796DF64F7");

                entity.Property(e => e.Marking)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.EquipCharacteristic)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Equip_Characteristic");

                entity.Property(e => e.TypeEquip)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Type_Equip");

                entity.HasOne(d => d.TypeEquipNavigation)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.TypeEquip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Type_Equipment");
            });

            modelBuilder.Entity<Ingridient>(entity =>
            {
                entity.HasKey(e => e.IngrVendorCode)
                    .HasName("PK__Ingridie__742A010F1F3F630E");

                entity.ToTable("Ingridient");

                entity.Property(e => e.IngrVendorCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Ingr_Vendor_Code");

                entity.Property(e => e.Gost)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IngrCharacteristic)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Ingr_Characteristic");

                entity.Property(e => e.IngrPackaging)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Ingr_Packaging");

                entity.Property(e => e.IngrPicture)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Ingr_Picture");

                entity.Property(e => e.IngrPurchasePrice).HasColumnName("Ingr_Purchase_Price");

                entity.Property(e => e.IngrQuantity).HasColumnName("Ingr_Quantity");

                entity.Property(e => e.IngrUnit)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Ingr_Unit");

                entity.Property(e => e.MainSupplyer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Main_Supplyer");

                entity.Property(e => e.NameIngr)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Name_Ingr");

                entity.Property(e => e.TypeIngr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Type_Ingr");

                entity.HasOne(d => d.MainSupplyerNavigation)
                    .WithMany(p => p.Ingridients)
                    .HasForeignKey(d => d.MainSupplyer)
                    .HasConstraintName("FK_Main_Ingr_Supplyer");
            });

            modelBuilder.Entity<IngridientsSpecification>(entity =>
            {
                entity.HasKey(e => new { e.IngrCode, e.NameProductSp });

                entity.ToTable("Ingridients_Specification");

                entity.Property(e => e.IngrCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Ingr_Code");

                entity.Property(e => e.NameProductSp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Product_Sp");

                entity.Property(e => e.IngrSpQuantity).HasColumnName("Ingr_Sp_Quantity");

                entity.HasOne(d => d.IngrCodeNavigation)
                    .WithMany(p => p.IngridientsSpecifications)
                    .HasForeignKey(d => d.IngrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Name_Ingr_Sp");

                entity.HasOne(d => d.NameProductSpNavigation)
                    .WithMany(p => p.IngridientsSpecifications)
                    .HasForeignKey(d => d.NameProductSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Name_Product_Ingr_Sp");
            });

            modelBuilder.Entity<OperationSpecification>(entity =>
            {
                entity.HasKey(e => new { e.SpOpProduct, e.NameOperation, e.SerialNumber });

                entity.ToTable("Operation_Specification");

                entity.Property(e => e.SpOpProduct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Sp_Op_Product");

                entity.Property(e => e.NameOperation)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Name_Operation");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Serial_Number");

                entity.Property(e => e.TimeForOperation)
                    .HasColumnType("datetime")
                    .HasColumnName("Time_For_Operation");

                entity.Property(e => e.TypeEquip)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Type_Equip");

                entity.HasOne(d => d.SpOpProductNavigation)
                    .WithMany(p => p.OperationSpecifications)
                    .HasForeignKey(d => d.SpOpProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Op_Product");

                entity.HasOne(d => d.TypeEquipNavigation)
                    .WithMany(p => p.OperationSpecifications)
                    .HasForeignKey(d => d.TypeEquip)
                    .HasConstraintName("FK_Type_Equipment_Op");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.NameProduct)
                    .HasName("PK__Product__0F172890DB86CC3A");

                entity.ToTable("Product");

                entity.Property(e => e.NameProduct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Product");

                entity.Property(e => e.ProdictSize)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Prodict_Size");
            });

            modelBuilder.Entity<SemimanufacturesSpecification>(entity =>
            {
                entity.HasKey(e => new { e.NameProductIngr, e.CodeIngr });

                entity.ToTable("Semimanufactures_Specification");

                entity.Property(e => e.NameProductIngr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Product_Ingr");

                entity.Property(e => e.CodeIngr)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Code_Ingr");

                entity.Property(e => e.IngrSpQuantity).HasColumnName("Ingr_Sp_Quantity");

                entity.HasOne(d => d.CodeIngrNavigation)
                    .WithMany(p => p.SemimanufacturesSpecifications)
                    .HasForeignKey(d => d.CodeIngr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Semimanufactures_Specification_Code_Ingr");

                entity.HasOne(d => d.NameProductIngrNavigation)
                    .WithMany(p => p.SemimanufacturesSpecifications)
                    .HasForeignKey(d => d.NameProductIngr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Semimanufactures_Specification_Name_Product_Ingr");
            });

            modelBuilder.Entity<Supplyer>(entity =>
            {
                entity.HasKey(e => e.NameSupplyer)
                    .HasName("PK__Supplyer__749D9E2C139B8DDD");

                entity.ToTable("Supplyer");

                entity.Property(e => e.NameSupplyer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Supplyer");

                entity.Property(e => e.SupplyDeadline)
                    .HasColumnType("date")
                    .HasColumnName("Supply_Deadline")
                    .HasDefaultValueSql("(format(getdate(),'dd.MM.yyyy'))");

                entity.Property(e => e.SupplyerAddress)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("Supplyer_Address");
            });

            modelBuilder.Entity<TypeEquipment>(entity =>
            {
                entity.HasKey(e => e.NameTypeEquipment)
                    .HasName("PK__Type_Equ__71EBD92E6CD02E98");

                entity.ToTable("Type_Equipment");

                entity.Property(e => e.NameTypeEquipment)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Type_Equipment");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.Login, e.Password });

                entity.ToTable("User");

                entity.Property(e => e.Login)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Fio)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FIO");

                entity.Property(e => e.Photo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
