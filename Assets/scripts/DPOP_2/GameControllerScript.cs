using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class GameControllerScript : MonoBehaviour {

	public GameObject ObjectiveUIPanel;
	public GameObject objectiveFailure;
	public GameObject JournalPane;
	public GameObject PausePane;

	public DataCollection userMetrics;

	public GameObject cameraHolder;
	public CameraControllerScript cameraController;
	GameObject playerObject;
	float zoom;

	private CursorLockMode lockMode;
	private bool cursorVisibility;
	private float timeScale;

	private BehaviorAgent behaviorAgent;

	// Use this for initialization
	void Start () {

		playerObject = null;
		zoom = 0f;
		Time.timeScale = 0f;

		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = true;

		GameObject[] smartObjects;
		NSM.debugLog = "Before calling FindGameObjects11";

		smartObjects = GameObject.FindGameObjectsWithTag ("SmartObject");
		//NSM.debugLog = "Before calling helper!!";
		HelperFunctions.initiatePlanSpace_v2 (smartObjects);
		HelperFunctions.initiatePlanSpace ();
		//NSM.debugLog = "Before calling NSM!!";
		NSM.InitiateNarrativeStateManager ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0f) {
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
			if (NSM.hasPlan == false && NSM.isPlanning == false) {
				Debug.LogWarning ("Planner Failed!!");
				userMetrics.publishData();
				ObjectiveUIPanel.SetActive (true);
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				Time.timeScale = 0f;
				//userMetrics.publishData();
			}

			//if (behaviorAgent != null)
			//	Debug.Log(behaviorAgent.Status.ToString());
			if (NSM.hasPlan == true && !NSM.root.IsRunning) {
				Debug.LogError ("BT Updated!!");
				//BehaviorManager.Instance.ClearReceivers ();
				BehaviorManager.Instance.Deregister(behaviorAgent);
				NSM.root = new Sequence (NSM.root, this.TeriminatePlan ());
				behaviorAgent = new BehaviorAgent (NSM.root);
				BehaviorManager.Instance.Register (behaviorAgent);
				behaviorAgent.StartBehavior ();
			}
		}


		//Escape Controls
		if (Input.GetKeyDown ("escape") && !JournalPane.activeSelf) {	
			if (!PausePane.activeSelf) {
				//lockMode = Cursor.lockState;
				//cursorVisibility = Cursor.visible;
				Time.timeScale = 0f;
				PausePane.SetActive (true);
			} else {
				//Cursor.lockState = lockMode;
				//Cursor.visible = cursorVisibility;
				Time.timeScale = 1f;
				PausePane.SetActive (false);
			}
		}

		if (Input.GetKeyDown (KeyCode.J) && !PausePane.activeSelf) {

			if (JournalPane.activeSelf) {

				//Cursor.lockState = lockMode;
				//Cursor.visible = cursorVisibility;
				Time.timeScale = 1f;
				JournalPane.SetActive (false);
			} else {
				//lockMode = Cursor.lockState;
				//cursorVisibility = Cursor.visible;
				//timeScale = Time.timeScale;

				//Cursor.lockState = CursorLockMode.Locked;
				//Cursor.visible = true;
				Time.timeScale = 0f;
				JournalPane.SetActive (true);
				NSM.UpdateJournal ();
			}
		}
	}

	public Node TeriminatePlan () {

		return new LeafInvoke(
			() => this.TerimateNarrative());
	}	

	public void TerimateNarrative() {
		Debug.LogWarning ("Debug log called : " + NSM.planTimes);
		userMetrics.publishData();
		objectiveFailure.SetActive (true);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0f;
	}


	void OnGUI() {
		/*if (NSM.hasPlan == false) {
			GUI.color = Color.white;
			GUI.Box(new Rect(20, 20, 200, 25), NSM.debugLog);
		}*/
		if (NSM.isJournalUpdated)
		{
			GUI.color = Color.white;
			if (NSM.first) {
				GUI.Box (new Rect (40, Screen.height - 30, 250, 25), "Press 'J' to view the journal!!");
			} else {
				GUI.Box (new Rect (40, Screen.height - 30, 250, 25), "You've changed the past!! ('J')");
			}
		}
	}
}
