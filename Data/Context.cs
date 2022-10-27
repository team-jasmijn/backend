using Data.Models;
using Microsoft.EntityFrameworkCore;
using Action = Data.Models.Action;
using File = Data.Models.File;

namespace Data
{
    public class Context : DbContext
    {
        public Context()
        {
        }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>().HasOne(e => e.Company);
            modelBuilder.Entity<Chat>().HasOne(e => e.Student);

            modelBuilder.Entity<Flirt>().HasOne(e => e.Company);
            modelBuilder.Entity<Flirt>().HasOne(e => e.Student);

            modelBuilder.Entity<User>().HasMany(e => e.Chats);
            modelBuilder.Entity<User>().HasMany(e => e.Flirts);
            modelBuilder.Entity<User>().HasMany(e => e.Files);
            modelBuilder.Entity<User>().HasMany(e => e.ProfileSettings);
            modelBuilder.Entity<User>().HasMany(e => e.SentChatMessages);
            //Entity frameworks flips when configuring this via Attribute modeling, so leave this
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ProfileSetting> Profilesettings { get; set; }
        public DbSet<ProfileSettingOption> ProfilesettingsOptions { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Action> Actions { get; set; }

        public DbSet<Flirt> Flirts { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }


        public DbSet<File> Files { get; set; }
        


    }
}