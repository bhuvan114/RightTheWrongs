using UnityEngine;
using System.Collections;

public class CameraControllerScript : MonoBehaviour {

	public GameObject cameraHolder;
	bool inGodsEyeView;

	// Use this for initialization
	void Start () {

		inGodsEyeView = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (inGodsEyeView) {
			this.transform.position = cameraHolder.transform.position;
			this.transform.rotation = cameraHolder.transform.rotation;
		}
	}
}
