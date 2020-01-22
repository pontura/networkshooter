using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class PadController : MonoBehaviour
{
    states state;
    enum states
    {
        IDLE,
        OVER
    }
    public RectTransform padArea;
    float multiplier = 2000;
    public NetworkClient networkClient;
    public GameObject target;
    void Start()
    {

    }
    Vector3 lastNormalizedPoint;
    private void Update()
    {
        if (networkClient.networkIdentity == null || networkClient.networkIdentity.player == null)
            return;
        print("CACA");
        if (state == states.IDLE)
        {
            target.SetActive(false);
            return;
        }
        else
        {
            target.SetActive(true);
        }
        Vector3 normalizedPoint = GetPosition();
        if (lastNormalizedPoint == normalizedPoint)
            return;
        lastNormalizedPoint = normalizedPoint;
        if (normalizedPoint == Vector3.zero)
            return;

        networkClient.networkIdentity.player.position.x = (int)Mathf.Round(normalizedPoint.x);
        networkClient.networkIdentity.player.position.y = (int)Mathf.Round(normalizedPoint.y);

        SocketIOComponent socketIo = networkClient.networkIdentity.GetSocket();
        socketIo.Emit("updatePosition", new JSONObject(JsonUtility.ToJson(networkClient.networkIdentity.player)));

        Events.OnDebbugText(networkClient.networkIdentity.player.position.x.ToString());
    }
    public void ButtonClicked()
    {
        SocketIOComponent socketIo = networkClient.networkIdentity.GetSocket();
        socketIo.Emit("shoot", new JSONObject(JsonUtility.ToJson(networkClient.networkIdentity.player)));
    }
    Vector2 GetPosition()
    {

#if UNITY_EDITOR
       
        Vector3 MousePos = Input.mousePosition;
#else
        Vector3 MousePos = Vector3.zero;
        if (Input.touchCount == 0)
            return Vector3.zero;
        if (Input.touchCount > 1000)
        {
            float last_x = -10000;
            foreach (Touch t in Input.touches)
            {
                if (t.position.x < last_x)
                {        
                    MousePos = t.position;
                    last_x = MousePos.x;
                }
            }
        }
        else
            MousePos = Input.touches[0].position;
#endif
        target.transform.position = MousePos;
        Vector2 originalPos = MousePos;
        MousePos -= transform.position;
        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(padArea, originalPos, GetComponentInParent<Canvas>().worldCamera, out localpoint);

        Vector2 normalizedPoint = Rect.PointToNormalized(padArea.rect, localpoint);
        normalizedPoint *= multiplier;
        normalizedPoint.x -= multiplier / 2;
        normalizedPoint.y -= multiplier / 2;
        normalizedPoint.x = (int)(normalizedPoint.x);
        normalizedPoint.y = (int)(normalizedPoint.y);

        return normalizedPoint;
    }
   
    public void OnMouseEnter()
    {
        state = states.OVER;
    }
    public void OnMouseExit()
    {
        state = states.IDLE;
    }
}
    
