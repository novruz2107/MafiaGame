using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : LoopElement
{
    public override IEnumerator enumerator()
    {
        Debug.Log("Day begins...");
        yield return new WaitForSeconds(3);
        //TODO: If Mafia or Comissioner chose someone whom was not protected by Doctor, then he kicked out
        //(Exception: If in first attempt Comissioner chooses Citizen, Citizen is inherited)
        if (Night.NightID % 2 == 0) //After comissioner night
        {
            if (Night.NightID > 2) //Its not the first attempt
            {
                Debug.Log("Comissioner kicked " + ComissionerNight.getChosenPlayer().Id + " who was " + ComissionerNight.getChosenPlayer().getCharacter().ToString());
                yield return new Kick(ComissionerNight.getChosenPlayer()).Enumerator;
            }
            else//its first attempt
            {
                if (ComissionerNight.getChosenPlayer().getCharacter() == Character.Characters.Citizen)
                {
                    Debug.Log("Comissioner chose " + ComissionerNight.getChosenPlayer().Id + " who was " + ComissionerNight.getChosenPlayer().getCharacter().ToString());
                    Debug.Log("Therefore, player " + ComissionerNight.getChosenPlayer().Id + " is inherited");
                    (ComissionerNight.getChosenPlayer() as Citizen).InheritAsComissioner();
                }
                else
                {
                    Debug.Log("Comissioner kicked " + ComissionerNight.getChosenPlayer().Id + " who was " + ComissionerNight.getChosenPlayer().getCharacter().ToString());
                    yield return new Kick(ComissionerNight.getChosenPlayer()).Enumerator;
                }
            }
        }
        else if(Night.NightID > 1) //After mafia night
        {
            Debug.Log("Don mafia kicked " + MafiaNight.getChosenPlayer().Id + " who was " + MafiaNight.getChosenPlayer().getCharacter().ToString());
            yield return new Kick(MafiaNight.getChosenPlayer()).Enumerator;
        }

        //Check if Game over, if yes, stop the coroutine
        if (Statistics.isGameOver)
            yield break;

        Debug.Log("Voting begins..");
        yield return new WaitForSeconds(5);

        //TODO: Voting begins and while voting goes on, enum waits. 
        //After voting finished, he/she whom gets the max votes will be kicked out of the game.
        new VotingSystem().vote(GameLoopController.Current.players.Count, true);
        yield return new WaitUntilCondition()
        {
            condition = () => !VotingSystem.isGoing
        };

        //TODO: If players have the same max number of votes, then again voting goes on
        if(VotingSystem.Instance.chosen == -1)
        {
            Debug.Log("Since players got the same votes, voting goes on again..");
            yield return new WaitForSeconds(2);
            new VotingSystem().vote(GameLoopController.Current.players.Count, false);
            yield return new WaitUntilCondition()
            {
                condition = () => !VotingSystem.isGoing
            };
        }

       
        yield return new Kick(VotingSystem.Instance.chosen).Enumerator;
    }

}
