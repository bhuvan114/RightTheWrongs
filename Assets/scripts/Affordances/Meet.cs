using UnityEngine;
using System.Collections;

using POPL.Planner;

public class Meet : Affordance {

	public Meet(SmartCharacter afdnt, SmartCharacter afdee) {
		
		affodant = afdnt;
		affordee = afdee;
		initialize ();
	}
	
	void initialize() {
		
		base.initialize ();
		preconditions.Add (new Condition(affordantName, "InScene", true));
		preconditions.Add (new Condition(affordeeName, "InScene", true));
		effects.Add(new Condition(affordeeName, affordantName, "Infront", true));
		effects.Add(new Condition(affordantName, affordeeName, "Infront", true));
	}
	
	//Behaviour Tree here
	public void execute() {
		Debug.Log("Meet execute");
		return new Sequence(this.HitAnimation());
	}

	Node MeetAnimation()
	{
		Debug.Log("MeetAnimation");
		Val<Vector3> target = Val.V (() => affordee.gameObject.transform.position);
		Val<Vector3> target2 = Val.V (() => affodant.gameObject.transform.position);
		return new Sequence (affodant.GetComponent<BehaviorMecanim> ().Node_GoToUpToRadius (target, 1), 
			affodant.GetComponent<BehaviorMecanim>().ST_TurnToFace(target),
			affordee.GetComponent<BehaviorMecanim>().ST_TurnToFace(target1),
			new LeafWait (1000));
	}
}
