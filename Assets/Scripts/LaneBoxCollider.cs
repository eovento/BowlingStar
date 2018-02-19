using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBoxCollider : MonoBehaviour {

    private PinSetter pinSetter;

	// Use this for initialization
	void Start () {
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
	}

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.name == "Ball") {
            pinSetter.SetBallOutOfPlay();
        }
    }

}
