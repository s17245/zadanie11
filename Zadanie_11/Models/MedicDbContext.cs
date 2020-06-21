using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadanie_11.Controllers;

namespace Zadanie_11.Models
{
    public class MedicDbContext : DbContext
    {

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }

        //konstruktor
        public MedicDbContext() { }

        public MedicDbContext(DbContextOptions options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Medicament>().HasKey(q => q.Id);
            builder.Entity<Prescription>().HasKey(q => q.Id);
            builder.Entity<Prescription_Medicament>().HasKey(q =>
                new
                {
                    q.IdMedicament,
                    q.IdPrescription
                });

            builder.Entity<Prescription_Medicament>()
                .HasOne(t => t.Medicament)
                .WithMany(t => t.Prescription_Medicaments)
                .HasForeignKey(t => t.IdMedicament);

            builder.Entity<Prescription_Medicament>()
                .HasOne(t => t.Prescription)
                .WithMany(t => t.Prescription_Medicaments)
                .HasForeignKey(t => t.IdPrescription);

            
            var doctors = new List<Doctor>();
            doctors.Add(new Doctor { Id = 1, FirstName = "Grzegorz", LastName = "Brzęczyszczykiewicz", Email = "greg@wp.com" });
            doctors.Add(new Doctor { Id = 2, FirstName = "Stefan", LastName = "Burczymucha", Email = "stef323@gmail.com" });
            doctors.Add(new Doctor { Id = 3, FirstName = "User", LastName = "123", Email = "123@gmail.com" });
            doctors.Add(new Doctor { Id = 4, FirstName = "Last", LastName = "User", Email = "user@gmail.com" });

            builder.Entity<Doctor>().HasData(doctors);

            var patients = new List<Patient>();
            patients.Add(new Patient { Id = 1, FirstName = "Zbigniew", LastName = "Alecki", BirthDate = new DateTime(1930, 1, 1) });
            patients.Add(new Patient { Id = 2, FirstName = "Mirosław", LastName = "Babacki", BirthDate = new DateTime(1947, 2, 2) });
            patients.Add(new Patient { Id = 3, FirstName = "Krzystosz", LastName = "Cabacki", BirthDate = new DateTime(1953, 3, 3) });
            patients.Add(new Patient { Id = 4, FirstName = "Maciej", LastName = "Mistrz", BirthDate = new DateTime(1797, 4, 4) });


            builder.Entity<Patient>().HasData(patients);

            var prescriptions = new List<Prescription>();
            prescriptions.Add(new Prescription { Id = 1, Date = new DateTime(2020, 6, 20), DueDate = new DateTime(2021, 6, 20), IdDoctor = 1, IdPatient = 1 });
            prescriptions.Add(new Prescription { Id = 2, Date = new DateTime(2020, 6, 20), DueDate = new DateTime(2021, 6, 20), IdDoctor = 2, IdPatient = 2 });
            prescriptions.Add(new Prescription { Id = 3, Date = new DateTime(2020, 6, 20), DueDate = new DateTime(2021, 6, 20), IdDoctor = 3, IdPatient = 3 });
            prescriptions.Add(new Prescription { Id = 4, Date = new DateTime(2020, 6, 20), DueDate = new DateTime(2021, 6, 20), IdDoctor = 1, IdPatient = 4 });

            builder.Entity<Prescription>().HasData(prescriptions);

            var medicaments = new List<Medicament>();
            medicaments.Add(new Medicament { Id = 1, Name = "Pawulon", Descripiton = "PrzeciwTeściowy", Type = "Paralek" });
            medicaments.Add(new Medicament { Id = 2, Name = "Stoperan", Descripiton = "Wyjazdowy", Type = "Suplement" });
            medicaments.Add(new Medicament { Id = 3, Name = "TurboSesja", Descripiton = "Na ból głowy", Type = "Paralek" });


            builder.Entity<Medicament>().HasData(medicaments);

            var prescriptionsMedicament = new List<Prescription_Medicament>();
            prescriptionsMedicament.Add(new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 1, Details = "Jakieś detale" });
            prescriptionsMedicament.Add(new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 2, Details = "Jakieś detale" });
            prescriptionsMedicament.Add(new Prescription_Medicament { IdMedicament = 3, IdPrescription = 3, Dose = 3, Details = "Jakieś detale" });
            prescriptionsMedicament.Add(new Prescription_Medicament { IdMedicament = 1, IdPrescription = 4, Dose = 4, Details = "Jakieś detale" });

            builder.Entity<Prescription_Medicament>().HasData(prescriptionsMedicament);

            
        }
    }
}
