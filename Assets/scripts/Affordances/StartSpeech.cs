using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class StartSpeech : Affordance {

	public StartSpeech(SmartPresident afdnt, SmartStage afdee) {

		affodant = afdnt;
		affordee = afdee;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		name = affordantName + " starts speech";

		preconditions.Add (new Condition(affordantName, "NearMic", true));

		effects.Add(new Condition(affordantName, "IsSpeaking", true));

		treeRoot = this.execute ();

	}

	//Behaviour Tree here
	public Node execute()
	{
		return new Sequence(this.SpeakingAnimation());
	}

	Node SpeakingAnimation()
	{
		return new Sequence (
			affodant.GetComponent<BehaviorMecanim> ().Node_HandAnimation ("WONDERFUL", true), new LeafWait (500));
		//new LeafInvoke(() => affordee.gameObject.GetComponent<GunController>().SetHolder(affodant.gameObject.transform.Find("Holder").gameObject)),
		//new LeafInvoke(() => affordee.gameObject.GetComponent<GunController>().SetIsHolding(true)));
	}
}
