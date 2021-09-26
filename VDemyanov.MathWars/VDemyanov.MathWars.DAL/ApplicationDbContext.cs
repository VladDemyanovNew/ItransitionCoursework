using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VDemyanov.MathWars.DAL.Models;

namespace VDemyanov.MathWars.Dal
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<MathProblem> MathProblems { get; set; }
        public virtual DbSet<MathProblemTag> MathProblemTags { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
