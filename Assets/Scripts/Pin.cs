using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{

    public float standingThreshold = 3f;
    public float distToRaise = 40f;
    
    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        isStanding();
    }
    

     // FIRST ATTEMPT:
     
        //public bool isStanding () {
        //    Vector3 rotationInEuler = transform.rotation.eulerAngles;
    
        //    float tiltInX = Mathf.Abs(rotationInEuler.x);
        //    float tiltInZ = Mathf.Abs(rotationInEuler.z);
    
        //    if (tiltInX < standingThreshold && tiltInZ < standingThreshold) {
        //        return true;            
        //    } else {
        //        return false;
        //    }
        //}


     // SECOND ATTEMPT:
     
        //public bool isStanding () {
        //    Vector3 rotationInEuler = transform.rotation.eulerAngles;
    
        //    //jdg: note this is called every tick
        //    float tiltInX = Mathf.DeltaAngle(rotationInEuler.x,0);
        //    float tiltInZ = Mathf.DeltaAngle(rotationInEuler.z,0);
    
        //    Vector3 position = transform.position;
        //    float positionY = transform.position.y;
    
        //    if (positionY > 0 || (tiltInX < standingThreshold && tiltInZ < standingThreshold))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    } 
        //}
    

     // THIRD (AND WORKING!) ATTEMPT:
     
    public bool isStanding()
    {

        float tiltX = (transform.eulerAngles.x < 180f) ? transform.eulerAngles.x : 360 - transform.eulerAngles.x;
        float tiltZ = (transform.eulerAngles.z < 180f) ? transform.eulerAngles.z : 360 - transform.eulerAngles.z;

        if (tiltX > standingThreshold || tiltZ > standingThreshold)
        {
            return false;
        }

        return true;
    }
    
    public void RaiseIfStanding () {
        if (isStanding ()) {
            rigidBody.useGravity = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rigidBody.freezeRotation = true;
            transform.Translate(new Vector3(0, distToRaise, 0), Space.World);
        }
    }        
    
    public void Lower () {
            rigidBody.useGravity = true;
            transform.Translate(new Vector3(0, -distToRaise, 0), Space.World);
    }
    
}