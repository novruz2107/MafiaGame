using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    static int id = 0;
    public Character()
    {
        this.Id = id++;
    }

    public Character(int Id)
    {
        this.Id = Id;
    }

    public Character(int Id, Characters CharacterIndex)
    {
        this.Id = Id;
        this.CharacterIndex = (byte)CharacterIndex;
    }

   
    public int Id;
    public bool isProtectedByDoc = false;
    public enum Characters : byte { Citizen, Mafia, Maniac, Doctor, Comissioner, Suka }
    private byte CharacterIndex;
    public void SetCharacter(Characters characterIndex)
    {
        this.CharacterIndex = (byte)characterIndex;
        //Type.GetType(characterIndex.ToString()).GetConstructor(new Type[0]).Invoke(new object[0]);
    }

    public Characters getCharacter()
    {
        return (Characters)CharacterIndex;
    }

    public void protectedByDoc(bool isProtected)
    {
        isProtectedByDoc = isProtected;
    }
}
