using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public CameraShake cameraShake;
    public GameObject ball;

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        //ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(cameraShake.Shake(0.3f, 0.5f));
    }
}
