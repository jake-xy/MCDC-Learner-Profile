using System;
using LearnerProfile.app.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnerProfile.app.Data;

public class LearnerProfileContext(DbContextOptions<LearnerProfileContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
}
