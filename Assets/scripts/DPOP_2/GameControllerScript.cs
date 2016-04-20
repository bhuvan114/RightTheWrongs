using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

	public GameObject cameraHolder;
	public CameraControllerScript cameraController;
	GameObject playerObject;
	float zoom;

	// Use this for initialization
	void Start () {

		playerObject = null;
		zoom = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (cameraController.inGodsEyeView) {
			zoom = 0f;
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			if (Input.GetKey (KeyCode.Z))
				zoom = -1f;
			if (Input.GetKey (KeyCode.X))
				zoom = 1f;
			Vector3 movement = new Vector3 (moveHorizontal, zoom, moveVertical);

			cameraHolder.transform.position = cameraHolder.transform.position + movement;
		}
	}
}
