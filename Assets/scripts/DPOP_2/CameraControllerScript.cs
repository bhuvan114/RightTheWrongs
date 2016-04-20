using UnityEngine;
using System.Collections;

public class CameraControllerScript : MonoBehaviour {

	public GameObject cameraHolder;
	Player3PController playerController;
	GameObject player;
	public bool inGodsEyeView;

	private float angleH = 0;
	private float angleV = 0;
	public float horizontalAimingSpeed = 400f;
	public float verticalAimingSpeed = 400f;
	public float maxVerticalAngle = 30f;
	public float minVerticalAngle = -60f;
	public Vector3 pivotOffset = new Vector3(0.0f, 1.0f,  0.0f);
	public Vector3 camOffset   = new Vector3(0.0f, 0.7f, -3.0f);
	public float smooth = 10f;
	private Vector3 smoothPivotOffset;
	private Vector3 smoothCamOffset;
	private Vector3 targetPivotOffset;
	private Vector3 targetCamOffset;
	private Vector3 relCameraPos;
	private float relCameraPosMag;

	// Use this for initialization
	void Start () {

		inGodsEyeView = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.E)) {
			inGodsEyeView = inGodsEyeView ? false : true;
			if (inGodsEyeView) {
				Debug.LogError ("Destroyin component!!");
				Destroy (player.GetComponent<Player3PController> ());
			}
		}
		
		if (inGodsEyeView) {
			this.transform.position = cameraHolder.transform.position;
			this.transform.rotation = cameraHolder.transform.rotation;

			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hit = new RaycastHit ();
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit, 1000)) {
					if (hit.collider.gameObject.CompareTag ("PlayerControllable")) {
						Debug.LogError (hit.collider.tag);
						playerController = new Player3PController ();
						player = hit.collider.gameObject.transform.parent.gameObject;
						player.AddComponent (typeof(Player3PController));
						relCameraPos = this.transform.position - player.transform.position;
						relCameraPosMag = relCameraPos.magnitude - 0.5f;
						smoothPivotOffset = pivotOffset;
						smoothCamOffset = camOffset;
						inGodsEyeView = false;
					}
				}
			}
		} 

	}

	void LateUpdate()
	{
		if (!inGodsEyeView) {
			angleH += Mathf.Clamp (Input.GetAxis ("Mouse X"), -1, 1) * horizontalAimingSpeed * Time.deltaTime;
			angleV += Mathf.Clamp (Input.GetAxis ("Mouse Y"), -1, 1) * verticalAimingSpeed * Time.deltaTime;

			angleV = Mathf.Clamp (angleV, minVerticalAngle, maxVerticalAngle);

			Quaternion aimRotation = Quaternion.Euler (-angleV, angleH, 0);
			Quaternion camYRotation = Quaternion.Euler (0, angleH, 0);
			this.transform.rotation = aimRotation;

	
			targetCamOffset = camOffset;

			// Test for collision
			Vector3 baseTempPosition = player.transform.position + camYRotation * pivotOffset;
			Vector3 tempOffset = targetCamOffset;
			for (float zOffset = targetCamOffset.z; zOffset < 0; zOffset += 0.5f) {
				tempOffset.z = zOffset;
				if (DoubleViewingPosCheck (baseTempPosition + aimRotation * tempOffset)) {
					targetCamOffset.z = tempOffset.z;
					break;
				}
			}
			smoothPivotOffset = Vector3.Lerp (smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
			smoothCamOffset = Vector3.Lerp (smoothCamOffset, targetCamOffset, smooth * Time.deltaTime);

			this.transform.position = player.transform.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset;
		}

	}

	bool DoubleViewingPosCheck(Vector3 checkPos)
	{
		return ViewingPosCheck (checkPos) && ReverseViewingPosCheck (checkPos);
	}

	bool ViewingPosCheck (Vector3 checkPos)
	{
		RaycastHit hit;

		// If a raycast from the check position to the player hits something...
		if(Physics.Raycast(checkPos, player.transform.position - checkPos, out hit, relCameraPosMag))
		{
			// ... if it is not the player...
			if(hit.transform != player && !hit.transform.GetComponent<Collider>().isTrigger)
			{
				// This position isn't appropriate.
				return false;
			}
		}
		// If we haven't hit anything or we've hit the player, this is an appropriate position.
		return true;
	}

	bool ReverseViewingPosCheck(Vector3 checkPos)
	{
		RaycastHit hit;

		if(Physics.Raycast(player.transform.position, checkPos - player.transform.position, out hit, relCameraPosMag))
		{
			if(hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
			{
				return false;
			}
		}
		return true;
	}

}
