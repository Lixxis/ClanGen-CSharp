﻿using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Clangen.Models.CatStuff;

public partial class Cat : IEquatable<Cat>
{

    // PRIVATE INSTANCE ATTRIBUTES
    private CatStatus _status = new();
    private int _moons;
    private string? _mentor;
    private List<string> _apprentices = new();

    //PUBLIC ATTRIBUTES and PROPERTIES

    /// <summary>
    /// String ID to idenify a cat
    /// </summary>
    public readonly string Id;
    
    /// <summary>
    /// Name object, holding information on the cat's name.  
    /// </summary>
    public Name name { get; set; }
    
    /// <summary>
    /// Holds a lot of information on a cat's looks. Used in sprite generation. 
    /// </summary>
    public Pelt pelt { get; set; }

    /// <summary>
    /// Holds a cat's trait, and personality facets. 
    /// </summary>
    public Personality personality { get; set; }

    /// <summary>
    /// Holds a cat's skills, and methods for advancing a cat's skills
    /// </summary>
    public CatSkills skills { get; set; }

    /// <summary>
    /// The biological sex of a cat. Male or Female. 
    /// </summary>
    public readonly CatSex sex;

    /// <summary>
    /// String representing the gender of a cat. 
    /// </summary>
    public string gender { get; set; }

    /// <summary>
    /// List of cat Ids. Holds the biological parents. 
    /// </summary>
    public readonly List<string> bioParents;
    
    /// <summary>
    /// List of cat Ids. Holds the adoptive parents. 
    /// </summary>
    public List<string> adoptiveParents { get; set; }
    /// <summary>
    /// Boolian indicating if the cat is outside the clan. 
    /// </summary>
    public bool outside { get; set; }
    /// <summary>
    /// Boolian indicating if a cat is in the Dark Forest
    /// </summary>
    public bool darkForest { get; set; }
    /// <summary>
    /// Boolian indicating if a cat is dead
    /// </summary>
    public bool dead { get; private set; }
    /// <summary>
    /// Current number of lives that a cat has. For most cats, this should always be 1 or 0. 
    /// </summary>
    public int lives { get; private set; } = 1;
    /// <summary>
    /// Holds relationships to other cats. Key values are cat ids, which are linked to the relationship object. 
    /// </summary>
    public Dictionary<string, Relationship> relationships { get; set; }
    /// <summary>
    /// Age of the cat, in moons. 
    /// </summary>
    public int moons
    {
        get { return _moons; }
        set
        {
            _moons = value;
            Console.WriteLine("TODO - set AGE");
        }
    }
    /// <summary>
    /// Status or Rank of the cat. 
    /// </summary>
    public CatStatus status
    {
        get { return _status; }
        set { ChangeStatus(value); }
    }

    // TODO-- proper setters.
    /// <summary>
    /// Cat's mentor. Can be null to indicate that a cat doesn't have a mentor
    /// </summary>
    public string? mentor
    {
        get { return _mentor; }
        set { _mentor = value; }
    }
    /// <summary>
    /// List of current apprentices. 
    /// </summary>
    public List<string> apprentice
    {
        get { return _apprentices; }
        set { _apprentices = value; }
    }

    /// <summary>
    /// List of previous apprentices.
    /// </summary>
    public List<string> previousApprentice { get; set; } = new();
    /// <summary>
    /// List of current mates. 
    /// </summary>
    public List<string> mates { get; private set; } = new();
    /// <summary>
    /// List of previous mates. 
    /// </summary>
    public List<string> previousMates { get; private set; }

    private int _experience;
    /// <summary>
    /// Cat experience. 
    /// </summary>
    public int experience
    {
        get { return _experience; }
        set
        {
            _experience = value;
            //TODO - properly set EXP level
            experienceLevel = ExpLevel.Untrained;
        }
    }
    /// <summary>
    /// Current experence level, as an enum. 
    /// </summary>
    public ExpLevel experienceLevel { get; set; }
    /// <summary>
    /// Cat pronouns, to be used for text replacment purposes. 
    /// </summary>
    public List<Pronoun> pronouns { get; set; }
    //public SKBitmap sprite { get; set; }


    public string fullName
    {
        get { return name.ToString(); }
    }


    public Cat(CatStatus status = CatStatus.Newborn, int moons = 0, CatSex sex = CatSex.Female, List<string>? bioParents = null,
        List<string>? adoptiveParents = null, string? prefix = null, string? gender = null, string? suffix = null, int experience = 0)
    {
        gender ??= sex.ToString();
        adoptiveParents ??= new();
        bioParents ??= new();
        
        this.Id = GetValidId();
        this.sex = sex;
        this.gender = gender;
        this.status = status;
        this.moons = moons;
        this.bioParents = bioParents;
        this.adoptiveParents = adoptiveParents;
        this.experience = experience;

        name = new Name(prefix, suffix, nameStatus: this.status);
    }

    //NEW CAT FUNCTIONS

    public static Cat GenerateRandomCat()
    {
        return new Cat();
    }


    // EQ-OVERRIDES

    public override bool Equals(object? obj) => this.Equals(obj as Cat);

    public bool Equals(Cat? p)
    {
        if (p is null)
        {
            return false;
        }

        if (Object.ReferenceEquals(this, p))
        {
            return true;
        }

        if (this.GetType() != p.GetType())
        {
            return false;
        }

        // Cat is the same if the IDs are equal. 
        return this.Id == p.Id;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Cat? lhs, Cat? rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Cat? lhs, Cat? rhs) => !(lhs == rhs);

    // END EQ OVERRIDES

    private void ChangeStatus(CatStatus newStatus)
    {
        _status = newStatus;
    }

    public void OneMoon()
    {
        moons += 1;
    }

    public bool IsPotentialMate(Cat otherCat)
    {
        if (this == otherCat)
        {
            return false;
        }

        return true;
    }

    public bool IsRelated(Cat otherCat)
    {
        return false;
    }

    /// <summary>
    /// Kills the kitty
    /// </summary>
    /// <returns> Some extra event text.  </returns>
    public string Die()
    {
        string output = "";
        lives -= 1;
        if (lives < 1)
        {
            dead = true;
        }
        else
        {
            output = $"{fullName} lost a life.";
        }

        return output;
    }

    /// <summary>
    /// Makes the cat a mate
    /// </summary>
    /// <param name="otherCat"></param>
    public void SetMate(Cat otherCat)
    {

    }

    public void UnSetMate(Cat otherCat)
    {

    }

}
