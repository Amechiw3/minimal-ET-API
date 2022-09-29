using ET_ASP.Models;
using Microsoft.EntityFrameworkCore;

namespace ET_ASP
{
    public class DataModel : DbContext
    {

        public DataModel(DbContextOptions<DataModel> options) : base(options) { }

        public DbSet<category> Categorias { get; set; }
        public DbSet<task> Tareas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<category> categoriesINIT = new()
            {
                new category()
                {
                    categoryID = Guid.Parse("2143bda8-b2c3-4c47-9fa6-0833ca25fda2"),
                    name = "Actividades pendientes"
                },
                new category()
                {
                    categoryID = Guid.Parse("bf7ef96c-b306-45eb-a55d-c54b84cfcbf1"),
                    name = "Actividades personales"
                }
            };
            modelBuilder.Entity<category>(categoria =>
            {
                categoria.ToTable("Category");
                categoria.HasKey(c => c.categoryID);
                categoria.Property(c => c.name).IsRequired().HasMaxLength(150);
                categoria.Property(c => c.description).IsRequired(false);

                categoria.HasData(categoriesINIT);
            });

            List<task> tasksINIT = new()
            {
                new task()
                {
                    taskID = Guid.Parse("7366e86e-628a-4f2e-8f1b-8ede5da47a13"),
                    categoryID = Guid.Parse("2143bda8-b2c3-4c47-9fa6-0833ca25fda2"),
                    prioTask = Prioridad.Media,
                    tittle = "Pago de servicios",
                    createAt = DateTime.Now
                },
                new task()
                {
                    taskID = Guid.Parse("f2f10b2d-f193-41ad-97e1-b3f68bf7ce46"),
                    categoryID = Guid.Parse("bf7ef96c-b306-45eb-a55d-c54b84cfcbf1"),
                    prioTask = Prioridad.Baja,
                    tittle = "Terminar de ver la serie",
                    createAt = DateTime.Now
                }
            };
            modelBuilder.Entity<task>(task => 
            {
                task.ToTable("Task");
                task.HasKey(t => t.taskID);
                task.HasOne(t => t.category).WithMany(c => c.tasks).HasForeignKey(t => t.categoryID);
                task.Property(t => t.tittle).IsRequired().HasMaxLength(200);
                task.Property(t => t.description).IsRequired(false).HasMaxLength(500);
                task.Property(t => t.prioTask).IsRequired();
                task.Property(t => t.createAt);                
                task.Ignore(t => t.resume);

                task.HasData(tasksINIT);
            });


        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
