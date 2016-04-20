using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

	public GameObject cameraHolder;
	bool playerControls;
	GameObject playerObject;

	// Use this for initialization
	void Start () {
	
		playerControls = false;
		playerObject = null;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.E)) {
			playerControls = false;
			Destroy (playerObject.GetComponent<Player3PController> ());
		}

		if (!playerControls) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			cameraHolder.transform.position = cameraHolder.transform.position + movement;
		} else {
			
		}
	}
}
