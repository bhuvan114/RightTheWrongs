using UnityEngine;
using System.Collections;

public class DoorInteractMenu : MonoBehaviour {

	DoorTrigger doorTrigger;

	void Update () {


	}

	public void OpenCloseDoor(){

		//doorTrigger.OpenCloseDoor ();
	}

	public void BuyStore()
	{
		//storeTrigger.BuyStore();
	}

	public void setDoor(GameObject obj) {

		doorTrigger = obj.GetComponent<DoorTrigger>();
	}
}
