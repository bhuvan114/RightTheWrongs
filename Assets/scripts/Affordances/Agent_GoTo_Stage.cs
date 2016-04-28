using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class Agent_GoTo_Stage : Affordance {

	Vector3 pos;

	public Agent_GoTo_Stage(SmartAgent afdnt, SmartStage afdee){

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
		effects.Add (new Condition (affordantName, "InScene", false));


		treeRoot = this.execute ();

	}

	//Behaviour Tree here
	public Node execute() {

		return (new Sequence(this.affodant.gameObject.GetComponent<BehaviorMecanim> ().Node_GoTo (pos)));
	}
}
