using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    public string url1 = "ws://";
    public string url2 = ":52300/socket.io/?EIO=4&transport=websocket";
    //192.168.0.19 : casa
    public InputField field;

    private void Start()
    {
        field.text = PlayerPrefs.GetString("ip", "192.168.0.19");
        SetField();
    }
    void SetField()
    {
        Data.Instance.url = url1 + field.text + url2;
    }
    public void Init()
    {
        PlayerPrefs.SetString("ip", field.text);
        SetField();
#if UNITY_ANDROID
             UnityEngine.SceneManagement.SceneManager.LoadScene("Pad_Tablet");
#else
            UnityEngine.SceneManagement.SceneManager.LoadScene("Server");   
#endif
    }
}
