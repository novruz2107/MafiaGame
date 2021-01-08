using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Statistics
{
    public static Statistics instance;
    public GameOver gameOver;
    public static bool isGameOver = false;

    //Temporarily hardcoded. It is coming from previous scenes
    public const int MAFIANUMBER = 3;
    public const int CITIZENNUMBER = 5;
    public const int COMISSIONERNUMBER = 1;
    public const int DOCTORNUMBER = 1;
    public const int SUKANUMBER = 0;
    public const int MANIACNUMBER = 1;

    public Statistics()
    {
        instance = this;
        gameOver = new GameOver();
    }


    //This method is used to update the number of players and thus, win/lose cases
    public void UpdateStatistics(Character kickedPlayer)
    {
        Character.Characters kickedChar = kickedPlayer.getCharacter();
        Debug.Log("Now there is/are " + getNumberOfChar(kickedChar) + " " + kickedChar.ToString());
        //Win/Lose cases go below. PS: The snippet should be refactored!
        if (kickedChar == Character.Characters.Mafia && getNumberOfChar(kickedChar) == 0)
        {
            if (getNumberOfChar(Character.Characters.Maniac) == 0)
            {
                Debug.Log("Citizen wins the game!");
                gameOver.Invoke(0); //0 means citizen wins
            }
            else
            {
                Debug.Log("Maniac wins the game!");
                gameOver.Invoke(2); //2 means maniac wins
            }
            isGameOver = true;
        }
        else if ((kickedChar == Character.Characters.Citizen || kickedChar == Character.Characters.Comissioner) &&
                    GameLoopController.Current.players.OfType<Citizen>().ToList().Count <= 1)
        {
            if (getNumberOfChar(Character.Characters.Maniac) == 0)
            {
                Debug.Log("Mafia wins the game!");
                gameOver.Invoke(1); //1 means mafia wins
            }
            else
            {
                Debug.Log("Maniac wins the game!");
                gameOver.Invoke(2); //2 means maniac wins
            }
            isGameOver = true;
        }

    }    

    public int getNumberOfChar(Character.Characters character)
    {
        return GameLoopController.Current.players.FindAll(i => i.getCharacter() == character).Count;
    }

}

[System.Serializable]
public class GameOver : UnityEvent<int>
{
}
