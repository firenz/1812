using UnityEngine;
using System.Collections;

public class YesButton : UIGenericButton {

	public delegate void YesClicked();
	public static event YesClicked yesClicked;

	protected override void OnEnable(){
		LocatedTextManager.currentLanguageChanged += ChangeButtonTextLanguage;
	}
	
	protected override void OnDisable(){
		LocatedTextManager.currentLanguageChanged -= ChangeButtonTextLanguage;
	}

	protected override void ActionOnClick (){
		yesClicked();
	}
}
