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
    //void Update()
    //{

    //    if (GameManager.Instance.type == GameManager.types.SERVER)
    //        return;

    //    ballPosition = GameManager.Instance.ball.transform.position;

    //    if (networkIdentity.IsControlling())
    //    { 
    //        if (ballPosition != lastBallPosition)
    //        {
    //            lastBallPosition = ballPosition;
    //            oldPosition = transform.position;
    //            stillCounter = 0;
    //            SendData();
    //        }
    //        else
    //        {
    //            stillCounter += Time.deltaTime;
    //            if(stillCounter>= 1)
    //            {
    //                stillCounter = 0;
    //                SendData();
    //            }
    //        }
    //  }
    //}
    
    //void SendData()
    //{
    //    smoothPos = Vector3.Lerp(ballPosition, smoothPos, 0.5f);
    //    player.position.x = Mathf.Round(smoothPos.x * 1000); //* 1000.0f) / 1000.0f;
    //    player.position.y = Mathf.Round(smoothPos.z * 1000); // * 1000.0f) / 1000.0f;
    //    networkIdentity.GetSocket().Emit("updatePosition", new JSONObject(JsonUtility.ToJson(player)));
    //    //print("x: " + pos.x + "y: " + pos.z);
    //}
}
