using UnityEngine;
using System.Collections;
using TreeSharpPlus;

using POPL.Planner;

public class EndSpeech : Affordance {

	public EndSpeech(SmartPresident afdnt, SmartStage afdee) {

		affodant = afdnt;
		affordee = afdee;
		initialize ();
	}

	void initialize() {

		base.initialize ();

		name = "President finshes speech";

		preconditions.Add (new Condition(affordantName, "IsSpeaking", true));

		effects.Add(new Condition(affordantName, "IsSpeaking", false));
		effects.Add(new Condition(affordantName, "SpeechGiven", true));

		treeRoot = this.execute ();

	}

	//Behaviour Tree here
	public Node execute()
	{
		return new Sequence(this.SpeakingAnimation());
	}

	Node SpeakingAnimation()
	{
		return new Sequence (new LeafWait (1500),
			affodant.GetComponent<BehaviorMecanim> ().Node_HandAnimation ("WONDERFUL", false));
		//new LeafInvoke(() => affordee.gameObject.GetComponent<GunController>().SetHolder(affodant.gameObject.transform.Find("Holder").gameObject)),
		//new LeafInvoke(() => affordee.gameObject.GetComponent<GunController>().SetIsHolding(true)));
	}
}
