using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VotingSystem
{
    public List<int> VoteList;
    public static bool isGoing;
    public static VotingSystem Instance;
    public int chosen => ListHelper.GetHighestVote(VoteList);


    public VotingSystem()
    {
        Instance = this;
        isGoing = true;
    }

    //Random vote function to automate voting (temporary)
    public void vote(int num, bool allowingSameNumVotes)
    {
        VoteList = new List<int>();
        for (int i = 0; i < num; i++)
        {
            int voted_whom;
            if (i == (num - 1) && !allowingSameNumVotes)
            {
                List<int> result = ListHelper.GetExceptionVotesForLastPlayer(VoteList);
                result.Add(i);
                voted_whom = ListHelper.RandomExcept(result, num);
            }
            else
            {
                voted_whom = ListHelper.RandomExcept(i, num);
            }
            RPCVote(i, voted_whom);
            Debug.Log(i + " voted " + voted_whom);
        }
        isGoing = false;
    }


    //Using for Don mafia only (temporary). Inclusive means player can vote himself.
    public void voteInclusive(int num)
    {
        VoteList = new List<int>();
        VoteList.AddRange(new int[num]);
        for (int i = 0; i < num; i++)
        {
            int voted_whom = UnityEngine.Random.Range(0, num);
            RPCVote(i, voted_whom);
            Debug.Log(i + " voted inclusively " + voted_whom);
        }
        isGoing = false;
    }
   

    public void RPCVote(int who_votes, int voted_whom) => VoteList.Add(voted_whom);

}
