using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MeshRenderer targetBase;
    public Transform ball;
    static GameManager mInstance = null;

    public states state;
    public enum  states
    {
        TARGET_OFF,
        TARGET_ON
    }

    public static GameManager Instance
    {
        get
        {
            return mInstance;
        }
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
    }
    void Update()
    {
        if (state == states.TARGET_OFF && targetBase.enabled)
            state = states.TARGET_ON;
        else if (state == states.TARGET_ON && !targetBase.enabled)
            state = states.TARGET_OFF;
    }
}
