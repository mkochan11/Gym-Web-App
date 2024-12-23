﻿using ApplicationCore.Entities;
using ApplicationCore.Entities.Abstract;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Receptionist> Receptionists{ get; set; }
        public DbSet<Manager> Managers{ get; set; }
        public DbSet<Owner> Owners{ get; set; }
        public DbSet<PersonalTrainer> PersonalTrainers { get; set; }
        public DbSet<GroupTrainer> GroupTrainers { get; set; }
        public DbSet<IndividualTraining> IndividualTrainings { get; set; }
        public DbSet<GroupTraining> GroupTrainings { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<MembershipPlan> MembershipPlans { get; set; }
        public DbSet<GymMembership> GymMemberships { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TrainingType> TrainingTypes { get; set; }
        public DbSet<Shift<Receptionist>> ReceptionistsShifts { get; set; }
        public DbSet<Availability<Receptionist>> ReceptionistsAvailabilities { get; set; }
    }
}
