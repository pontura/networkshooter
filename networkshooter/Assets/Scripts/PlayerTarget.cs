using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    NetworkIdentity networkIdentity;
    public SpriteRenderer sr;

    void Start()
    {
        networkIdentity = GetComponent<NetworkIdentity>();
        int colorID = networkIdentity.GetNum()-1;
        sr.color = Data.Instance.settings.GetColor(colorID);
    }
}
