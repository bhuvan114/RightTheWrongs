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
		if (isPlayercontrolled && behaviorAgent!= null) {
			//if (behaviorAgent.CurrentEvent != null)
			//	Debug.Log (behaviorAgent.CurrentEvent.Name);
			//else {
			//	Debug.Log ("null event");
			//}
			/*root = dummy_tree ();*/
			/*behaviorAgent = new BehaviorAgent (character.GetComponent<BehaviorMecanim> ().Node_NavStop ());
			BehaviorManager.Instance.Register (behaviorAgent);
			behaviorAgent.StartBehavior ();*/
			behaviorAgent.StopBehavior ();
		}
	}

	// Use this for initialization
	void Start () {
		character = this.gameObject;
		isPlayercontrolled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.E)) {
			if (isPlayercontrolled && root != null) {
				Debug.Log (behaviorAgent.Status.ToString ());
				//root = default_go_look ();
				//behaviorAgent = new BehaviorAgent (root);
				/*root = default_go_look ();
				behaviorAgent = new BehaviorAgent (root);
				BehaviorManager.Instance.Register (behaviorAgent);*/
				behaviorAgent.StartBehavior ();
			}
			isPlayercontrolled = false;
		}

		//Debug.Log ("1");
		if (!isPlayercontrolled) {
			//Debug.Log ("2");
			//Default Audience Behavior
			//if (behaviorAgent == null || behaviorAgent.Status != BehaviorStatus.Running) {
			if (root == null) {
				//Debug.Log ("3");
				root = default_go_look ();
				behaviorAgent = new BehaviorAgent (root);
				//Debug.Log (behaviorAgent.ToString ());
				BehaviorManager.Instance.Register (behaviorAgent);
				behaviorAgent.StartBehavior ();
				//Debug.Log (BehaviorManager.Instance.ToString ());
			} //else {
			//Debug.Log (behaviorAgent.Status.ToString ());
			//else if (!root.IsRunning) {
			//	root = null;
			//}

			//if (behaviorAgent != null)
			//	Debug.Log (behaviorAgent.Status.ToString ());
		} else {
			//if(behaviorAgent != null)
				//Debug.Log (behaviorAgent.Status.ToString());
		}
	}

	protected Node default_go()
	{
		Val<Vector3> target = Val.V (() => position.position);
		return new Sequence (character.GetComponent<BehaviorMecanim> ().Node_GoTo (target));//, this.dummy_tree());//, new LeafWait (1000));
	}

	protected Node default_look()
	{
		Val<Vector3> target = Val.V (() => look.position);
		return new Sequence (//character.GetComponent<BehaviorMecanim>().ST_TurnToFace(target),
			character.GetComponent<BehaviorMecanim>().Node_OrientTowards(target), new LeafWait (1000));/* dummy_tree1(),*/
								
	}

	protected Node default_go_look()
	{
		return new Sequence (this.default_go(), this.default_look());
	}

	protected Node dummy_tree(){

		return new LeafInvoke (
			() => this.dummy_func ());
	}

	public void dummy_func(){
		Debug.Log ("Go executed");
	}

	protected Node dummy_tree1(){

		return new LeafInvoke (
			() => this.dummy_func1 ());
	}

	public void dummy_func1(){
		Debug.Log ("look executed");
	}
}
