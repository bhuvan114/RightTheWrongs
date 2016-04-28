using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class DrawGun : Affordance {

	private GameObject handHolder;

	public DrawGun(SmartAssassin afdnt, SmartGun afdee) {
		
		affodant = afdnt;
		affordee = afdee;
		initialize ();
	}
	
	void initialize() {
		
		base.initialize ();

		preconditions.Add (new Condition(affordantName, "HasGun", true));
		preconditions.Add (new Condition(affordantName, "Undetected", true));
		preconditions.Add(new Condition(affordeeName, "IsDrawn", false));

		effects.Add(new Condition(affordeeName, "IsDrawn", true));

		treeRoot = this.execute ();
		
	}

    //Behaviour Tree here
    public Node execute()
    {
        return new Sequence(this.DrawGunAnimation());
    }

    Node DrawGunAnimation()
    {
        return new Sequence (
			new LeafInvoke (() => affordee.gameObject.GetComponent<GunControllerScript>().SetIsHolding(true)),
			affodant.GetComponent<BehaviorMecanim> ().Node_HandAnimation ("PISTOLAIM", true));//, new LeafWait (500));
            //new LeafInvoke(() => affordee.gameObject.GetComponent<GunController>().SetHolder(affodant.gameObject.transform.Find("Holder").gameObject)),
            //new LeafInvoke(() => affordee.gameObject.GetComponent<GunController>().SetIsHolding(true)));
    }
}