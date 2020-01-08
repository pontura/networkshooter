using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkTransform : MonoBehaviour
{

    private Vector3 oldPosition;
    private NetworkIdentity networkIdentity;
    private float stillCounter = 0;
    private Player player;

    void Start()
    {
        networkIdentity = GetComponent<NetworkIdentity>();
        oldPosition = transform.position;
        player = new Player();
        player.position = new Position();
        player.position.x = player.position.y = 0;
        if (!networkIdentity.IsControlling())
            enabled = false;
    }
    void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.state == GameManager.states.TARGET_OFF)
            return;
      if (networkIdentity.IsControlling())
        { 
            if (oldPosition != transform.position)
            {
                oldPosition = transform.position;
                stillCounter = 0;
                SendData();
            }
            else
            {
                stillCounter += Time.deltaTime;
                if(stillCounter>= 1)
                {
                    stillCounter = 0;
                   // SendData();
                }
            }
      }
    }
    void SendData()
    {
        Vector3 pos = GameManager.Instance.ball.transform.position;
        transform.position = new Vector3(pos.x, 0, pos.z);
        player.position.x = Mathf.Round(transform.position.x * 1000); //* 1000.0f) / 1000.0f;
        player.position.y = Mathf.Round(transform.position.y * 1000); // * 1000.0f) / 1000.0f;
        networkIdentity.GetSocket().Emit("updatePosition", new JSONObject(JsonUtility.ToJson(player)));
       // Events.OnDebbugText("x" + player.position.x);
    }
}
