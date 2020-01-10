using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class UIManager : MonoBehaviour
{
    public Text field;
    bool isTracking;
    public GameObject ball;

    void Start()
    {
        Events.OnDebbugText += OnDebbugText;
    }
    void OnDestroy()
    {
        Events.OnDebbugText -= OnDebbugText;
    }
    void OnDebbugText(string text)
    {
        field.text = text;
    }
}
