using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class StageTrigger : MonoBehaviour {

	GoTo_Stage goToStage;
	Pres_Leave_Stage leaveStage;
	private BehaviorAgent behaviorAgent;
	bool showMessage;

	// Use this for initialization
	void Start () {

		showMessage = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {


		Debug.LogError ("TriggerEntered : " + other.tag);

		if (other.tag == "SmartObject") {
			//Debug.LogError ("TriggerEntered middle");
			if (other.gameObject.GetComponent<Player3PController> ()) {

				//Debug.LogError ("TriggerEntered Final");
				if (!NSM.IsConditionInCurrentState (new Condition ("President", "SpeechGiven", true))) {


					showMessage = true;
					SmartPresident pres = GameObject.Find ("President").GetComponent<SmartPresident> ();
					SmartStage stage = this.transform.parent.gameObject.GetComponent<SmartStage> ();
					leaveStage = new Pres_Leave_Stage (pres, stage);
					behaviorAgent = new BehaviorAgent (leaveStage.UpdateStateForUI ());
					BehaviorManager.Instance.Register (behaviorAgent);
					behaviorAgent.StartBehavior ();
				}
				/*goToStage = new GoTo_Stage (other.gameObject.GetComponent<SmartCharacter> (), this.transform.parent.gameObject.GetComponent<SmartStage> ());
				behaviorAgent = new BehaviorAgent (goToStage.UpdateStateForUI ());
				BehaviorManager.Instance.Register (behaviorAgent);
				behaviorAgent.StartBehavior ();*/
			}
		}
	}

	void OnTriggerExit(Collider other) {

		if (other.tag == "SmartObject") {
			showMessage = false;
			//if (other.gameObject.GetComponent<Player3PController> ()) {
			//}
		}
	}

	void OnGUI(){

		if (showMessage) {
			GUI.Box (new Rect (40, /*Screen.height + */30, 450, 25), "The stage is no longer secure and speech is cancelled!!");
		}
	}
}
