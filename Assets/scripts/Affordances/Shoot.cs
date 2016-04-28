using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class Shoot : Affordance{

	public Shoot(SmartAssassin afdnt, SmartCharacter afdee) {

		affodant = afdnt;
		affordee = afdee;
		initialize ();
	}

	public Shoot(SmartAssassin afdnt, SmartAgent afdee) {

		affodant = afdnt;
		affordee = afdee;
		initialize ();
	}

	public Shoot(SmartAssassin afdnt, SmartPresident afdee) {

		affodant = afdnt;
		affordee = afdee;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		preconditions.Add (new Condition("Gun", "IsDrawn", true));
		//preconditions.Add (new Condition(affordeeName, "HasAmmo", true));

		effects.Add(new Condition(affordeeName, "IsShot", true));
		//effects.Add(new Condition(affordantName, affordeeName, "IsDrawn", true));
		treeRoot = this.execute ();

	}

	//Behaviour Tree here
	public Node execute()
	{
		Debug.Log("ShootGun execute");
		return new Sequence(this.ShootGunAnimation());
	}

	Node ShootGunAnimation()
	{
		Debug.Log("ShootGunAnimation");
		return new Sequence(
			affodant.GetComponent<BehaviorMecanim>().Node_OrientTowards(affordee.transform.position), new LeafWait(500),
			affodant.GetComponent<BehaviorMecanim>().Node_HandAnimation("PISTOLFIRE", true), new LeafWait(500),
			affodant.GetComponent<BehaviorMecanim>().Node_HandAnimation("PISTOLFIRE", false), new LeafWait(500),
			affordee.GetComponent<BehaviorMecanim>().Node_BodyAnimation("DYING", true), new LeafWait(1500)
		);
	}
}
