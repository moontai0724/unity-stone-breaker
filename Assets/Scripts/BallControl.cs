using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BallControl : MonoBehaviour
{
    public Vector2 initialVelocity = new Vector2(1.0f, 10.0f);
    public static bool started = false;
    public AudioSource deadAudio;

    void Start()
    {
        BallControl.started = false;
    }

    void Update()
    {
        if (Input.anyKeyDown && BallControl.started == false)
        {
            GetComponent<Rigidbody2D>().velocity = initialVelocity.x * UnityEngine.Random.Range(-1f, 1f) * Vector3.right + initialVelocity.y * Vector3.down;
            BallControl.started = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Deadline")
            this.lose();
    }

    void lose()
    {
        deadAudio.Play(0);
        SceneController.lose();
    }
}