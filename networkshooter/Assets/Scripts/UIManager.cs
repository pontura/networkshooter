using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class UIManager : MonoBehaviour
{
    public Text field;
    bool isTracking;
   // public GameObject ball;
    public GameObject flash;

    void Start()
    {
        Events.OnDebbugText += OnDebbugText;
        Events.CatchBall += CatchBall;
        ResetFlash();
    }
    void OnDestroy()
    {
        Events.OnDebbugText -= OnDebbugText;
        Events.CatchBall -= CatchBall;
    }
    void CatchBall(Ball ball)
    {
        flash.SetActive(true);
        Invoke("ResetFlash", 0.2f);
    }
    void OnDebbugText(string text)
    {
        field.text = text;
    }
    void ResetFlash()
    {
        flash.SetActive(false);
    }
}
