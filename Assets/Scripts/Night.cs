using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night : LoopElement
{
    public static int NightID = 0;
    public override IEnumerator enumerator()
    {
        NightID++;
        Debug.Log("Night " + NightID.ToString() +" , everyone closes their eyes");
        //TODO: All players close their eyes

        yield return new WaitForSeconds(5);
       
    }
}
