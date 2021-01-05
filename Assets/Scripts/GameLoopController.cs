using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameLoopController : MonoBehaviour
{
    public List<Character> players = new List<Character>();    
    public static GameLoopController Current;
    private IEnumerator gameLoop;

    private void Start()
    {
        new Statistics();
        Statistics.instance.gameOver.AddListener((int i) => StopCoroutine(gameLoop));
        //For only temporarily offline testing. Distributing roles goes on before this scene
        distributeRoles();

        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);

        gameLoop = GameLoop();
        StartCoroutine(gameLoop);
    }

    
    IEnumerator GameLoop()
    {
        yield return new WaitUntilCondition() { condition = () => true };
        yield return new Night().Enumerator;
        yield return new RecognisePlayers().Enumerator;
        while (true)
        {
            yield return new Day().Enumerator;
            yield return new Night().Enumerator;

            //TODO: MafiaNight and ComissionerNight should be below
            if (Night.NightID % 2 == 0)
                yield return new ComissionerNight().Enumerator;
            else
                yield return new MafiaNight().Enumerator;
        }

    }

    private void distributeRoles()
    {
        //It will take the value from previous scenes, for the sake of simplicity, its 6
        //Also Role Distributer works in previous scenes, we took it here
        for (int i = 0; i < 7; i++)
        {
            if (i < Statistics.MAFIANUMBER)
                players.Add(new Mafia(i, Character.Characters.Mafia));
            else if (i < (Statistics.MAFIANUMBER + Statistics.CITIZENNUMBER))
                players.Add(new Citizen(i, Character.Characters.Citizen));
            else
                players.Add(new Citizen(i, Character.Characters.Comissioner));
        }
        players.Shuffle();
    }

}


public abstract class LoopElement
{
    public IEnumerator Enumerator;

    public LoopElement()
    {
        Enumerator = enumerator();
    }
    public virtual IEnumerator enumerator() => null;
    
}
