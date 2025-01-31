﻿using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TreeSharpPlus;

using POPL.Planner;
//namespace POPL.Planner {

public static class NSM {

	/* Functions

		1. InitiateNarrativeStateManager - Initiates current state, assigns that as start state to the planner
		2. SetGoalState - Sets the goal state for the planner
		3. UpdateAffordanceStatus - If affordance is executed, move affordance from actions to executedActions list
		4. UpdateCausalLinksStatus - If act0 is executed, move to runningCLs list; if act1 is executed, move to executedCLs list
		5. CheckInconsistencies -
			UnderConsistent - If a causal link from runningCLs is negated, act1 is UnderConsistent
			OverConsistent - If a causal link from causalLinks is already executed, ac1 is OverConsistent
		6. PropogateConsistency - For an over consistent state, propagate consestency upward in the causal link heirarchy
		7. AddToAgenda - Add a <condition, affordance> to the planner
		8. ConstructBehaviourTree
		9. UpdatePlanSpace - calls UpdateNarrativeState, UpdateAffordanceStatus, UpdateCausalLinksStatus
		10.UpdateNarrativeState - Updates the current state of the narrative
		11.AddConditionToNarrativeState - Adds a condition to narrative state
	*/

	/*
	private static List<Condition> conditions_NoPlayer =  new List<Condition>();
	private static Dictionary<Affordance, List<Affordance>> constraints = new Dictionary<Affordance, List<Affordance>> ();
	private static Dictionary<Affordance, int> affOrder = new Dictionary<Affordance, int> ();

	public static bool terminated = false;
	*/

	public static string debugLog = "Yolo!!";

	public static string playerControlledCharacter = "";
	public static List<string> plannerControlledObjects = new List<string>();
	public static string planTimes = "";
	public static string planLengths = "";
	public static string popTimes = "";
	public static string popLengths = "";
	public static string planStatus = "";
	public static int noOfPossibleActions = 0;
	private static string journalMessage;
	static Affordance start, goal;
	public static bool hasPlan = false;
	public static bool isJournalUpdated = false;
	public static bool first = true;
	public static bool isPlanning = false;//isConsistent, isReplanRequired;
	private static DynamicPlanner planner = new DynamicPlanner();
	private static Planner pop = new Planner ();

	private static List<Condition> currentState =  new List<Condition>();
	public static Dictionary<Affordance, List<Affordance>> constraints = new Dictionary<Affordance, List<Affordance>> ();

	public static List<Affordance> affordances = new List<Affordance>();
	//private static List<Affordance> executedActions = new List<Affordance>();

	public static List<CausalLink> causalLinks = new List<CausalLink>();
	private static List<CausalLink> runningCLs = new List<CausalLink>();
	//private static List<CausalLink> executedCLs = new List<CausalLink>();

	public static Node root = null;


	public static bool IsConditionInCurrentState(Condition cond){

		if (currentState.Contains (cond))
			return true;

		return false;
	}

	static void SetStartState() {

		start = new Affordance();
		start.setStart ();
		foreach(Condition cond in currentState)
			start.addEffects(cond);
	}

	static void ResetStartState() {

		start.removeEffects ();
		foreach(Condition cond in currentState)
			start.addEffects(cond);
	}

	static void AddConditionToNarrativeState(Condition cond) {

		for(int ind = 0; ind < currentState.Count; ind++) {
			if(currentState[ind].isNegation(cond)) {
				Debug.Log ("Negated!!");
				currentState[ind] = cond;
				return;
			} else if (currentState[ind].Equals(cond)) {
				return;
			}
		}
		currentState.Add (cond);
	}

	public static void SetPlannerControledObjects(){

		plannerControlledObjects = new List<string>();
		foreach (Affordance aff in affordances) {

			if (!plannerControlledObjects.Contains (aff.affodant.name))
				plannerControlledObjects.Add (aff.affodant.name);

			if (!plannerControlledObjects.Contains (aff.affordee.name))
				plannerControlledObjects.Add (aff.affordee.name);
		}
	}

