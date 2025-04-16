using Microsoft.EntityFrameworkCore;
using System;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class DocToDataDBContext : DbContext
    {

        public DocToDataDBContext(DbContextOptions<DocToDataDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyMember> CompanyMembers { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public virtual DbSet<ExpenseSubCategory> ExpenseSubCategories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Merchant> Merchants { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<ReceiptCategory> ReceiptCategories { get; set; }
        public virtual DbSet<ReceiptImage> ReceiptImages { get; set; }
        public virtual DbSet<ReceiptItem> ReceiptItems { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<StockTransaction> StockTransactions { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbConnString = Environment.GetEnvironmentVariable("SqlConnectionString");
                optionsBuilder.UseSqlServer(dbConnString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Companies", "Common");

                entity.HasIndex(e => e.CompanyName, "UQ__Companie__9BCE05DCC5D2C492")
                    .IsUnique();

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(550);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");

                entity.Property(e => e.TelephoneNumber).HasMaxLength(50);

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_Subscriptions");
            });

            modelBuilder.Entity<CompanyMember>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.ToTable("CompanyMembers", "Authentication");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MemberID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyMembers)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyMembers_Companies");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.CompanyMembers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyMembers_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CompanyMembers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyMembers_Users");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Countries", "Common");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.CurrenctId);

                entity.ToTable("Currencies", "Common");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Symbol).HasMaxLength(50);
            });

            modelBuilder.Entity<ExpenseCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("ExpenseCategories", "Common");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExpenseCategories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ExpenseCategories_Users");
            });

            modelBuilder.Entity<ExpenseSubCategory>(entity =>
            {
                entity.HasKey(e => e.SubCategoryId);

                entity.ToTable("ExpenseSubCategories", "Common");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubCategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ExpenseSubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpenseSubCategories_ExpenseCategories");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Items", "Inventory");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Merchant>(entity =>
            {
                entity.ToTable("Merchants", "Common");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.CompanyRegNo).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.TaxCompanyRegNo).HasMaxLength(250);

                entity.Property(e => e.Website).HasMaxLength(250);
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("PaymentTypes", "Common");

                entity.Property(e => e.PaymentType1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("PaymentType");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.ToTable("Receipts", "Receipt");

                entity.Property(e => e.ReceiptId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApprovedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CustomerAddress).HasMaxLength(500);

                entity.Property(e => e.CustomerName).HasMaxLength(150);

                entity.Property(e => e.CustomerPhone).HasMaxLength(50);

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OtherCharge).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReceiptDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiptNumber).HasMaxLength(50);

                entity.Property(e => e.ServiceCharge).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Receipts_Countries");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Receipts_Currencies");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.MerchantId)
                    .HasConstraintName("FK_Receipts_Merchants");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Receipts_PaymentTypes");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Receipts_Status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Receipts_Users");
            });

            modelBuilder.Entity<ReceiptCategory>(entity =>
            {
                entity.ToTable("ReceiptCategories", "Receipt");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReceiptId).HasColumnName("ReceiptID");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.ReceiptCategories)
                    .HasForeignKey(d => d.ReceiptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptCategories_Receipts");
            });

            modelBuilder.Entity<ReceiptImage>(entity =>
            {
                entity.ToTable("ReceiptImages", "Receipt");

                entity.Property(e => e.ReceiptImageId)
                    .HasColumnName("ReceiptImageID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ImagePath).IsRequired();

                entity.Property(e => e.OriginalFileName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.ReceiptImages)
                    .HasForeignKey(d => d.ReceiptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptImages_Receipts");
            });

            modelBuilder.Entity<ReceiptItem>(entity =>
            {
                entity.ToTable("ReceiptItems", "Receipt");

                entity.Property(e => e.ReceiptItemId)
                    .HasColumnName("ReceiptItemID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ItemDescription)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.ReceiptItems)
                    .HasForeignKey(d => d.ReceiptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceiptItems_Receipts");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.ReceiptItems)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK_ReceiptItems_ExpenseSubCategories");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.ReceiptItems)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .HasConstraintName("FK_ReceiptItems_UnitOfMeasures");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles", "Authentication");

                entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61602644367E")
                    .IsUnique();

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status", "Common");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StockTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("StockTransactions", "Inventory");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.StockTransactions)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockTransactions_Items");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.StockTransactions)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockTransactions_TransactionTypes");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("Subscriptions", "Common");

                entity.Property(e => e.SubscriptionId)
                    .HasColumnName("SubscriptionID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.MaxAccounts).HasDefaultValueSql("((5))");

                entity.Property(e => e.SubscriptionName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SubscriptionType>(entity =>
            {
                entity.ToTable("SubscriptionTypes", "Common");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubscriptionName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("TransactionTypes", "Inventory");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UnitOfMeasure>(entity =>
            {
                entity.ToTable("UnitOfMeasures", "Common");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "Authentication");

                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FailedLoginAttempts).HasDefaultValueSql("((0))");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastLoginAt).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.LockoutEnd).HasColumnType("datetime");

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
