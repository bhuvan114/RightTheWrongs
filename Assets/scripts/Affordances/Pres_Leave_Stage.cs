using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class Pres_Leave_Stage : Affordance {

	Vector3 pos;

	public Pres_Leave_Stage(SmartPresident afdnt, SmartStage afdee){

		affodant = afdnt;
		affordee = afdee;
		pos = afdee.exitPos.position;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		name = "President leaves stage";

		preconditions.Add(new Condition(affordantName, "InScene", false));
		preconditions.Add(new Condition (affordantName, "PresOnStage", true));

		effects.Add (new Condition (affordantName, "PresOnStage", false));
		effects.Add (new Condition (affordantName, "InScene", true));
		effects.Add (new Condition (affordeeName, "IsSecure", false));

		treeRoot = this.execute ();
	}

	//Behaviour Tree here
	public Node execute() {

		return new Sequence (this.affodant.gameObject.GetComponent<BehaviorMecanim> ().Node_GoTo (pos));
	}
}
