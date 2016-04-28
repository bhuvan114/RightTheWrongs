using UnityEngine;
using System.Collections;

public class GunControllerScript : MonoBehaviour {

	public Transform hipHolder;
	public Transform handHolder;
	bool isHolding;


	public void SetIsHolding(bool status) {

		isHolding = status;
	}

	// Use this for initialization
	void Start () {
	
		isHolding = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (isHolding) {
			this.transform.position = handHolder.position;
			this.transform.rotation = handHolder.rotation;
		} else {
			this.transform.position = hipHolder.position;
			this.transform.rotation = hipHolder.rotation;
		}
	}
}
