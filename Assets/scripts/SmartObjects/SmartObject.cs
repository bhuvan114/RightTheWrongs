using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TreeSharpPlus;

namespace POPL.Planner
{
	public class SmartObject : MonoBehaviour {

		public string name;

		public List<System.Type> getAllAffordances() {

			List<System.Type> affordances = new List<System.Type> ();
			System.Reflection.FieldInfo[] members = this.GetType ().GetFields ();
			foreach (System.Reflection.FieldInfo member in members) {
				if (member.FieldType.BaseType == typeof(POPL.Planner.Affordance)) {
					affordances.Add(member.FieldType);
				}
			}
			return affordances;
		}

		public SmartObject(string objName) {

			name = objName;
		}

		public SmartObject(){

			name = "Dummy";
		}

		public bool Equals(SmartObject obj) {

			return name.Equals (obj.name);
		}

		public Node SetPlannerControl_BT(bool status){

			return new LeafInvoke (
				() => this.SetPlannerControl_BT (status));
		}
		
		public void SetPlannerControl(bool status){

			if (this.gameObject.GetComponent<AudienceBehavior> ())
				this.gameObject.GetComponent<AudienceBehavior> ().SetPlannerControl (status);
		}
	}
}