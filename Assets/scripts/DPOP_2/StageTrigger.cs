using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class StageTrigger : MonoBehaviour {

	GoTo_Stage goToStage;
	private BehaviorAgent behaviorAgent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {


		Debug.LogError ("TriggerEntered : " + other.tag);

		if (other.tag == "SmartObject") {
			Debug.LogError ("TriggerEntered middle");
			if (other.gameObject.GetComponent<Player3PController> ()) {

				Debug.LogError ("TriggerEntered Final");
				goToStage = new GoTo_Stage (other.gameObject.GetComponent<SmartCharacter> (), this.transform.parent.gameObject.GetComponent<SmartStage> ());
				behaviorAgent = new BehaviorAgent (goToStage.UpdateStateForUI ());
				BehaviorManager.Instance.Register (behaviorAgent);
				behaviorAgent.StartBehavior ();
			}
		}
	}
}
