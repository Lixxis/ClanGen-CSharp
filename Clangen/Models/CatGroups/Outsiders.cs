﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clangen.Models.CatStuff;

namespace Clangen.Models.CatGroups;

public class Outsiders : Group
{
    [JsonInclude]
    private string _name = string.Empty;

    public override string GetName()
    {
        return _name;
    }

    [JsonConstructor]
    public Outsiders(string ID, bool dead) : base(ID, dead, new CatDictionary())
    {

    }

    public Outsiders(string ID, IReadOnlyFetchableObject<string, Cat> allCats, 
        string name = "Cats Outside the Clan") : base(ID, false, allCats)
    {
        this._name = name;
    }
}