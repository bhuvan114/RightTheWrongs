using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class Pres_GoTo_Mic : Affordance {

	Vector3 pos;

	public Pres_GoTo_Mic(SmartPresident afdnt, SmartStage afdee){

		affodant = afdnt;
		affordee = afdee;
		pos = afdee.micPos.position;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		name = "President goes to mic";

		preconditions.Add(new Condition(affordantName, "PresOnStage", true));

		effects.Add (new Condition (affordantName, "NearMic", true));


		treeRoot = this.execute ();

	}

	//Behaviour Tree here
	public Node execute() {

		return (new Sequence(this.affodant.gameObject.GetComponent<BehaviorMecanim> ().Node_GoTo (pos)));
	}
}
