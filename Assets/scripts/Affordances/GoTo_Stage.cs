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

	public GoTo_Stage(SmartAgent afdnt, SmartStage afdee){

		affodant = afdnt;
		affordee = afdee;
		pos = afdee.stageInteract.position;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		preconditions.Add(new Condition(affordantName, "InScene", true));

		effects.Add (new Condition (affordantName, "OnStage", true));
		effects.Add (new Condition (affordantName, "StageAim", false));
		treeRoot = this.execute ();

	}

	//Behaviour Tree here
	public Node execute() {

		return (new Sequence(this.affodant.gameObject.GetComponent<BehaviorMecanim> ().Node_GoTo (pos)));
	}
}
