using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Kick
{
    public IEnumerator Enumerator;
    private static Character lastKickedChar;

    public Kick(int actor_index)
    {
        Enumerator = enumerator(actor_index);
    }

    public Kick(Character player)
    {
        Enumerator = enumerator(player);
    }

    private IEnumerator enumerator(int actor_index)
    {
        Debug.Log("Kicking the player number:" + actor_index + ", id: " + GameLoopController.Current.players[actor_index].Id + " who was " + GameLoopController.Current.players[actor_index].getCharacter().ToString());
        lastKickedChar = GameLoopController.Current.players[actor_index];
        Statistics.instance.UpdateStatistics(GameLoopController.Current.players[actor_index]);
        GameLoopController.Current.players.RemoveAt(actor_index);
        checkAfterKick(lastKickedChar);
        yield return new WaitForSeconds(5);
    }

    private IEnumerator enumerator(Character player)
    {
        Debug.Log("Kicking the player who was " + player.getCharacter().ToString());
        lastKickedChar = player;
        Statistics.instance.UpdateStatistics(player);
        GameLoopController.Current.players.Remove(player);
        checkAfterKick(lastKickedChar);
        yield return new WaitForSeconds(5);
    }

    public static Character getLastKickedCharacter()
    {
        return lastKickedChar;
    }

    private void checkAfterKick(Character kickedPlayer)
    {
        Character.Characters kickedChar = kickedPlayer.getCharacter();
        //Checking if comissionar is kicked, then inherited citizen becomes comissioner
        if (kickedChar == Character.Characters.Comissioner)
            //if there is inherited citizen
            if (GameLoopController.Current.players.OfType<Citizen>().Where(i => ((i).IsInheritedComissioner())).Count() > 0)
            {
                GameLoopController.Current.players.OfType<Citizen>().Where(i => ((i).IsInheritedComissioner())).First().SetCharacter(Character.Characters.Comissioner);
                Debug.Log("Since comissioner is kicked, " + GameLoopController.Current.players.Where(i => (i.getCharacter() == Character.Characters.Comissioner)).First().Id + " is new Com (inherited)");
            }
            // if there is no inherited citizen, then it is choosen randomly
            else
            {
                GameLoopController.Current.players.OfType<Citizen>().OrderBy(x => Guid.NewGuid()).FirstOrDefault().SetCharacter(Character.Characters.Comissioner);
                Debug.Log("Since comissioner is kicked, " + GameLoopController.Current.players.Where(i => (i.getCharacter() == Character.Characters.Comissioner)).First().Id + " is new Com");
            }
        else if (kickedChar == Character.Characters.Mafia && (kickedPlayer as Mafia).IsDon())
        {
            GameLoopController.Current.players.OfType<Mafia>().OrderBy(x => Guid.NewGuid()).FirstOrDefault().MakeDon();
            Debug.Log("Since don mafia is kicked, " + GameLoopController.Current.players.OfType<Mafia>().Where(i => ((i).IsDon())).First().Id + " is new Don");
        }
    }
}
