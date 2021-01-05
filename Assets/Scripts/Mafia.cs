using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mafia : Character
{
    private bool isDon = false;

    public Mafia(int Id, Characters CharacterIndex) : base(Id, CharacterIndex) { }

    public Mafia() : base() { }

    public void MakeDon()
    {
        Debug.Log("Player " + Id.ToString() + " is chosen as Don Mafia.");
        isDon = true;
    }

    public bool IsDon()
    {
        return isDon;
    }
}
