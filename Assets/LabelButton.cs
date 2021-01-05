using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelButton : MonoBehaviour
{
    Camera cam;
    Transform panel;
    public float FixedSize = .005f;
    public Vector3 camPosition = Vector3.zero;
    public Vector3 camRotation = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        panel = transform.GetChild(1);
        GetComponent<Button>().onClick.AddListener(openClosePanel);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.forward = cam.transform.forward;
        //transform.LookAt(cam.transform, Vector3.down);
        //transform.localEulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        transform.forward = new Vector3(cam.transform.forward.x, transform.forward.y, cam.transform.forward.z);
        var distance = (cam.transform.position - transform.position).magnitude;
        var size = distance * FixedSize * cam.fieldOfView;
        transform.localScale = Vector3.one * size;
        //transform.forward = transform.position - cam.transform.position;

        if (panel.gameObject.activeInHierarchy)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPosition, 10 * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(Quaternion.Euler(cam.transform.eulerAngles), Quaternion.Euler(camRotation), 10 * Time.deltaTime);
            
            if(Input.GetMouseButtonDown(0))
                panel.gameObject.SetActive(false);
        }
    }

    public void openClosePanel()
    {
        
        if (!panel.gameObject.activeInHierarchy)
        {
            panel.gameObject.SetActive(true);
        }
    }
}
