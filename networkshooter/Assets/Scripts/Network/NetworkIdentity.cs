using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkIdentity : MonoBehaviour
{
    [SerializeField]
    private string id;
    [SerializeField]
    private int num;
    [SerializeField]
    private bool isControlling;

    private SocketIOComponent socket;
    public Player player;

    private void Awake()
    {
        isControlling = false;
    }
    public void SetControllerID(string ID)
    {
        this.id = ID;
        isControlling = (NetworkClient.ClientID == ID) ? true : false;
    }
    public void SetNum(int _num)
    {
        this.num = _num;
    }
    public void SetSocketReference(SocketIOComponent socket)
    {
        this.socket = socket;
    }
    public string GetID()
    {
        return id;
    }
    public int GetNum()
    {
        return num;
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
