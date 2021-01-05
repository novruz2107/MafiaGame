using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComissionerNight : LoopElement
{
    private static Character chosenPlayer;
    public override IEnumerator enumerator()
    {
        yield return null;
        //TODO: Comissioner opens his eyes and chooses a player (except himself)
        Debug.Log("Comissioner is choosing to kick someone..");
        yield return new WaitForSeconds(5);
        int chosen = Random.Range(0, GameLoopController.Current.players.Count);
        while (GameLoopController.Current.players[chosen].getCharacter() == Character.Characters.Comissioner)
            chosen = Random.Range(0, GameLoopController.Current.players.Count);
        setChosenPlayer(GameLoopController.Current.players[chosen]);
        Debug.Log("Comissioner chose someone. Comissioner Night is over.");
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
