﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250104160238_AddGymReportEntity")]
    partial class AddGymReportEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApplicationCore.Entities.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceptionistId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReceptionistId");

                    b.ToTable("ReceptionistsAvailabilities");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RepetitionsNumber")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("RestTime")
                        .HasColumnType("time");

                    b.Property<int>("SeriesNumber")
                        .HasColumnType("int");

                    b.Property<int>("TrainingPlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrainingPlanId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GroupTrainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EmploymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GroupTrainers");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GroupTraining", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<int>("GroupTrainerId")
                        .HasColumnType("int");

                    b.Property<int>("MaxParticipantNumber")
                        .HasColumnType("int");

                    b.Property<int>("TrainingTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupTrainerId");

                    b.HasIndex("TrainingTypeId");

                    b.ToTable("GroupTrainings");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GroupTrainingParticipation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("GroupTrainingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("GroupTrainingId");

                    b.ToTable("GroupTrainingParticipations");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GymMembership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MembershipPlanId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("MembershipPlanId");

                    b.ToTable("GymMemberships");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GymReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NewClients")
                        .HasColumnType("int");

                    b.Property<int?>("NewMemberships")
                        .HasColumnType("int");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("TotalEmployeesCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TotalGroupTrainings")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("TotalGroupTrainingsTime")
                        .HasColumnType("time");

                    b.Property<decimal?>("TotalIncome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TotalIndividualTrainings")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("TotalIndividualTrainingsTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("GymReports");
                });

            modelBuilder.Entity("ApplicationCore.Entities.IndividualTraining", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<int>("PersonalTrainerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("PersonalTrainerId");

                    b.ToTable("IndividualTrainings");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EmploymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("ApplicationCore.Entities.MembershipPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("CanReserveTrainings")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationInMonths")
                        .HasColumnType("int");

                    b.Property<string>("DurationTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MembershipPlans");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EmploymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GymMembershipId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GymMembershipId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("ApplicationCore.Entities.PersonalTrainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EmploymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PersonalTrainers");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Receptionist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EmploymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Receptionists");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Shift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceptionistId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReceptionistId");

                    b.ToTable("ReceptionistsShifts");
                });

            modelBuilder.Entity("ApplicationCore.Entities.TrainingPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonalTrainerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("PersonalTrainerId");

                    b.ToTable("TrainingPlans");
                });

            modelBuilder.Entity("ApplicationCore.Entities.TrainingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TrainingTypes");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Availability", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Receptionist", "Receptionist")
                        .WithMany("Availabilities")
                        .HasForeignKey("ReceptionistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receptionist");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Exercise", b =>
                {
                    b.HasOne("ApplicationCore.Entities.TrainingPlan", "TrainingPlan")
                        .WithMany("Exercises")
                        .HasForeignKey("TrainingPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainingPlan");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GroupTraining", b =>
                {
                    b.HasOne("ApplicationCore.Entities.GroupTrainer", "GroupTrainer")
                        .WithMany("Trainings")
                        .HasForeignKey("GroupTrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.TrainingType", "TrainingType")
                        .WithMany()
                        .HasForeignKey("TrainingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupTrainer");

                    b.Navigation("TrainingType");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GroupTrainingParticipation", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Client", "Client")
                        .WithMany("GroupTrainingsParticipations")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.GroupTraining", "GroupTraining")
                        .WithMany("Participations")
                        .HasForeignKey("GroupTrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("GroupTraining");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GymMembership", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Client", "Client")
                        .WithMany("GymMemberships")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.MembershipPlan", "MembershipPlan")
                        .WithMany()
                        .HasForeignKey("MembershipPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("MembershipPlan");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GymReport", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Owner", "Owner")
                        .WithMany("GymReports")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ApplicationCore.Entities.IndividualTraining", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Client", "Client")
                        .WithMany("IndividualTrainings")
                        .HasForeignKey("ClientId");

                    b.HasOne("ApplicationCore.Entities.PersonalTrainer", "PersonalTrainer")
                        .WithMany("Trainings")
                        .HasForeignKey("PersonalTrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("PersonalTrainer");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Payment", b =>
                {
                    b.HasOne("ApplicationCore.Entities.GymMembership", "GymMembership")
                        .WithMany("Payments")
                        .HasForeignKey("GymMembershipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GymMembership");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Shift", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Receptionist", "Receptionist")
                        .WithMany("Shifts")
                        .HasForeignKey("ReceptionistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receptionist");
                });

            modelBuilder.Entity("ApplicationCore.Entities.TrainingPlan", b =>
                {
                    b.HasOne("ApplicationCore.Entities.Client", "Client")
                        .WithMany("TrainingPlans")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.PersonalTrainer", "PersonalTrainer")
                        .WithMany("Plans")
                        .HasForeignKey("PersonalTrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("PersonalTrainer");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Client", b =>
                {
                    b.Navigation("GroupTrainingsParticipations");

                    b.Navigation("GymMemberships");

                    b.Navigation("IndividualTrainings");

                    b.Navigation("TrainingPlans");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GroupTrainer", b =>
                {
                    b.Navigation("Trainings");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GroupTraining", b =>
                {
                    b.Navigation("Participations");
                });

            modelBuilder.Entity("ApplicationCore.Entities.GymMembership", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Owner", b =>
                {
                    b.Navigation("GymReports");
                });

            modelBuilder.Entity("ApplicationCore.Entities.PersonalTrainer", b =>
                {
                    b.Navigation("Plans");

                    b.Navigation("Trainings");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Receptionist", b =>
                {
                    b.Navigation("Availabilities");

                    b.Navigation("Shifts");
                });

            modelBuilder.Entity("ApplicationCore.Entities.TrainingPlan", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
