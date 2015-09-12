using UnityEngine;
using System.Collections;

public class NoButton : UIGenericButton {

	public delegate void NoClicked();
	public static event NoClicked noClicked;

	protected override void OnEnable(){
		LocatedTextManager.currentLanguageChanged += ChangeButtonTextLanguage;
	}
	
	protected override void OnDisable(){
		LocatedTextManager.currentLanguageChanged -= ChangeButtonTextLanguage;
	}

	protected override void ActionOnClick (){
		noClicked();
	}
}
