using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public string userID;
    [SerializeField] private Text userIDField;
    [SerializeField] private Text scoreField;

    public void Init(NetworkIdentity ni)
    {
        this.userID = ni.GetID();
        userIDField.text = userID;
        scoreField.text = "0";
    }
}
