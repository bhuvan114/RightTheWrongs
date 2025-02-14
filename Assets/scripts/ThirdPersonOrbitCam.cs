﻿using UnityEngine;
using System.Collections;

public class ThirdPersonOrbitCam : MonoBehaviour 
{
	public Transform player;
	public Texture2D crosshair;
	public Transform godsEye;
	
	public Vector3 pivotOffset = new Vector3(0.0f, 1.0f,  0.0f);
	public Vector3 camOffset   = new Vector3(0.0f, 0.7f, -3.0f);
	
	public float smooth = 10f;
	
	public Vector3 aimPivotOffset = new Vector3(0.0f, 1.7f,  -0.3f);
	public Vector3 aimCamOffset   = new Vector3(0.8f, 0.0f, -1.0f);
	
	public float horizontalAimingSpeed = 400f;
	public float verticalAimingSpeed = 400f;
	public float maxVerticalAngle = 30f;
	public float flyMaxVerticalAngle = 60f;
	public float minVerticalAngle = -60f;
	
	public float mouseSensitivity = 0.3f;
	
	public float sprintFOV = 100f;
	
	private Player3PController playerControl;
	private float angleH = 0;
	private float angleV = 0;
	private Transform cam;
	
	private Vector3 relCameraPos;
	private float relCameraPosMag;
	
	private Vector3 smoothPivotOffset;
	private Vector3 smoothCamOffset;
	private Vector3 targetPivotOffset;
	private Vector3 targetCamOffset;
	
	private float defaultFOV;
	private float targetFOV;
	
	void Awake()
	{
		cam = transform;
		playerControl = player.GetComponent<Player3PController> ();
		
		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;
		
		smoothPivotOffset = pivotOffset;
		smoothCamOffset = camOffset;
		
		defaultFOV = cam.GetComponent<Camera>().fieldOfView;
	}
	
	void LateUpdate()
	{
		if (!Input.GetKey (KeyCode.C)) {
			angleH += Mathf.Clamp (Input.GetAxis ("Mouse X"), -1, 1) * horizontalAimingSpeed * Time.deltaTime;
			angleV += Mathf.Clamp (Input.GetAxis ("Mouse Y"), -1, 1) * verticalAimingSpeed * Time.deltaTime;
		
			// fly
			/*if(playerControl.IsFlying())
		{
			angleV = Mathf.Clamp(angleV, minVerticalAngle, flyMaxVerticalAngle);
		}
		else
		{*/
			angleV = Mathf.Clamp (angleV, minVerticalAngle, maxVerticalAngle);
			//}
		
		
			Quaternion aimRotation = Quaternion.Euler (-angleV, angleH, 0);
			Quaternion camYRotation = Quaternion.Euler (0, angleH, 0);
			cam.rotation = aimRotation;
		
			if (playerControl.IsAiming ()) {
				targetPivotOffset = aimPivotOffset;
				targetCamOffset = aimCamOffset;
			} else {
				targetPivotOffset = pivotOffset;
				targetCamOffset = camOffset;
			}
		
			//if(playerControl.isSprinting())
			//{
			//	targetFOV = sprintFOV;
			//}
			//else
			//{
			targetFOV = defaultFOV;
			//}
			cam.GetComponent<Camera> ().fieldOfView = Mathf.Lerp (cam.GetComponent<Camera> ().fieldOfView, targetFOV, Time.deltaTime);
		
			// Test for collision
			Vector3 baseTempPosition = player.position + camYRotation * targetPivotOffset;
			Vector3 tempOffset = targetCamOffset;
			for (float zOffset = targetCamOffset.z; zOffset < 0; zOffset += 0.5f) {
				tempOffset.z = zOffset;
				if (DoubleViewingPosCheck (baseTempPosition + aimRotation * tempOffset)) {
					targetCamOffset.z = tempOffset.z;
					break;
				}
			}
		
			// fly
			/*if(playerControl.IsFlying())
		{
			targetCamOffset.y = 0;
		}*/
		
			smoothPivotOffset = Vector3.Lerp (smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
			smoothCamOffset = Vector3.Lerp (smoothCamOffset, targetCamOffset, smooth * Time.deltaTime);
		
			cam.position = player.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset;
		} else {

			cam.rotation = godsEye.rotation;
			cam.position = godsEye.position;
			cam.GetComponent<Camera> ().fieldOfView = Mathf.Lerp (cam.GetComponent<Camera> ().fieldOfView, defaultFOV, Time.deltaTime);
		}
		
	}
	
	// concave objects doesn't detect hit from outside, so cast in both directions
	bool DoubleViewingPosCheck(Vector3 checkPos)
	{
		return ViewingPosCheck (checkPos) && ReverseViewingPosCheck (checkPos);
	}
	
	bool ViewingPosCheck (Vector3 checkPos)
	{
		RaycastHit hit;
		
		// If a raycast from the check position to the player hits something...
		if(Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
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
		
		if(Physics.Raycast(player.position, checkPos - player.position, out hit, relCameraPosMag))
		{
			if(hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
			{
				return false;
			}
		}
		return true;
	}
	
	// Crosshair
	void OnGUI () 
	{
		float mag = Mathf.Abs ((aimPivotOffset - smoothPivotOffset).magnitude);
		if (playerControl.IsAiming() &&  mag < 0.05f)
			GUI.DrawTexture(new Rect(Screen.width/2-(crosshair.width*0.5f), 
			                         Screen.height/2-(crosshair.height*0.5f), 
			                         crosshair.width, crosshair.height), crosshair);
	}
}
