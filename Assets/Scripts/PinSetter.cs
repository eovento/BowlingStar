using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Pin))]

public class PinSetter : MonoBehaviour {

    public Text standingDisplay; 
    public GameObject pinSet;

    private Ball ball;
    private bool ballOutOfPlay = false;
    private float lastChangeTime;
    private int lastStandingCount = -1;
    private int lastSettledCount = 10;
    private Animator animator;
    
    ActionMaster actionMaster = new ActionMaster();
    
	// Use this for initialization
	void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();
        
        if (ballOutOfPlay) {
            CheckStanding();
            standingDisplay.color = Color.red;
        }
	}
    
    int CountStanding () {
        int standing = 0;
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.isStanding()) {
                standing++;
            }
        }
        return standing;
    }

    void OnTriggerExit(Collider collider) {
        GameObject thingLeft = collider.gameObject;
        
        // Pins get out of the play box
        if (thingLeft.GetComponentInParent<Pin>()) {
            Destroy(thingLeft.transform.parent.gameObject);
        }
    }
    
    void CheckStanding () {
        // Update the lastStandingCount
        // Call PinsHaveSettled() when they have
        int currentStanding = CountStanding();
        
        if (currentStanding != lastStandingCount) {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f;
        if ((Time.time - lastChangeTime) > settleTime) {
            PinsHaveSettled();
        }
        
    }
    
    void PinsHaveSettled () {
        int pinFall = lastSettledCount - CountStanding();
        lastSettledCount = CountStanding();

        ActionMaster.Action action = actionMaster.Bowl(pinFall);
        Debug.Log("You just hit: " + pinFall + " pins." + "    Current action: " + action);
        
        if (action == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
        } else if (action == ActionMaster.Action.EndTurn) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        } else if (action == ActionMaster.Action.Reset) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        } else if (action == ActionMaster.Action.EndGame) {
            throw new UnityException("Still thinking how to handle End Game.");
        }
         
        ball.Reset();
        lastStandingCount = -1;
        ballOutOfPlay = false;
        standingDisplay.color = Color.green;
    }
    
    public void RenewPins () {
        Debug.Log("Renewing pins.");
        Instantiate(pinSet, new Vector3(0, 0, 1829), Quaternion.identity);
    }
    
    public void RaisePins () {
        Debug.Log("Raising pins.");
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
        pin.RaiseIfStanding();
        }        
    }
    
    public void LowerPins () {
        Debug.Log("Lowering Pins.");
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
        pin.Lower();
        }
    }
    
    public void SetBallOutOfPlay () {
        ballOutOfPlay = true;
    }

}