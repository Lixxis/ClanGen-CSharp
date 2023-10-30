﻿using System.Collections.Generic;
using Clangen.Models.CatStuff;

namespace Clangen.Models.CatGroups;

public class Afterlife : Group
{
    private string _name; 
    
    public override string GetName()
    {
        return _name;
    }

    public override void UpdateCatStatus(Cat cat, Cat.CatStatus oldStatus)
    {
        throw new System.NotImplementedException();
    }

    public Afterlife(string id, IReadOnlyDictionary<string, Cat> allCats,  string name = "Afterlife", 
        SortedSet<string>? cats = null) : base(id, allCats)
    {
        this._name = name;
    }
}