using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Slonik.Models;


namespace Slonik.BaseContex
{
    public class ContexSlonik : DbContext
    {
        //Создание и вставка таблиц
        public DbSet<Detal> detals { get; set; }
        public DbSet<Coefic> coefics { get; set; }
        public DbSet<Arhiv> arhivs { get; set; }
        public DbSet<Users> users { get; set; }
        public ContexSlonik() : base("SlonikConnection") { } //имя строки подключения
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //установка ключеий и установка связи один ко многим
            modelBuilder.Entity<Detal>().HasKey(a => a.Id);
            modelBuilder.Entity<Detal>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Coefic>().HasKey(a => a.Id);

            modelBuilder.Entity<Coefic>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Detal>().HasRequired<Coefic>(x => x.Coefic).WithMany(s => s.Detals).HasForeignKey(x => x.CoeficId).WillCascadeOnDelete(false);
        }
    }
}