using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class TargetToServer : MonoBehaviour
{
    public SocketIO.NetworkClient networkClient;
    Vector3 ballPosition;
    Vector3 lastBallPosition;
    Vector3 smoothPos;
    float stillCounter;
    NetworkIdentity networkIdentity;
    Player player;

    void Awake()
    {
        Events.OnAddPlayer += OnAddPlayer;
    }
    private void OnDestroy()
    {
        Events.OnAddPlayer -= OnAddPlayer;
    }
    void OnAddPlayer(NetworkIdentity ni)
    {
        print("ADD PLAYER " + NetworkClient.ClientID);

        if (ni.GetID() == NetworkClient.ClientID)
        {
            networkIdentity = ni;
            player = new Player();
            player.position = new Position();
            player.position.x = player.position.y = 0;
        }
    }
    public void SetCollisionPoint(Vector3 raycastHitPosition)
    {
        transform.position = raycastHitPosition;
    }
    void Update()
    {
        if (player == null)
            return;
        if (GameManager.Instance.type == GameManager.types.SERVER)
            return;

        ballPosition = transform.localPosition;
        
        if (ballPosition != lastBallPosition)
        {
            lastBallPosition = ballPosition;
            stillCounter = 0;
            SendData();
        }
        else
        {
            stillCounter += Time.deltaTime;
            if (stillCounter >= 1)
            {
                stillCounter = 0;
                SendData();
            }
        }
    }

    void SendData()
    {
        smoothPos = Vector3.Lerp(ballPosition, smoothPos, 0.5f);
        player.position.x = Mathf.Round(smoothPos.x * 1000); //* 1000.0f) / 1000.0f;
        player.position.y = Mathf.Round(smoothPos.z * 1000); // * 1000.0f) / 1000.0f;
        networkIdentity.GetSocket().Emit("updatePosition", new JSONObject(JsonUtility.ToJson(player)));
      //  print("x: " + smoothPos.x + "y: " + smoothPos.z);
    }
}
