using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text field;
    bool isTracking;
    public GameObject ball;

    void Start()
    {
        Events.OnTarget += OnTarget;
    }
    void OnDestroy()
    {
        Events.OnTarget -= OnTarget;
    }
    void OnTarget(GameObject go, bool isOn)
    {
        isTracking = isOn;
    }
    void Update()
    {
        if (GameManager.Instance.state == GameManager.states.TARGET_ON)
            field.text = ball.transform.localPosition.ToString();
        else
            field.text = "NO";
    }
}
