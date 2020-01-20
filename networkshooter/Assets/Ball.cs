using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int id;
    public MeshRenderer meshRenderer;
    public Transform attachedTo;

    public void Init(int id)
    {
        this.id = id;
        if (id == 0)
            meshRenderer.material.color = Color.black;
        else
            meshRenderer.material.color = Data.Instance.settings.GetColor(id-1);
    }
    public void UpdateDistance(float speed)
    {
        if (attachedTo != null)
            MoveAttacehd();
        else
            MoveForward(speed);
    }
    public void AttachTo(Transform t)
    {
        attachedTo = t;
        Invoke("ResetAttach", 0.3f);
    }
    public void MoveForward(float speed)
    {
        Vector3 pos = transform.localPosition;
        pos.z -= speed * Time.deltaTime;
        transform.localPosition = pos;
    }
    void MoveAttacehd()
    {
        Vector3 pos = transform.position;
        transform.position = Vector3.Lerp(pos, attachedTo.position, 0.3f);
    }
    void ResetAttach()
    {
        GameManager.Instance.GetComponent<BallsManager>().ResetBall(this);
    }
}
