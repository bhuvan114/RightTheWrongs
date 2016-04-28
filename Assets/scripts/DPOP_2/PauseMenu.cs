using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject controls;
	public GameObject plot;

	public void DisplayPlot() {
		Debug.Log ("DisplayPlot");
		plot.SetActive (true);
		controls.SetActive (false);
	}
	

	public void DisplayControls () {
		Debug.Log ("DisplayControls");
		plot.SetActive (false);
		controls.SetActive (true);
	}
}
