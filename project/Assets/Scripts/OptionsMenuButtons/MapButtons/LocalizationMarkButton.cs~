using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocalizationMarkButton : UIGenericButton {
	public string localizationID;
	public string warpToScene;

	private Text localizationText;
	private string localizationName;

	private void OnEnable(){
		ModalWindowHandler.onInitialized += DisableButton;
		ModalWindowHandler.onDisable += EnableButton;
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
		MapButton.disableLocalizationMarks += OnMouseExit;
	}

	private void OnDisable(){
		ModalWindowHandler.onInitialized -= DisableButton;
		ModalWindowHandler.onDisable -= EnableButton;
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
		MapButton.disableLocalizationMarks -= OnMouseExit;
	}

	// Use this for initialization
	private void Start() {
		localizationName = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "MAPS", localizationID)[0];
		localizationText = this.transform.parent.FindChild("Text").GetComponent<Text>();
		localizationText.text = localizationName;
		Color _newColor = localizationText.color;
		_newColor.a = 0f;
		localizationText.color = _newColor;
	}

	protected override void Update(){
		if(isMouseOver){
			if(this.GetComponent<Button>().interactable){
			CustomCursorController.Instance.ChangeCursorOverUIButton();
			localizationText.gameObject.SetActive(true);
			}
		}
	}

	public override void OnMouseOver() {
		base.OnMouseOver();
		StartCoroutine(EnableText());
	}

	public override void OnMouseExit() {
		base.OnMouseExit();
		StartCoroutine(DisableText());
	}

	public override void OnClick(){
		base.OnClick();
		StartCoroutine(WaitForNewLocalization());
	}

	private IEnumerator WaitForNewLocalization(){
		ModalWindowHandler.Instance.Initialize("WARP_LEVEL");

		do{
			yield return null;
		}while(!ModalWindowHandler.Instance.IsSelectionEnded() );
		
		if(ModalWindowHandler.Instance.IsYesClicked()){
			ModalWindowHandler.Instance.Disable();
			GameController.WarpToLevel(warpToScene);
		}
		else{
			ModalWindowHandler.Instance.Disable();
		}
	}

	private void EnableButton(){
		this.GetComponent<Button>().interactable = true;
	}

	private void DisableButton(){
		this.GetComponent<Button>().interactable = false;
	}

	private void ChangeLanguage(){
		localizationName = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "MAPS", localizationID)[0];
	}

	private IEnumerator EnableText(){
		localizationText.gameObject.SetActive(true);
		yield return StartCoroutine(FadeAlphaColor(1f, 0.5f));
	}

	private IEnumerator DisableText(){
		yield return StartCoroutine(FadeAlphaColor(0f, 0.5f));
		localizationText.gameObject.SetActive(false);
	}

	private IEnumerator FadeAlphaColor(float alpha, float time){
		float _alphaText = localizationText.color.a;

		for(float t = 0f; t < 1f; t += (Time.deltaTime / time)){
			Color _newColor = localizationText.color;
			_newColor.a = Mathf.Lerp(_alphaText, alpha, t);
			localizationText.color = _newColor;
			yield return null;
		}
	}

}
