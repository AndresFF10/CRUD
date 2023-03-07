using System;
using System.Collections.Generic;

namespace CRUD2.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? Phone { get; set; }

    public DateTime WorkingStartDate { get; set; }

    public byte[] Picture { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public float Salary { get; set; }

    public DateTime? LastRevision { get; set; }

    public string? IncreasesSalary { get; set; }

    public string? IncreasesDates { get; set; }
}
