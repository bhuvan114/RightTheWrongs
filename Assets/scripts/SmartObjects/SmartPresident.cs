using UnityEngine;
using System.Collections;

using POPL.Planner;

public class SmartPresident : SmartObject {

	public Greet greet;
	public Meet meet;
	public Apologize apologize;
	public Argue argue;
	public Pres_GoTo_Stage goToStage;
	public Pres_GoTo_Mic goToMic;
	public Pres_GoToExpo goToExpo;
	public Pres_Leave_Stage leaveStage;
	public StartSpeech startSpeech;
	public EndSpeech endSpeech;
}
