using System;
using System.ComponentModel;

namespace LearnerProfile.app.Models;

public enum UserRole {Admin, Teacher, Parent}

public class User
{
    public Guid Id { get; set; } // primary key
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public UserRole Role { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // navigation properties for EFCore
    // both nullable since one user can only have one role
    public Parent? ParentProfile { get; set; }
    public Teacher? TeacherProfile { get; set; }
}


public class Parent
{
    public Guid Id { get; set; }

    // foreign key
    public Guid UserId { get; set; }
    public User User { get; set; } = null!; // null-forgiving operator since EFCore will populate this

    // parent profile
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string ContactNumber { get; set; } = "";

    // each parent can have more than 1 child enrolled
    public ICollection<Student> Students { get; set; } = [];
}


public class Student
{
    public Guid Id { get; set; }

    // foreign key
    public Guid ParentId { get; set; }
    public Parent Parent { get; set; } = null!;

    // student profile
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string IdNumber { get; set; } = ""; // Student ID Number found on physical IDs
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = "";
    public string EnrollmentStatus { get; set; } = "";
    public string GuardianFullName { get; set; } = "";
    public string GuardianRelationship { get; set; } = "";
    public string GuardianContactNumber { get; set; } = "";
    public string GuardianEmailAddress { get; set; } = "";
}


public class Teacher
{
    public Guid Id { get; set; }

    // foreign key
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    // teacher profile
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string ContactNumber { get; set; } = "";
    public string IdNumber { get; set; } = ""; // employee ID
}
