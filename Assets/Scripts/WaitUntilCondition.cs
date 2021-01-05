using System;
using UnityEngine;

public class WaitUntilCondition : CustomYieldInstruction
{
    public Action action { get; set; } = () => { };
    public Func<bool> condition { get; set; } = () => true;

    public override bool keepWaiting
    {
        get
        {
            action.Invoke();
            return !condition();
        }
    }
}

