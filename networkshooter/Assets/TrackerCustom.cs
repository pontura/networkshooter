using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackerCustom : DefaultTrackableEventHandler
{
    public ImageTargetBehaviour imageTargetBehavior;
    protected override void OnTrackingFound()
    {
        Debug.Log("__________________");
        Events.OnTarget(gameObject, true);
    }
    protected override void OnTrackingLost()
    {
        Debug.Log("LLLLLLLLLLLLLLLLLL");
        Events.OnTarget(gameObject, false);
    }
}
