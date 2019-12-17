using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AAAAAAA : MonoBehaviour
{
    public InputField field;
    public string value;
    public string value_default;

    private void Start()
    {
        field.text = value;
    }
    public void AAAAA()
    {      
        Data.Instance.url = field.text;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
