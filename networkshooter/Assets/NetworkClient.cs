using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using WebSocketSharp.Net;

namespace SocketIO
{
    public class NetworkClient : SocketIOComponent
    {
        public InputField field;

        public override void Start()
        {
            field.text = url;
            base.Start();            
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
    }
}
