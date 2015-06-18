using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReturnToMainMenuButton : UIGenericButton {
	private Button returnMainMenuBtn;
	private Text returnMainMenuBtnText;
	private Color defaultColorText;
	
	public delegate void DisableAllButtonsAndMenus();
	public static event DisableAllButtonsAndMenus disableAll;
	
	private void OnEnable(){
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
	}
	
	private void OnDisable(){
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
	}
	
	private void Start(){
		returnMainMenuBtn = this.GetComponent<Button>();
		returnMainMenuBtnText = this.transform.FindChild("Text").GetComponent<Text>();

		defaultColorText = returnMainMenuBtnText.color;
		returnMainMenuBtnText.color = Color.clear;
		returnMainMenuBtnText.text = LocalizedTextManager.GetLocalizedText("MAIN_MENU", "RETURN_TO", "NAME")[0];
		DisableButton();
	}
	
	protected override void Update(){
		if(isMouseOver){
			if(returnMainMenuBtn.interactable){
				CustomCursorController.Instance.ChangeCursorOverUIButton();
			}
		}
	}
	
	public override void OnClick(){
		base.OnClick();
		disableAll();	
		DisableButton();
	}
	
	private void ChangeLanguage(){
		returnMainMenuBtnText.text = LocalizedTextManager.GetLocalizedText("MAIN_MENU", "RETURN_TO", "NAME")[0];
	}


	public void EnableButton(){
		this.transform.SetAsLastSibling();
		returnMainMenuBtn.interactable = true;
		returnMainMenuBtnText.color = defaultColorText;
	}
	
	public void DisableButton(){
		returnMainMenuBtn.interactable = false;
		returnMainMenuBtnText.color = Color.clear;
	}
}

