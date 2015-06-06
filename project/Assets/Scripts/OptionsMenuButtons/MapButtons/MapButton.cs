using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapButton : OptionsMenuButton {
	private GameObject mapsMenu;

	public delegate void DisableLocalizationMarks();
	public static event DisableLocalizationMarks disableLocalizationMarks;

	protected override void Start (){
		buttonID = "MAPS";
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
		buttonName = LocalizedTextManager.GetLocalizedText(groupID, buttonID, stringID)[0];
		buttonText.text = buttonName;
		mapsMenu = this.transform.parent.parent.FindChild("MapsMenu").gameObject;
		mapsMenu.transform.SetAsFirstSibling();
	}

	public override void OnClick (){
		MapsMenuOnClick();
	}
	
	public void MapsMenuOnClick(){
		EnableMenus();
	}

	public override void EnableMenus(){
		base.EnableMenus();
		MenuActivated();
		mapsMenu.transform.SetAsLastSibling();
	}
	
	public override void DisableMenus(){
		base.DisableMenus();
		mapsMenu.transform.SetAsFirstSibling();
		mapsMenu.transform.FindChild("MapsPanel/ScrollView/MapsBackground").GetComponent<RectTransform>().transform.localPosition = new Vector3( 0f, 0f, 0f);
		disableLocalizationMarks();
	}
}
