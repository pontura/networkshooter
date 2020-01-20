using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public string userID;
    [SerializeField] private Text userIDField;
    [SerializeField] private Text scoreField;
    [SerializeField] private Image bg;

    public void Init(NetworkIdentity ni)
    {
        this.userID = ni.GetID();
        userIDField.text = ni.GetNum() + ": " + userID;
        scoreField.text = "0";
        bg.color = Data.Instance.settings.GetColor(ni.GetNum()-1);
    }
}
