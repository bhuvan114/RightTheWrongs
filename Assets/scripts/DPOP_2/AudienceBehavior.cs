using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class AudienceBehavior : MonoBehaviour {

	public Transform position;
	public Transform look;
	private GameObject character;
	private BehaviorAgent behaviorAgent;
	private Node root = null;

	bool isPlayercontrolled;

	public void SetPlayerControl(bool control) {

		isPlayercontrolled = control;
	}

	// Use this for initialization
	void Start () {
		character = this.gameObject;
		isPlayercontrolled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.E))
			isPlayercontrolled = false;

		if (!isPlayercontrolled) {

			//Default Audience Behavior
			if (root == null) {
				root = default_go_look ();
				behaviorAgent = new BehaviorAgent (root);
				BehaviorManager.Instance.Register (behaviorAgent);
				behaviorAgent.StartBehavior ();
			} else if (!root.IsRunning) {
				root = null;
			}
		}
	}

	protected Node default_go()
	{
		Val<Vector3> target = Val.V (() => position.position);
		return new Sequence (character.GetComponent<BehaviorMecanim> ().Node_GoTo (target), new LeafWait (1000));
	}

	protected Node default_look()
	{
		Val<Vector3> target = Val.V (() => look.position);
		return new Sequence (//character.GetComponent<BehaviorMecanim>().ST_TurnToFace(target),
							 character.GetComponent<BehaviorMecanim>().Node_OrientTowards(target),
								new LeafWait (1000));
	}

	protected Node default_go_look()
	{
		return new Sequence (this.default_go(), this.default_go());
	}
}
