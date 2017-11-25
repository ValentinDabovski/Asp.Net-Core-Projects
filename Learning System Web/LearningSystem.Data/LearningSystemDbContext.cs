namespace LearningSystem.Data
{
    using Models.DataModels;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class LearningSystemDbContext : IdentityDbContext<User>
    {
        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StudentCourse>()
                .HasKey(st => new { st.CourseId, st.StudentId });

            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(c => c.Courses)
                .HasForeignKey(sc => sc.StudentId);


            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(s => s.Students)
                .HasForeignKey(sc => sc.CourseId);


            builder.Entity<Course>()
                .HasOne(t => t.Trainer)
                .WithMany(u => u.Trainigs)
                .HasForeignKey(t => t.TrainerId);
      

            builder.Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.AuthorId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
