﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Data.Models.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("Id", "CreateDate", "ModifyDate");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("Data.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("StudentId");

                    b.HasIndex("UserId");

                    b.HasIndex("Id", "CreateDate", "ModifyDate");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Data.Models.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("SenderId");

                    b.HasIndex("Id", "CreateDate", "ModifyDate");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("Data.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Source")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Id", "CreateDate", "ModifyDate");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Data.Models.Flirt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("StudentId");

                    b.HasIndex("UserId");

                    b.HasIndex("Id", "CreateDate", "ModifyDate");

                    b.ToTable("Flirts");
                });

            modelBuilder.Entity("Data.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Id", "CreateDate", "ModifyDate");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Data.Models.ProfileSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Key")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ProfileSettingOptionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ProfileSettingOptionId");

                    b.HasIndex("UserId");

                    b.HasIndex("Id", "CreateDate", "ModifyDate", "Value", "Key");

                    b.ToTable("Profilesettings");
                });

            modelBuilder.Entity("Data.Models.ProfileSettingOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Key")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Id", "CreateDate", "ModifyDate");

                    b.ToTable("ProfilesettingsOptions");

                    b.HasData(
                        new
                        {
                            Id = -16,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "Description"
                        },
                        new
                        {
                            Id = -17,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "Education"
                        },
                        new
                        {
                            Id = -18,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "Goals"
                        },
                        new
                        {
                            Id = -19,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "Experience"
                        },
                        new
                        {
                            Id = -20,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "School"
                        },
                        new
                        {
                            Id = -21,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "EducationLevel"
                        },
                        new
                        {
                            Id = -22,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "Qualities"
                        },
                        new
                        {
                            Id = -23,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "Hobbies"
                        },
                        new
                        {
                            Id = -24,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "WorkCulture"
                        },
                        new
                        {
                            Id = -25,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "CompanyName"
                        },
                        new
                        {
                            Id = -26,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "City"
                        },
                        new
                        {
                            Id = -27,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "LookingFor"
                        },
                        new
                        {
                            Id = -28,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "WorkWise"
                        },
                        new
                        {
                            Id = -29,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "ActiveIn"
                        },
                        new
                        {
                            Id = -30,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Key = "Recap"
                        });
                });

            modelBuilder.Entity("Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Id", "CreateDate", "ModifyDate");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Approved")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Hash")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Salt")
                        .HasColumnType("longtext");

                    b.Property<string>("TimeZoneId")
                        .HasColumnType("longtext");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id", "CreateDate", "ModifyDate");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Approved = false,
                            CreateDate = new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "artimmerman@landstede.nl",
                            Hash = "$2a$11$A1PAL2tcek6yMqg8VVVzauteuOFGnD1S4DoqlPP/Hf9ulBwfJTS8y",
                            Name = "Arjan Timmerman",
                            Salt = "$2a$11$FIhli04K3CDnTp4ObcYIb.",
                            TimeZoneId = "Africa/Abidjan",
                            UserType = 4
                        });
                });

            modelBuilder.Entity("Data.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.HasIndex("Id", "CreateDate", "ModifyDate");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Data.Models.Action", b =>
                {
                    b.HasOne("Data.Models.Module", "Module")
                        .WithMany("Actions")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("Data.Models.Chat", b =>
                {
                    b.HasOne("Data.Models.User", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.User", null)
                        .WithMany("Chats")
                        .HasForeignKey("UserId");

                    b.Navigation("Company");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Data.Models.ChatMessage", b =>
                {
                    b.HasOne("Data.Models.Chat", "Chat")
                        .WithMany("ChatMessages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.User", "Sender")
                        .WithMany("SentChatMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Data.Models.File", b =>
                {
                    b.HasOne("Data.Models.User", "Company")
                        .WithMany("Files")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Data.Models.Flirt", b =>
                {
                    b.HasOne("Data.Models.User", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.User", null)
                        .WithMany("Flirts")
                        .HasForeignKey("UserId");

                    b.Navigation("Company");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Data.Models.Module", b =>
                {
                    b.HasOne("Data.Models.Role", "Role")
                        .WithMany("Modules")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Data.Models.ProfileSetting", b =>
                {
                    b.HasOne("Data.Models.ProfileSettingOption", "ProfileSettingOption")
                        .WithMany("ProfileSettings")
                        .HasForeignKey("ProfileSettingOptionId");

                    b.HasOne("Data.Models.User", "User")
                        .WithMany("ProfileSettings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProfileSettingOption");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.UserRole", b =>
                {
                    b.HasOne("Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.Chat", b =>
                {
                    b.Navigation("ChatMessages");
                });

            modelBuilder.Entity("Data.Models.Module", b =>
                {
                    b.Navigation("Actions");
                });

            modelBuilder.Entity("Data.Models.ProfileSettingOption", b =>
                {
                    b.Navigation("ProfileSettings");
                });

            modelBuilder.Entity("Data.Models.Role", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("Files");

                    b.Navigation("Flirts");

                    b.Navigation("ProfileSettings");

                    b.Navigation("Roles");

                    b.Navigation("SentChatMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
