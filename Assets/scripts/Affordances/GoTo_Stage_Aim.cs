using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class GoTo_Stage_Aim : Affordance {

	Vector3 pos;

	public GoTo_Stage_Aim(SmartCharacter afdnt, SmartStage afdee){

		affodant = afdnt;
		affordee = afdee;
		pos = afdee.stageAim.position;
		initialize ();
	}

	public GoTo_Stage_Aim (SmartAgent afdnt, SmartStage afdee){

		affodant = afdnt;
		affordee = afdee;
		pos = afdee.stageAim.position;
		initialize ();
	}

	public GoTo_Stage_Aim (SmartAssassin afdnt, SmartStage afdee){

		affodant = afdnt;
		affordee = afdee;
		pos = afdee.stageAim.position;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		preconditions.Add(new Condition(affordantName, "InScene", true));

		effects.Add (new Condition (affordantName, "OnStage", false));
		effects.Add (new Condition (affordantName, "StageAim", true));
		effects.Add (new Condition (affordantName, "Undetected", true));
		treeRoot = this.execute ();
	}

	//Behaviour Tree here
	public Node execute() {

		return (new Sequence(this.affodant.gameObject.GetComponent<BehaviorMecanim> ().Node_GoTo (pos)));
	}
}
