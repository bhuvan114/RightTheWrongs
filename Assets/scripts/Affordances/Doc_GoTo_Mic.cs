using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class Doc_GoTo_Mic : Affordance {

	Vector3 pos;

	public Doc_GoTo_Mic(SmartDoctor afdnt, SmartStage afdee){

		affodant = afdnt;
		affordee = afdee;
		pos = afdee.micPos.position;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		name = "Doctor goes to president";

		preconditions.Add(new Condition("President", "NearMic", true));
		preconditions.Add (new Condition("President", "IsShot", true));

		effects.Add (new Condition (affordantName, "NearPresident", true));


		treeRoot = this.execute ();

	}

	//Behaviour Tree here
	public Node execute() {

		return (new Sequence(this.affodant.gameObject.GetComponent<BehaviorMecanim> ().Node_GoTo (pos)));
	}
}
