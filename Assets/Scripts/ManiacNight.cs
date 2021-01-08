using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiacNight : LoopElement
{
    private static Character chosenPlayer;
    public override IEnumerator enumerator()
    {
        yield return null;
        //TODO: Chooses someone to kick (except himself)
        //TODO: Maniac opens his eye
        Debug.Log("Maniac is thinking..");
        yield return new WaitForSeconds(5);
        int chosen = Random.Range(0, GameLoopController.Current.players.Count);
        //choose someone untill its not maniac
        while (GameLoopController.Current.players[chosen].getCharacter() == Character.Characters.Maniac)
            chosen = Random.Range(0, GameLoopController.Current.players.Count);
        setChosenPlayer(GameLoopController.Current.players[chosen]);
        Debug.Log("Maniac chose someone (id:" + GameLoopController.Current.players[chosen].Id + "). Maniac Night is over.");
        yield return new WaitForSeconds(3);
    }

    public static Character getChosenPlayer()
    {
        return chosenPlayer;
    }

    private void setChosenPlayer(Character c)
    {
        chosenPlayer = c;
    }
}
