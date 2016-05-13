using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class GoTo_Stage : Affordance {

	Vector3 pos;

	public GoTo_Stage(SmartCharacter afdnt, SmartStage afdee){

		affodant = afdnt;
		affordee = afdee;
		pos = afdee.stageInteract.position;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		name = affordantName + " moves to stage";

		preconditions.Add(new Condition(affordantName, "InScene", true));

		effects.Add (new Condition (affordantName, "OnStage", true));
		effects.Add (new Condition (affordantName, "StageAim", false));
		effects.Add (new Condition (affordantName, "InScene", false));
		effects.Add (new Condition (affordeeName, "IsSecure", false));


		treeRoot = this.execute ();

	}

	//Behaviour Tree here
	public Node execute() {

		return (new Sequence(this.affodant.gameObject.GetComponent<BehaviorMecanim> ().Node_GoTo (pos)));
	}
}
