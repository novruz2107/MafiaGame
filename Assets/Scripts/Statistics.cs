using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Statistics
{
    private Dictionary<Character.Characters, int> charNumbers = new Dictionary<Character.Characters, int>();
    public static Statistics instance;
    public GameOver gameOver;
    public static bool isGameOver = false;

    //Temporarily hardcoded
    public const int MAFIANUMBER = 2;
    public const int CITIZENNUMBER = 4;
    public const int COMISSIONERNUMBER = 1;
    public const int DOCTORNUMBER = 0;
    public const int LAWYERNUMBER = 0;
    public const int MANIACNUMBER = 0;

    public Statistics()
    {
        instance = this;
        gameOver = new GameOver();
        charNumbers = new Dictionary<Character.Characters, int>();
        charNumbers.Add(Character.Characters.Citizen, CITIZENNUMBER);
        charNumbers.Add(Character.Characters.Comissioner, COMISSIONERNUMBER);
        charNumbers.Add(Character.Characters.Doctor, DOCTORNUMBER);
        charNumbers.Add(Character.Characters.Suka, LAWYERNUMBER);
        charNumbers.Add(Character.Characters.Mafia, MAFIANUMBER);
        charNumbers.Add(Character.Characters.Maniac, MANIACNUMBER);
    }

    public void UpdateStatistics(Character kickedPlayer)
    {
        Character.Characters kickedChar = kickedPlayer.getCharacter();
        charNumbers[kickedChar]--;
        if (kickedChar == Character.Characters.Mafia && charNumbers[kickedChar] == 0)
        {
            Debug.Log("Citizen wins the game!");
            gameOver.Invoke(0); //0 means citizen wins
            isGameOver = true;
        }
        else if ((kickedChar == Character.Characters.Citizen || kickedChar == Character.Characters.Comissioner) && charNumbers[kickedChar] == 1)
        {
            Debug.Log("Mafia wins the game!");
            gameOver.Invoke(1); //1 means mafia wins
            isGameOver = true;
        }

    }    

}

[System.Serializable]
public class GameOver : UnityEvent<int>
{
}