	public static void InitiateNarrativeStateManager() {

		debugLog = "Initiate NSM!!";
		//AddConditionToNarrativeState(new Condition("Assasin", "InScene", true));

		AddConditionToNarrativeState(new Condition("President", "InScene", true));
		AddConditionToNarrativeState(new Condition("Agent", "InScene", true));
		AddConditionToNarrativeState(new Condition("Assassin", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud1", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud2", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud3", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud4", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud5", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud6", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud7", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud8", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud9", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud10", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud11", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud12", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud13", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud14", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud15", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud16", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud17", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud18", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud19", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud20", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud21", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud22", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud23", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud24", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud25", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud26", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud27", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud28", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud29", "InScene", true));
		AddConditionToNarrativeState(new Condition("Aud30", "InScene", true));

		AddConditionToNarrativeState(new Condition("Assassin", "HasGun", true));
		AddConditionToNarrativeState(new Condition("Assassin", "Undetected", false));
		AddConditionToNarrativeState(new Condition("Gun", "IsDrawn", false));
		AddConditionToNarrativeState(new Condition ("Stage", "IsSecure", false));

		SetStartState ();

		goal = new Affordance ();
		goal.setGoal ();
		//goal.addPrecondition (new Condition("Assasin", "Gun", "IsDrawn", true));
		//goal.addPrecondition (new Condition("President", "NearMic", true));

		//goal.addPrecondition (new Condition("Assassin", "ExpoAim", true));
		goal.addPrecondition (new Condition("Assassin", "StageAttempt", true));
		//goal.addPrecondition (new Condition("President", "PresAtExpo", true));
		//goal.addPrecondition (new Condition("President", "IsShot", true));
		//goal.addPrecondition (new Condition("Doctor", "NearPresident", true));
		goal.addPrecondition (new Condition("President", "IsDead", true));
		isPlanning = true;
		hasPlan = false;
		//List<POPL.Planner.Tuple<Condition, Affordance>> inconsistencies = new List<POPL.Planner.Tuple<Condition, Affordance>> ();
		//foreach (Condition cond in goal.getPreconditions())
		//	inconsistencies.Add (new POPL.Planner.Tuple<Condition, Affordance> (cond, goal));


		System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch ();
		watch.Start ();
		pop.computePlan();
		watch.Stop ();

		planner = new DynamicPlanner ();
		debugLog = "new planner!!";

		//Recording plan times
		System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch ();
		swatch.Start ();
		if (planner.ComputePlan ()) {
			swatch.Stop ();
			SetPlannerControledObjects ();
			//Debug.LogError ("Goal Found");
			ConstructBehaviourTree ();
			hasPlan = true;
		} else {
			swatch.Stop ();
		}
		Debug.LogError ("Aniki iPlan : " + watch.Elapsed.TotalMilliseconds.ToString () + "-" + swatch.Elapsed.TotalMilliseconds.ToString () + "-" + pop.getNoOfActions ().ToString () + "-" + affordances.Count ().ToString ());
		planTimes = planTimes + "," + swatch.Elapsed.TotalMilliseconds.ToString ();
		isPlanning = false;
	}

	static void UpdateCausalLinks(Affordance action) {

		// if action is act1, set running; if action is act2 delete causal link
		for(int i = 0; i < causalLinks.Count; i++) {
			if (causalLinks[i].act1.Equals (action)) {
				causalLinks [i].disp ();
				runningCLs.Add (causalLinks [i]);
				causalLinks.RemoveAt (i);
				i--;
			} 
		}

		for(int i = 0; i < runningCLs.Count; i++) {
			if (runningCLs[i].act2.Equals (action)) {

				runningCLs.RemoveAt (i);
				i--;
			}
		}
	}

	static void RemoveAffordanceAndConstraints(Affordance act) {

		if (affordances.Contains (act))
			affordances.Remove (act);

		if (constraints.ContainsKey (act))
			constraints.Remove (act);

		foreach (Affordance key in constraints.Keys) {
			if (constraints [key].Contains (act))
				constraints [key].Remove (act);
		}
	}

	static void PropogateConsistency(List<Affordance> affs) {

		List<Affordance> overConsistentActions = new List<Affordance> ();
		foreach (Affordance act in affs) {
			//List<CausalLink> ocRunningCLs = HelperFunctions.GetCLsByAction1 (runningCLs, act);
			if (HelperFunctions.GetCLsByAction1 (causalLinks, act).Count () == 0) {

				RemoveAffordanceAndConstraints (act);
				List<CausalLink> ocRunningCLs = HelperFunctions.GetCLsByAction2 (runningCLs, act);
				foreach (CausalLink cl in ocRunningCLs)
					runningCLs.Remove (cl);

				List<CausalLink> ocCLs = HelperFunctions.GetCLsByAction2 (causalLinks, act);
				foreach (CausalLink cl in ocCLs) {

					causalLinks.Remove (cl);
					overConsistentActions.Add (cl.act1);
				}

			}
		}

		if (overConsistentActions.Count () > 0)
			PropogateConsistency (overConsistentActions);
	}

	static bool IsActionInconsistent (Affordance action, out List<POPL.Planner.Tuple<Condition, Affordance>> inconsistencies) {

		bool isInconsistent = false;
		inconsistencies = new List<POPL.Planner.Tuple<Condition, Affordance>> ();
		List<Affordance> overConsistentActions = new List<Affordance> ();
		foreach (Condition effect in action.getEffects()) {
			for (int i = 0; i < causalLinks.Count (); i++) {
				if (causalLinks [i].p.Equals (effect)) {
					inconsistencies.Add (new POPL.Planner.Tuple<Condition, Affordance> (causalLinks [i].p, causalLinks [i].act2));
					overConsistentActions.Add (causalLinks [i].act1);
					causalLinks.RemoveAt (i);
					i--;
					isInconsistent = true;
				}
			}
		}

		if (overConsistentActions.Count() > 0)
			PropogateConsistency (overConsistentActions);

		foreach (Condition effect in action.getEffects()) {
			foreach (CausalLink cl in runningCLs) {
				if (cl.p.isNegation (effect)) {
					//cl.disp ();
					isInconsistent = true;
				}
			}
		}
		Debug.LogWarning ("UpdateNarrativeStateForUserAction - IsInCon - " + isInconsistent.ToString());
		return isInconsistent;
	}

	public static void UpdateNarrativeStateForUserAction(Affordance action) {

		Time.timeScale = 0f;
		//Node tempRoot = root;
		Debug.LogError ("UpdateNarrativeStateForUserAction!!");
		//Add effects to Narrative State
		foreach (Condition effect in action.getEffects())
			AddConditionToNarrativeState (effect);

		List<POPL.Planner.Tuple<Condition, Affordance>> inconsistencies = new List<POPL.Planner.Tuple<Condition, Affordance>>();
		if (IsActionInconsistent (action, out inconsistencies)) {
			Debug.LogWarning ("UpdateNarrativeStateForUserAction - Is incon!!");
			hasPlan = false;
			isPlanning = true;
			//inconsistencies = new List<POPL.Planner.Tuple<Condition, Affordance>> ();
			foreach (CausalLink cl in runningCLs)
				inconsistencies.Add (new POPL.Planner.Tuple<Condition, Affordance> (cl.p, cl.act2));

			Debug.LogWarning ("incons!!");
			foreach (POPL.Planner.Tuple<Condition, Affordance> incon in inconsistencies)
				incon.First.disp ();
			disp ();
			//Debug.LogWarning ("currstate");
			//SetStartState();	

			runningCLs = new List<CausalLink> ();
			//SetStartState ();
			ResetStartState ();
			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch ();
			watch.Start ();
			pop.computePlan();
			watch.Stop ();
			//popLengths = popLengths + "," + pop.getNoOfActions ().ToString ();
			//popTimes = popTimes + "," + swatch.Elapsed.TotalMilliseconds.ToString ();
			int success = 0;
			System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch ();
			swatch.Start ();
			if (planner.ComputePlan (inconsistencies)) {
				swatch.Stop ();
				success = 1;
				SetPlannerControledObjects ();
				planTimes = planTimes + "," + swatch.Elapsed.TotalMilliseconds.ToString ();
				planLengths = planLengths + "," + affordances.Count ().ToString ();
				ConstructBehaviourTree ();
				hasPlan = true;
			} else {
				swatch.Stop ();
				planTimes = planTimes + "," + swatch.Elapsed.TotalMilliseconds.ToString ();
				planLengths = planLengths + "," + "0";
			}
			isPlanning = false;
			Debug.LogError ("Aniki replan_"+ success+" : " + watch.Elapsed.TotalMilliseconds.ToString () + "-" + swatch.Elapsed.TotalMilliseconds.ToString () + "-" + pop.getNoOfActions ().ToString () + "-" + affordances.Count ().ToString ());

		} else {
			//root = tempRoot;
		}
		Debug.LogWarning ("UpdateNarrativeStateForUserAction - Done!!");
		Time.timeScale = 1f;
	}

	public static void UpdateNarrativeState(Affordance action) {

		//Add effects to Narrative State
		Debug.LogError("Bleh --------------");
		foreach (Condition effect in action.getEffects()) {
			effect.disp ();
			AddConditionToNarrativeState (effect);
		}

		//if (affordances.Contains (action))
		Debug.LogError ("Removing action");
		action.disp ();
		//Remove affordance from narrative
		affordances.Remove(action);

		foreach (Affordance aff in affordances)
			aff.disp ();
		//Remove Affordance from constraints
		if (constraints.ContainsKey (action))
			constraints.Remove (action);


		//Update Causal Links
		Debug.LogWarning("UpdateCausalLinks");
		UpdateCausalLinks(action);
	}

	// Add node to set start as executed in BT
	public static void ConstructBehaviourTree () {

		foreach (Affordance aff in affordances)
			aff.disp ();

		planner.ShowOrderingConstraints ();
		planner.ShowCausalLinks ();

		Dictionary<Affordance, int> affOrder = new Dictionary<Affordance, int> ();
		int indx = affordances.Count;
		foreach (Affordance act in affordances) {
			if(act.isStart()) {
				affOrder.Add(act, 1);
			} else {
				affOrder.Add(act, indx);
			}
		}



		indx = 1;
		//IEnumerable<Affordance> affs = (from aff in affOrder where aff.Value == indx select aff.Key);
		IEnumerable<Affordance> affs = affOrder.Keys.Where (t => affOrder [t] == indx);
		List<Affordance> afds;
		while(affs.Count() != 0) {
			//Debug.Log(indx);
			afds = new List<Affordance>();
			foreach (Affordance act in affs) {
				if (constraints.ContainsKey(act)) {
					foreach(Affordance child in constraints[act])
						afds.Add(child);
				}
			}

			foreach(Affordance afd in afds)
				affOrder[afd] = indx + 1;

			indx = indx + 1;
			affs = (from aff in affOrder where aff.Value == indx select aff.Key);
			//foreach (Affordance af in affs)
			//	af.disp ();
		}

		foreach(Affordance act in affOrder.Keys) {

			act.disp();
			Debug.LogWarning(affOrder[act]);
		}

		List<Node> affSTs = new List<Node>();
		affSTs.Add (start.UpdateState());
		journalMessage = "- Day started\n";
		//Debug.Log ("Indx = " + indx);
		for (int i=1; i<indx; i++) {
			//Debug.Log ("i = " + i);
			//affs = (from aff in affOrder where aff.Value == indx select aff.Key);
			affs = affOrder.Keys.Where (t => affOrder [t] == i);
			foreach(Affordance act in affs) {
				if(!act.isGoal() && !act.isStart()) {

					//act.disp();
					affSTs.Add(new Sequence(act.GetSubTree(), act.UpdateState ()));

					journalMessage = journalMessage + "- " + act.name + "\n";
				}
			}
		}
		root = new Sequence(affSTs.ToArray());
		isJournalUpdated = true;
		Debug.Log (journalMessage);
		//UpdateJournal (journalMessage);
	}

	public static Affordance GetStartState(){
		/*
		Affordance aff = new Affordance();
		aff.setStart ();
		foreach(Condition cond in currentState)
			aff.addEffects(cond);
		return aff;*/
		return start;
	}

	public static Affordance GetGoalState(){

		return goal;
	}

	public static void UpdateJournal() {

		if (!GameObject.Find ("JournalText"))
			Debug.LogError ("No COMP");

		Text journal = GameObject.Find ("JournalText").GetComponent<Text>();
		journal.text = journalMessage;
		isJournalUpdated = false;
		first = false;
	}

	public static void disp () {

		Debug.LogWarning ("Affordances - ");
		foreach (Affordance aff in affordances)
			aff.disp ();
		Debug.LogWarning ("Current state - ");
		foreach (Condition con in currentState)
			con.disp ();
	}
}
//}
