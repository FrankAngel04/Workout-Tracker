using System;
using SQLite;

namespace D424.Classes;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [Unique]
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public DateTime AccountCreated { get; set; } = DateTime.Today;
    public int FailedAttempts { get; set; } = 0;
    public DateTime? LastFailedAttempt { get; set; }
    
    public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, workFactor: 8);
    
    public bool VerifyPassword(string enteredPassword) => BCrypt.Net.BCrypt.Verify(enteredPassword, PasswordHash);
}