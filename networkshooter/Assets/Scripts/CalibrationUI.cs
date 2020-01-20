using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class CalibrationUI : MonoBehaviour
{
    public Text debbugText;
    public float scaleOffset = 20f;
    float offsetQty = 1f;
    float newScaleOffset;

    private void Start()
    {
        NetworkClient.offsetScale = PlayerPrefs.GetFloat("offsetScale", 0.4f);
        scaleOffset = NetworkClient.offsetScale;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            scaleOffset += offsetQty;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            scaleOffset -= offsetQty;
        
        if(newScaleOffset != scaleOffset)
        {
            newScaleOffset = scaleOffset;
            SetField();
        }

    }
    void SetField()
    {
        debbugText.text = "scaleOffset: " + scaleOffset;
        Invoke("Reset", 1);
        NetworkClient.offsetScale = scaleOffset;
        PlayerPrefs.SetFloat("offsetScale", scaleOffset);
    }
    void Reset()
    {
        debbugText.text = "";
    }
}
