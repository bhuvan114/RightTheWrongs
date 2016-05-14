using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class UseXRay : Affordance {

	public UseXRay(SmartCharacter afdnt, SmartPresident afdee){

		affodant = afdnt;
		affordee = afdee;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		//name = "Doctor treats President but couldn't find the bullet";

		//preconditions.Add(new Condition(affordantName, "NearPresident", true));

		//effects.Add (new Condition (affordeeName, "Istreated", true));
		effects.Add (new Condition (affordeeName, "InScene", false));

		treeRoot = this.execute ();
	}

	//Behaviour Tree here
	public Node execute() {

		return new Sequence (new LeafWait(10));
	}
}
