using UnityEngine;
using System.Collections;

public class ReturnToSettingsMenuButton : UIGenericButton {

	public delegate void ReturnToSettingsMenu();
	public static event ReturnToSettingsMenu returnToSettings;

	protected override void ActionOnClick (){
		returnToSettings();
	}
}
