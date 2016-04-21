using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class Hit : Affordance {

	public Hit(SmartCharacter afdnt, SmartObject afdee){
		
		affodant = afdnt;
		affordee = afdee;
		initialize ();
	}

	void initialize() {
		
		base.initialize ();

		preconditions.Add (new Condition(affordantName, affordeeName, "InFront", true));
		effects.Add(new Condition(affordeeName, affordantName, "HitBy", true));
		

	}

	//Behaviour Tree here
	public Node execute()
	{
		Debug.Log("Hit execute");
		return new Sequence(this.HitAnimation());
	}

	Node HitAnimation()
	{
		Debug.Log("Hit animation");
		return new Sequence(
			affodant.GetComponent<BehaviorMecanim>().Node_HandAnimation("BASH", true), 
			affordee.GetComponent<BehaviorMecanim>().Node_BodyAnimation("DYING", true),
			new LeafWait(500)
		);
	}
}
