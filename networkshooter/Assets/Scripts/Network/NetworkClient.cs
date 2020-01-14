using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using WebSocketSharp.Net;
using System;

namespace SocketIO
{
    public class NetworkClient : SocketIOComponent
    {
        public Vector2 centerOffset;
        public static float offsetScale = 0.4f;

        public Transform playersContainer;
        public NetworkIdentity playerToInstantiate;
        public InputField field;
        private Dictionary<string, NetworkIdentity> serverObjects;
        public GameObject loginPanel;
        public static string ClientID { get; private set; }
        public bool isPlayerAddedToScene;

        public override void Start()
        {
            loginPanel.SetActive(true);
            SetupEvents();
            field.text = url;
            base.Start();
            Initialize();            
        }
        private void Initialize()
        {
            serverObjects = new Dictionary<string, NetworkIdentity>();
        }
        public override void Update()
        {
            base.Update();
        }

        public void Clicked()
        {
            ws = new WebSocket(field.text);
            url = field.text;
            Connect();
        }
        void SetupEvents()
        {
            On("open", (E) =>
                {
                    loginPanel.SetActive(false);
                    DebbugText("Se conecto al server");
                }
            );
            On("register", (E) =>
                {
                    ClientID = RemoveQuotes(E.data["id"].ToString());
                    loginPanel.SetActive(false);
                    DebbugText("register " + ClientID);
                }
            );
            On("disconnected", (E) =>
            {
                string id = RemoveQuotes(E.data["id"].ToString());

                DebbugText("disconnect " + id);

                if (id == ClientID)
                    loginPanel.SetActive(true);
                
                // Destroy(serverObjects[id].gameObject);
                Events.OnRemovePlayer(serverObjects[id]);
                serverObjects.Remove(id);
            }
          );
            On("spawn", (E) =>
            {
                AddPlayer(E.data["id"].ToString());               
            }
         );
            On("updatePosition", (E) =>
            {
                if (GameManager.Instance.type == GameManager.types.CLIENT)
                    return;
                string id = RemoveQuotes(E.data["id"].ToString());
                if (id == ClientID)
                    return;
                float x = offsetScale * ( centerOffset.x + float.Parse(E.data["position"]["x"].ToString()) / 1000); // lo hace float;
                float y = offsetScale * ( centerOffset.y + float.Parse(E.data["position"]["y"].ToString()) / 1000);
                NetworkIdentity ni = serverObjects[id];
                ni.transform.position = new Vector3(x, 0, y);
                //Debug.Log("SUMA " + id + " x: " + x + " y: " + y + " " + E.data["position"]["y"].ToString());
            }
         );
        }
        
        void DebbugText(string text)
        {
            Debug.Log(text);
        }
        string RemoveQuotes(string t)
        {
            string newText = "";
            newText = t.Remove(0, 1);
            return newText.Remove(newText.Length - 1, 1);
        }
        void AddPlayer(string text)
        {
            string id = RemoveQuotes(text);
            if (id == ClientID)
            {
                if (isPlayerAddedToScene)
                    return;
                isPlayerAddedToScene = true;
            }
            DebbugText("AddPlayer " + id);
            NetworkIdentity player;
            if (GameManager.Instance.type == GameManager.types.SERVER)
                player = Instantiate(playerToInstantiate, playersContainer);
            else
                player = new NetworkIdentity();
            player.SetControllerID (id);
            player.SetSocketReference(this);
            serverObjects.Add(id, player);
            Events.OnAddPlayer(player);
        }
        public NetworkIdentity GetNetworkIdentity()
        {
            if (ClientID == "")
                return null;
            NetworkIdentity ni  = serverObjects[ClientID];
            if (ni != null)
                return ni;
            return null;
        }
    }

    [Serializable]
    public class Player
    {
        public string id;
        public Position position;
    }
    [Serializable]
    public class Position
    {
        public float x;
        public float y;
    }
}
