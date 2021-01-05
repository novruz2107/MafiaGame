using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public static class ListHelper
{
    //Choosing random num with exception
    public static int RandomExcept(List<int> excepts, int max)
    {
        int result = UnityEngine.Random.Range(0, max);
        while (excepts.Contains(result))
        {
            result = UnityEngine.Random.Range(0, max);
        }
        return result;
    }

    public static int RandomExcept(int except, int max)
    {
        int result = UnityEngine.Random.Range(0, max);
        while (result == except)
        {
            result = UnityEngine.Random.Range(0, max);
        }
        return result;
    }

    //Return highest, but if highest is more than one, returns -1
    public static int GetHighestVote(List<int> VoteList)
    {
        List<int> idsWithHigherVotes = VoteList.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).ToList<int>();
        List<int> higherVotes = VoteList.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Count()).ToList();

        //for (int i = 0; i < higherVotes.Count; i++)
        //    Debug.Log(higherVotes[i]);

        if (higherVotes.Count > 1 && (higherVotes[0] == higherVotes[1])) //If two players got the same votes
            return -1;
        else
            return idsWithHigherVotes[0];
    }

    public static List<int> GetExceptionVotesForLastPlayer(List<int> VoteList)
    {
        List<int> idsWithHigherVotes = VoteList.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).ToList<int>();
        List<int> higherVotes = VoteList.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Count()).ToList();

        int maxNumVote = higherVotes[0] - 1;
        if(higherVotes.Count > 1 && (higherVotes[0] > 1) && (higherVotes[0] == higherVotes[1]))
        {
            //When two player got max vote, last player can vote one of them only
            List<int> result = new List<int>(Enumerable.Range(0, VoteList.Count));

            int k = 0;
            while (higherVotes[0] == higherVotes[k]) {
                result.Remove(idsWithHigherVotes[k]);
                k++;
            }

            return result;
        }
        else if(higherVotes[0] == 1)
        {
            List<int> result = new List<int>();
            for (int i = 0; i <= VoteList.Count; i++)
                if(!idsWithHigherVotes.Contains(i))
                    result.Add(i);
            return result;
        }
        else
        {
            return VoteList.GroupBy(i => i).Select((grp) =>
            {
                if (grp.Count() == maxNumVote)
                    return grp.Key;
                else
                    return -1;
            }).ToList();
        }
    }


    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
