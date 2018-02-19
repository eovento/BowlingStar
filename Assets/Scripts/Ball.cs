using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public Vector3 launchVelocity;

    public bool inPlay = false;

    private Vector3 ballInitalPosition;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    
    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        ballInitalPosition = transform.position;
    }
    
    public void Launch(Vector3 velocity) {
        inPlay = true;
        rigidBody.useGravity = true;
        rigidBody.velocity = velocity;
        
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void Reset()
    {
        Debug.Log("Resetting ball.");
        inPlay = false;
        transform.position = ballInitalPosition;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update () {
    
    }
}