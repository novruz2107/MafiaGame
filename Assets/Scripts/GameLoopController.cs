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
        Statistics.instance.gameOver.AddListener((int i) => StopCoroutine(gameLoop)); //Event for gameOver and stopping coroutine
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
        yield return new WaitUntilCondition() { condition = () => true }; //Refactor this one to start the loop (Photon stuff)
        yield return new Night().Enumerator;
        yield return new RecognisePlayers().Enumerator;
        while (true)
        {
            yield return new Day().Enumerator;
            yield return new Night().Enumerator;

            
            if (Night.NightID % 2 == 0) // its even night
            {
                yield return new ComissionerNight().Enumerator;
                if(Statistics.instance.getNumberOfChar(Character.Characters.Suka) > 0)
                    yield return new SukaNight().Enumerator;
            }
            else //its odd night
            {
                yield return new MafiaNight().Enumerator;
                if (Statistics.instance.getNumberOfChar(Character.Characters.Maniac) > 0)
                    yield return new ManiacNight().Enumerator;
            }

            if (Statistics.instance.getNumberOfChar(Character.Characters.Doctor) > 0)
                yield return new DoctorNight().Enumerator;
        }

    }

    private void distributeRoles()
    {
        //It will take the value from previous scenes
        //Also Role Distributer works in previous scenes, we took it here temporarily!
        for (int i = 0; i < 11; i++)
        {
            if (i < Statistics.MAFIANUMBER)
                players.Add(new Mafia(i, Character.Characters.Mafia));
            else if (i < (Statistics.MAFIANUMBER + Statistics.CITIZENNUMBER))
                players.Add(new Citizen(i, Character.Characters.Citizen));
            else if (i < Statistics.MAFIANUMBER + Statistics.CITIZENNUMBER + Statistics.COMISSIONERNUMBER)
                players.Add(new Citizen(i, Character.Characters.Comissioner));
            else if (i < Statistics.MAFIANUMBER + Statistics.CITIZENNUMBER + Statistics.COMISSIONERNUMBER + Statistics.DOCTORNUMBER)
                players.Add(new Character(i, Character.Characters.Doctor));
            else
                players.Add(new Character(i, Character.Characters.Maniac));
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
