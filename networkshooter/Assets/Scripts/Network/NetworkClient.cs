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
        public static float offsetScale = 0.4f;
        public Vector2 aspectRatio;

        public Transform playersContainer;
        public NetworkIdentity networkIdentity_to_instantiate;
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
                Destroy(serverObjects[id].gameObject);
                serverObjects.Remove(id);
            }
          );
            On("spawn", (E) =>
            {
                DebbugText("spawn num: " + E.data["num"]);
                AddPlayer(E.data["id"].ToString(), int.Parse(E.data["num"].ToString()));               
            }
         );
            On("updatePosition", (E) =>
            {
                if (Data.Instance.type == Data.types.CLIENT)
                    return;
                string id = RemoveQuotes(E.data["id"].ToString());
                if (id == ClientID)
                    return;
                float x = offsetScale * ( float.Parse(E.data["position"]["x"].ToString())); // lo hace float;
                float y = offsetScale * ( float.Parse(E.data["position"]["y"].ToString()));
                NetworkIdentity ni = serverObjects[id];
                ni.transform.position = new Vector3(x* aspectRatio.x, y*aspectRatio.y, 0)/1000;
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
        public NetworkIdentity networkIdentity;
        
        void AddPlayer(string text, int num)
        {
            string id = RemoveQuotes(text);
            NetworkIdentity ni = null;
            if (id == ClientID)
            {
                if (isPlayerAddedToScene)
                    return;
                isPlayerAddedToScene = true;
              //  ni = new NetworkIdentity();
            //    print("ASDASDASDA " + ni);
            }
            //else if (Data.Instance.type == Data.types.SERVER)
            //{
                ni = Instantiate(networkIdentity_to_instantiate);
                ni.transform.SetParent(playersContainer);
           // }
          //  if (ni == null)
        //        return;
            ni.SetNum(num);
            ni.SetControllerID (id);
            ni.SetSocketReference(this);
            serverObjects.Add(id, ni);
            OnAddPlayer(ni);
        }
        void OnAddPlayer(NetworkIdentity ni)
        {           

            print("ADD PLAYER " + ni.GetID() + " server id: " +  NetworkClient.ClientID);
            
            ni.player = new Player();
            ni.player.position = new Position();
            ni.player.position.x = ni.player.position.y = 0;

            if (ni.GetID() == ClientID)
                networkIdentity = ni;

            if (Data.Instance.type == Data.types.SERVER)
                Events.OnAddPlayer(ni);
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
        public int num;
    }
    [Serializable]
    public class Position
    {
        public float x;
        public float y;
    }
}
