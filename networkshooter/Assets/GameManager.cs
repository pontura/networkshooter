using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MeshRenderer targetBase;
    public Transform ball;
    static GameManager mInstance = null;

    public types type;
    public enum types
    {
        SERVER,
        CLIENT
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

}
