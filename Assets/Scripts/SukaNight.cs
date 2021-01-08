using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SukaNight : LoopElement
{
    private static List<Character> chosenPlayers = new List<Character>();
    public override IEnumerator enumerator()
    {
        yield return null;
        //Choose someone to look at his card, and add this to known player list (chosenPlayers)
        Debug.Log("Suka is thinking..");
        yield return new WaitForSeconds(5);
        int chosen = Random.Range(0, GameLoopController.Current.players.Count);
        //choosing someone untill its not suka and its not already chosen
        while (GameLoopController.Current.players[chosen].getCharacter() == Character.Characters.Suka &&
                chosenPlayers.Contains(GameLoopController.Current.players[chosen]))
            chosen = Random.Range(0, GameLoopController.Current.players.Count);
        setChosenPlayer(GameLoopController.Current.players[chosen]);
        Debug.Log("Suka chose someone (id:" + GameLoopController.Current.players[chosen].Id + "). Suka Night is over.");
        yield return new WaitForSeconds(3);
    }

    public static List<Character> getChosenPlayer()
    {
        return chosenPlayers;
    }

    private void setChosenPlayer(Character c)
    {
        chosenPlayers.Add(c);
    }
}
