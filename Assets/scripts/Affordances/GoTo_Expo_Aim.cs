using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class GoTo_Expo_Aim : Affordance {

	Vector3 pos;

	public GoTo_Expo_Aim(SmartAssassin afdnt, SmartExpo afdee){

		affodant = afdnt;
		affordee = afdee;
		pos = afdee.expoAim.position;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		name = affordantName + " moves near exposition";

		preconditions.Add(new Condition(affordantName, "InScene", true));
		preconditions.Add (new Condition ("President", "PresAtExpo", true));
		effects.Add (new Condition (affordantName, "Undetected", true));
		//preconditions.Add (new Condition ("President", "PresAtExpo", true));

		effects.Add (new Condition(affordantName, "ExpoAim", true));

		treeRoot = this.execute ();
	}

	//Behaviour Tree here
	public Node execute() {

		return new Sequence (this.affodant.gameObject.GetComponent<BehaviorMecanim> ().Node_GoTo (pos));
	}
}
