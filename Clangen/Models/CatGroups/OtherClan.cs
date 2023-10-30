﻿using System.Collections.Generic;
using Clangen.Models.CatStuff;

namespace Clangen.Models.CatGroups;

public class OtherClan : Group
{
    public OtherClan(string id, IReadOnlyDictionary<string, Cat> allCats, string prefix, string? leader = null, string? deputy = null,
        List<string>? medicineCats = null) : base(id, allCats)
    {
        this.prefix = prefix;
        this.leader = leader;
        this.deputy = deputy;
        if (medicineCats != null)
        {
            this.medicineCats = medicineCats;
        }
    }

    public string prefix { get; set; }
    public string suffix { get; set; } = "Clan";

    public override string GetName()
    {
        return $"{prefix}{suffix}";
    }

    public override void UpdateCatStatus(Cat cat, Cat.CatStatus oldStatus)
    {
        throw new System.NotImplementedException();
    }

    public string? leader { get; set; }
    public string? deputy { get; set; }
    public List<string> medicineCats { get; set; } = new();
}