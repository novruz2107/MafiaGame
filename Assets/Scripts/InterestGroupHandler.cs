using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestGroupHandler : MonoBehaviour
{
    public static InterestGroupHandler Current;
    public enum EventId : byte { GoToCommonGroup, GoToIndividualGroup }

    public System.Action OnGoneToCommonGroup, OnGoneToIndividualGroup, OnSwitchedGroup;

    public static void GoToIndividualGroup() { }
    public static void GoToCommonGroup() { }

    private void Start()
    {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
    }

    public void OnEvent(int i)
    {
        switch (i)
        {
            case 0:
                //SetToGroup(Character.CharacterIndex.Value);
                OnGoneToIndividualGroup();
                OnSwitchedGroup();
                break;
            case 1:
                //SetToGroup(0);
                OnGoneToCommonGroup();
                OnSwitchedGroup();
                break;
            default:
                break;
        }
    }

}
