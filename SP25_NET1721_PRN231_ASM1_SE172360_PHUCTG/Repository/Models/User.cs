﻿namespace Repository.Models;

public class User
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public bool? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string ImageUrl { get; set; }

    public string PhoneNumber { get; set; }

    public string PasswordHash { get; set; }

    public string RoleName { get; set; }

    public string Address { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public int? RoleId { get; set; }

    public virtual Role Role { get; set; }
}