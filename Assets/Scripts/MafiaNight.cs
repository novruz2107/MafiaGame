using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MafiaNight : LoopElement
{
    private static Character chosenPlayer;
    public override IEnumerator enumerator()
    {
        yield return null;
        //TODO: Mafias open their eyes
        //GameLoopController.Current.players.OfType<Mafia>().ToList() ------- this return all mafias

        //TODO: Discussion goes on for a minute, and Don Mafia chooses one player (except himself and mafia guys)
        //GameLoopController.Current.players.OfType<Mafia>().Where(i => ((i).IsDon())).First().IsDon() -------  this returns Don mafia
        Debug.Log("Mafias are discussing..");
        yield return new WaitForSeconds(5);
        int chosen = Random.Range(0, GameLoopController.Current.players.Count);
        while(GameLoopController.Current.players[chosen].getCharacter() == Character.Characters.Mafia) //choosing someone untill its not mafia
            chosen = Random.Range(0, GameLoopController.Current.players.Count);
        setChosenPlayer(GameLoopController.Current.players[chosen]);
        Debug.Log("Don mafia chose someone (id:" + GameLoopController.Current.players[chosen].Id + "). Mafia Night is over.");
        yield return new WaitForSeconds(3);
        //TODO: Mafias close their eyes

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
