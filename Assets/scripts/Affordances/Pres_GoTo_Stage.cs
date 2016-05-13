using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class Pres_GoTo_Stage : Affordance {

	Vector3 pos;

	public Pres_GoTo_Stage(SmartPresident afdnt, SmartStage afdee){

		affodant = afdnt;
		affordee = afdee;
		pos = afdee.presStageInteract.position;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		name = "President goes to stage";

		preconditions.Add(new Condition(affordantName, "InScene", true));
		//preconditions.Add(new Condition ("Agent", "OnStage", true));
		preconditions.Add(new Condition (affordeeName, "IsSecure", true));
		//preconditions.Add (new Condition("Assassin", "StageAttempt", true));

		effects.Add (new Condition (affordantName, "PresOnStage", true));
		effects.Add (new Condition (affordantName, "InScene", false));

		treeRoot = this.execute ();
	}

	//Behaviour Tree here
	public Node execute() {

		return new Sequence (this.affodant.gameObject.GetComponent<BehaviorMecanim> ().Node_GoTo (pos));
	}
}
