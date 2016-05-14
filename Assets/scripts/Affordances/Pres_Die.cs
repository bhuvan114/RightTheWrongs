using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class Pres_Die : Affordance {

	public Pres_Die(SmartDoctor afdnt, SmartPresident afdee){

		affodant = afdnt;
		affordee = afdee;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		name = "President passed away";

		preconditions.Add(new Condition(affordeeName, "Istreated", true));
		preconditions.Add(new Condition(affordeeName, "InScene", true));

		effects.Add (new Condition (affordeeName, "IsDead", true));

		treeRoot = this.execute ();
	}

	//Behaviour Tree here
	public Node execute() {

		return new Sequence (new LeafWait(100));
	}
}
