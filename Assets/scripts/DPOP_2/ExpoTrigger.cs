using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class ExpoTrigger : MonoBehaviour {

	SmartPresident pres;
	SmartCharacter player;
	bool playerInTrigger;
	bool doctorInTrigger;
	bool showMessage;
	UseXRay xray;

	private BehaviorAgent behaviorAgent;
	private Node root = null;
	// Use this for initialization
	void Start () {
	
		pres = GameObject.Find ("President").GetComponent<SmartPresident> ();
		showMessage = false;
		playerInTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {

		/*if (playerInTrigger) {
			
			//if (NSM.IsConditionInCurrentState (new Condition ("President", "IsShot", true))) {
			if (doctorInTrigger) {
				showMessage = true;
			} else {
				showMessage = false;
			}
		}*/
		if (Input.GetKeyDown (KeyCode.F)) {
			if (playerInTrigger && doctorInTrigger) {
				Debug.LogError ("Going to run XRAY!!");
				xray = new UseXRay (player, pres);
				behaviorAgent = new BehaviorAgent (xray.UpdateStateForUI ());
				BehaviorManager.Instance.Register (behaviorAgent);
				behaviorAgent.StartBehavior ();
			}
		}
	}

	void OnTriggerEnter (Collider other) {

		Debug.LogWarning ("Entered - " + other.name);
		if (other.gameObject.GetComponent<Player3PController> ()) {
			playerInTrigger = true;
			player = other.gameObject.GetComponent<SmartCharacter> ();
		} else if(other.name.Equals("Doctor")) {
			doctorInTrigger = true;
		}
	}

	void OnTriggerExit (Collider other) {


		if (other.gameObject.GetComponent<Player3PController> ()) {
			playerInTrigger = false;
		} else if(other.name.Equals("Doctor")) {
			doctorInTrigger = false;
		}
	}

	/*void OnTriggerStay (Collider other) {

		if (other.gameObject.GetComponent<Player3PController> ()) {
			if (NSM.IsConditionInCurrentState (new Condition ("President", "IsShot", true))) {
				Debug.Log ("Expo activated");
				showMessage = true;
			}
		}
	}*/

	void OnGUI () {

		if (playerInTrigger && doctorInTrigger) {
			GUI.Box (new Rect (/*Screen.width-*/40, /*Screen.height + */30, 350, 25), "Press 'F' to use the X-Ray");
		} else if (doctorInTrigger) {
			GUI.Box (new Rect (/*Screen.width-*/40, /*Screen.height + */30, 350, 25), "Hint : Go to president to save him");
		}
	}
}
