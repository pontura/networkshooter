using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        Ball ball = col.GetComponent<Ball>();
        if (ball == null)
            return;
        ball.AttachTo(transform);
        Events.CatchBall(ball);
    }
}
