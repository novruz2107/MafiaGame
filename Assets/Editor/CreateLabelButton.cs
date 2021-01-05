using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateLabelButton : Editor
{
    
    [MenuItem("GameObject/Create Label with Panel", false, 0)]
    static void createLabel()
    {
        LabelButton labelButton = GameObject.FindObjectOfType<LabelButton>();
        Transform canvas = null;
        if (!labelButton)
        {
            GameObject instance = Instantiate(Resources.Load("LabelButtonCanvas", typeof(GameObject))) as GameObject;
            labelButton = instance.GetComponentInChildren<LabelButton>();
            labelButton.camPosition = Camera.main.transform.position;
            labelButton.camRotation = Camera.main.transform.eulerAngles;
        }
        else
        {
            canvas = FindObjectOfType<LabelButton>().transform.parent;
            GameObject instance = Instantiate(Resources.Load("LabelButton", typeof(GameObject)), canvas) as GameObject;
            instance.GetComponent<LabelButton>().camPosition = Camera.main.transform.position;
            instance.GetComponent<LabelButton>().camRotation = Camera.main.transform.eulerAngles;
        }

    }
}
