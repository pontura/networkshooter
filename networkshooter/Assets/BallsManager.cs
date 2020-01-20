using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    public Vector2 area;
    public Ball ball;
    public List<Ball> all;
    public Transform ballsContainer;
    int originalDistance = 500;
    public float speed = 10;
    public float time_To_spawn = 0.25f;
    void Start()
    {
        Loop();
        Events.CatchBall += CatchBall;
    }
    private void OnDestroy()
    {
        Events.CatchBall -= CatchBall;
    }
    void CatchBall(Ball ball)
    {
        
    }
    void Loop()
    {
        AddBall();
        Invoke("Loop", time_To_spawn);
    }
    private void Update()
    {
        Ball ballToDestroy = null;
        foreach (Ball ball in all)
        {
            if (ball.transform.localPosition.z < -20)
                ballToDestroy = ball;
            ball.UpdateDistance(speed);
        }
        if (ballToDestroy)
            ResetBall(ballToDestroy);
    }
    void AddBall()
    {
        Ball newBall = Instantiate(ball, ballsContainer);
        float _x = Random.Range(-area.x, area.x);
        float _y = Random.Range(-area.y, area.y);
        newBall.transform.localPosition = new Vector3(_x, _y,  originalDistance);
        all.Add(newBall);
        newBall.Init(Random.Range(0, 3));
    }
    public void ResetBall(Ball ball)
    {
        all.Remove(ball);
        Destroy(ball.gameObject);
    }
}
