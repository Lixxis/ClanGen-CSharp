﻿using System.Collections.Generic;
using Clangen.Models.CatStuff;

namespace Clangen.Models.CatGroups;

public class Outsiders : Group
{
    private string _name;

    public override string GetName()
    {
        return _name;
    }
    
    public Outsiders(IReadOnlyFetchableObject<string, Cat> allCats, 
        string name = "Cats Outside the Clan") : base(false, allCats)
    {
        this._name = name;
    }
}