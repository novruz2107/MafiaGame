using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Unity;

public class RecognisePlayers : LoopElement
{
    public override IEnumerator enumerator()
    {
        yield return null;
        //TODO: Mafias open their eyes, they vote for Don. Untill they choose,
        //enum waits. Then GameLoop begins

        //Below mafias open eyes
        List<Mafia> mafias = GameLoopController.Current.players.OfType<Mafia>().ToList();
        

        new VotingSystem().voteInclusive(mafias.Count);
        yield return new WaitUntilCondition()
        {
            condition = () => !VotingSystem.isGoing
        };

        int chosen = VotingSystem.Instance.chosen;

        //TODO: If each mafia has only one vote, then Don is chosen randomly
        if (chosen == -1)
            chosen = ListHelper.RandomExcept(-1, mafias.Count);

        mafias[chosen].MakeDon();
        //Below snippet shows to get the Don Mafia
        yield return new WaitForSeconds(2);

    }
}

