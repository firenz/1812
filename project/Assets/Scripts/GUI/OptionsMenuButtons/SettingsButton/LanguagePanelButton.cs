using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LanguagePanelButton : UIGenericButton {

	public delegate void ShowLanguageMenu();
	public static event ShowLanguageMenu showLanguageMenuOnClick;
	
	protected override void ActionOnClick (){
		showLanguageMenuOnClick();	
	}
}
