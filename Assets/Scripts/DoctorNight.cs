using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorNight : LoopElement
{
    public override IEnumerator enumerator()
    {
        yield return null;
        //TODO: Doctor should protect someone (Character), but if that someone was protected last night,
        //he cannot be protected.
        Debug.Log("Doc is choosing someone to protect..");
        yield return new WaitForSeconds(5);
        int chosen = Random.Range(0, GameLoopController.Current.players.Count);
        while (GameLoopController.Current.players[chosen].getCharacter() == Character.Characters.Doctor ||
                GameLoopController.Current.players[chosen].isProtectedByDoc)
        {
            chosen = Random.Range(0, GameLoopController.Current.players.Count);
        }
        GameLoopController.Current.players.Find(i => ((i).isProtectedByDoc))?.protectedByDoc(false);
        GameLoopController.Current.players[chosen].isProtectedByDoc = true;
        Debug.Log("Doc chose player id: " + GameLoopController.Current.players[chosen].Id);
        yield return new WaitForSeconds(3);
    }
}
