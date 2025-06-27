using Microsoft.EntityFrameworkCore;
using MyMiniBank.Api.Models.Entities;
namespace MyMiniBank.Api.Models.DataBaseContext
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<TransferTransaction> TransferTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //設定BankAccount和User之間的關聯
            modelBuilder.Entity<BankAccount>()
                .HasOne(b => b.User)
                .WithMany(u => u.BankAccounts)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade); // 設定刪除行為為級聯刪除(Cascade Delete)，刪除使用者時，相關的銀行帳戶也會被刪除。
            //設定BankAccount的唯一索引規則
            modelBuilder.Entity<BankAccount>()
                .HasIndex(b => b.AccountNumber)
                .IsUnique();
            modelBuilder.Entity<BankAccount>()
                .Property(b => b.AccountType)
                .HasConversion<string>(); // 將AccountType枚舉轉換為字串存儲
            modelBuilder.Entity<BankAccount>()
                .Property(b => b.Status)
                .HasConversion<string>(); // 將AccountStatus枚舉轉換為字串存儲

            //設定User的唯一索引規則
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            //設定User的Email唯一索引規則
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            //TransferTransaction 關聯設定
            modelBuilder.Entity<TransferTransaction>()
                .HasOne(t => t.FromAccount)
                .WithMany()
                .HasForeignKey(t => t.FromAccountId)
                .OnDelete(DeleteBehavior.Restrict); // 不允許刪除來源帳戶

            modelBuilder.Entity<TransferTransaction>()
                .HasOne(t => t.ToAccount)
                .WithMany()
                .HasForeignKey(t => t.ToAccountId)
                .OnDelete(DeleteBehavior.Restrict); // 不允許刪除目標帳戶

            //設定TransferTransaction的唯一索引規則
            modelBuilder.Entity<TransferTransaction>()
                .HasIndex(t=>t.Id)
                .IsUnique();

            //設定表名稱
            modelBuilder.Entity<BankAccount>().ToTable("BankAccounts");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<TransferTransaction>().ToTable("TransferTransactions");

            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // 在保存之前可以添加一些邏輯，例如自動填充CreatedAt和UpdatedAt屬性
            foreach (var entry in ChangeTracker.Entries<BankAccount>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            foreach (var entry in ChangeTracker.Entries<User>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}