using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class Pres_GoToExpo : Affordance {

	Vector3 pos;

	public Pres_GoToExpo(SmartPresident afdnt, SmartExpo afdee){

		affodant = afdnt;
		affordee = afdee;
		pos = afdee.expoInteract.position;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		name = "President goes to expo";

		preconditions.Add(new Condition(affordantName, "InScene", true));
		preconditions.Add (new Condition (affordantName, "SpeechGiven", true));

		effects.Add (new Condition (affordantName, "PresAtExpo", true));

		treeRoot = this.execute ();
	}

	//Behaviour Tree here
	public Node execute() {

		return new Sequence (this.affodant.gameObject.GetComponent<BehaviorMecanim> ().Node_GoTo (pos));
	}
}
