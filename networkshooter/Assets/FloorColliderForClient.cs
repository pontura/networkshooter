using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FloorColliderForClient : MonoBehaviour
{
    public GameObject targets;
    ImageTargetBehaviour[] allTargets;
    public MeshRenderer[] meshes;
    void Start()
    {
        allTargets = targets.GetComponentsInChildren<ImageTargetBehaviour>();
        print("meshes " + meshes.Length);
        Loop();
    }   

    void Loop()
    {
        Vector3 pos = Vector3.zero;
        Vector3 sc = Vector3.one;
        Quaternion average = new Quaternion(0, 0, 0, 0);
        int total = 0;
        int id = 0;
        ImageTargetBehaviour lastChecked = null;
        bool CenterChecked = false;
        foreach (ImageTargetBehaviour it in allTargets)
        {
            id++;
            if (meshes[id].enabled)
            {
                if(id == 3 & !CenterChecked)
                {
                    pos = it.transform.localPosition;
                    sc = it.transform.localScale;

                    transform.position = pos;
                    transform.localScale = sc;
                    CenterChecked = true;
                }            
                total++;
                if (lastChecked != null)
                    average = Quaternion.Slerp(it.transform.rotation, lastChecked.transform.rotation, 1 / (float)total);
                else
                    average = it.transform.rotation;
                lastChecked = it;
            }           
        }           
        if (total > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, average, 0.05f);
        }
        
        Invoke("Loop", 0.05f);
    }
}
