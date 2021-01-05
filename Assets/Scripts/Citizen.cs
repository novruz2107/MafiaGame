using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : Character
{
    private bool isInheritedComissioner = false;

    public Citizen(int Id, Characters CharacterIndex) : base(Id, CharacterIndex) { }

    public Citizen() : base() { }

    public void InheritAsComissioner()
    {
        Debug.Log("Player " + Id.ToString() + " is inherited as Comissioner for the future.");
        isInheritedComissioner = true;
    }

    public bool IsInheritedComissioner()
    {
        return isInheritedComissioner;
    }
}
