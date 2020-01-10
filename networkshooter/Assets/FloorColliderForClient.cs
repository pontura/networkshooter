using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FloorColliderForClient : MonoBehaviour
{
    public GameObject targets;
    ImageTargetBehaviour[] allTargets;

    void Start()
    {
        allTargets = targets.GetComponentsInChildren<ImageTargetBehaviour>();
        Loop();
    }   

    void Loop()
    {
        print("length: " + allTargets.Length);
        Vector3 pos = Vector3.zero;
        Vector3 rot = Vector3.zero;
        Vector3 sc = Vector3.one;
        int total = 0;
        foreach(ImageTargetBehaviour it in allTargets)
        {
            if(it.transform.localEulerAngles != Vector3.zero)
            {
                pos += it.transform.localPosition;
                rot += it.transform.rotation.eulerAngles;
                sc += it.transform.localScale;
                total++;
            }
        }
        if(total > 1)
        {
            pos /= total;
            rot /= total;
            sc /= total;
        }
        transform.position = pos;
        transform.eulerAngles = rot;
        transform.localScale = sc;
        Invoke("Loop", 0.05f);
    }
}
