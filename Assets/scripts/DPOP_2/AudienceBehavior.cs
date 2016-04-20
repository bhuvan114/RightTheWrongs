using UnityEngine;
using System.Collections;

public class AudienceBehavior : MonoBehaviour {

	bool isPlayercontrolled;

	public void SetPlayerControl(bool control) {

		isPlayercontrolled = control;
	}

	// Use this for initialization
	void Start () {

		isPlayercontrolled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.E))
			isPlayercontrolled = false;

		if (!isPlayercontrolled) {

			//Default Audience Behavior
		}
	}
}
