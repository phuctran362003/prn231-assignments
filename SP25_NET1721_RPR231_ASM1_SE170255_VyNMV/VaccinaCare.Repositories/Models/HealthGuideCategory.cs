﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace VaccinaCare.Repositories.Models;

public partial class HealthGuideCategory
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<HealthGuide>? HealthGuides { get; set; } = new List<HealthGuide>();
}