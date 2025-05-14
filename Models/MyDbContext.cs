using Microsoft.EntityFrameworkCore;
namespace apibase.Models
{
    public class MyDbContext : DbContext
    { 
        public DbSet<UserModel> Users { get; set; } // таблица 
        public DbSet<ScheduleModel> Schedule { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<LessonType> Lesson_Type { get; set; }
        public DbSet<Classroom> Classroom { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<VisitModel> Visits { get; set; }
        public DbSet<Semester> Semester { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связей для ScheduleItem

            // 1. Связь с преподавателем (UserModel)
            modelBuilder.Entity<ScheduleModel>()
                .HasOne(s => s.Teacher)
                .WithMany()
                .HasForeignKey(s => s.User_Id)
                .OnDelete(DeleteBehavior.Restrict); // Или Cascade в зависимости от требований

            // 2. Связь с предметом (Subject)
            modelBuilder.Entity<ScheduleModel>()
                .HasOne(s => s.Subject)
                .WithMany() // Если у Subject есть коллекция ScheduleItems
                .HasForeignKey(s => s.Subject_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
        .HasOne(s => s.LessonType)
        .WithMany() // Если нет обратной навигации
        .HasForeignKey(s => s.LessonTypeId);


            // 4. Связь с аудиторией (Classroom)
            modelBuilder.Entity<ScheduleModel>()
                .HasOne(s => s.Classroom)
                .WithMany() // Если у Classroom есть коллекция ScheduleItems
                .HasForeignKey(s => s.Classroom_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // 5. Связь с группой (Group)
            modelBuilder.Entity<ScheduleModel>()
                .HasOne(s => s.Group)
                .WithMany() // Если у Group есть коллекция ScheduleItems
                .HasForeignKey(s => s.Group_Id)
                .OnDelete(DeleteBehavior.Restrict);


            // Конфигурация Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasOne(s => s.Group)
                      .WithMany()
                      .HasForeignKey(s => s.GroupId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Конфигурация Visit
            modelBuilder.Entity<VisitModel>(entity =>
            {
                entity.HasOne(v => v.Student)
                      .WithMany()
                      .HasForeignKey(v => v.Student_Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(v => v.Group)
                      .WithMany()
                      .HasForeignKey(v => v.Group_Id)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //// Конфигурация Semester
            //modelBuilder.Entity<Semester>(entity =>
            //{
            //    entity.ToTable("semester");
            //    entity.HasKey(e => e.Semester_Id);

            //    entity.Property(e => e.Semester_Name)
            //        .IsRequired()
            //        .HasMaxLength(100);

            //    // Односторонняя связь с Group (без обратной навигации)
            //    entity.HasOne<Group>()
            //          .WithMany()
            //          .HasForeignKey(s => s.Group_Id)
            //          .OnDelete(DeleteBehavior.Restrict);
            //});

            modelBuilder.Entity<Semester>()
    .HasOne(s => s.Group)
    .WithMany()
    .HasForeignKey(s => s.Group_Id);

        }
    }
}
