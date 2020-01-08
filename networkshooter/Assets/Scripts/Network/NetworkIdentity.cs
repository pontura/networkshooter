using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkIdentity : MonoBehaviour
{
    [SerializeField]
    private string id;
    [SerializeField]
    private bool isControlling;

    private SocketIOComponent socket;


    private void Awake()
    {
        isControlling = false;
    }
    public void SetControllerID(string ID)
    {
        this.id = ID;
        isControlling = (NetworkClient.ClientID == ID) ? true : false;
    }
    public void SetSocketReference(SocketIOComponent socket)
    {
        this.socket = socket;
    }
    public string GetID()
    {
        return id;
    }
    public bool IsControlling()
    {
        return isControlling;
    }
    public SocketIOComponent GetSocket()
    {
        return socket;
    }
}
