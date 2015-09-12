using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocalizationMarkButton : UIGenericButton {
	[SerializeField]
	private string warpSceneName;

	private string sceneNameLocalized;

	protected override void OnEnable(){
		base.OnEnable();
		ReturnToButton.resetUIPositions += DisableText;
	}

	protected override void OnDisable(){
		base.OnDisable();
		ReturnToButton.resetUIPositions -= DisableText;
	}
	
	protected override void Start() {
		thisButton = this.GetComponent<Button>();
		buttonText = this.transform.parent.FindChild("Text").GetComponent<Text>();
		buttonText.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, localizedTextStringID)[0];
		Color _newColor = buttonText.color;
		_newColor.a = 0f;
		buttonText.color = _newColor;
		buttonText.gameObject.SetActive(false);
	}

	protected override void ActionOnClick (){
		DisableText();
		StartCoroutine(WaitForNewLocalization());
	}

	private IEnumerator WaitForNewLocalization(){
		ModalWindowHandler.Instance.Initialize("WARP_LEVEL");
		
		do{
			yield return null;
		}while(!ModalWindowHandler.Instance.isAnswerSelected);

		if(ModalWindowHandler.Instance.isYesClicked){
			ModalWindowHandler.Instance.Disable();
			GameController.WarpToLevel(warpSceneName, true);
		}
		ModalWindowHandler.Instance.Disable();
	}

	public override void OnMouseOver (){
		if(thisButton.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorOverUIElement();
			if(!buttonText.gameObject.activeSelf){
				buttonText.gameObject.SetActive(true);
			}
			EnableText();
		}
	}

	public override void OnMouseExit (){
		if(thisButton.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorToDefault();
			DisableText();
		}
	}

	private void EnableText(){
		StartCoroutine(FadeAlphaColor(1f, 0.5f));
	}
	
	private void DisableText(){
		StartCoroutine(FadeAlphaColor(0f, 0.5f));
	}
	
	private IEnumerator FadeAlphaColor(float alpha, float time){
		float _alphaText = buttonText.color.a;
		
		for(float t = 0f; t < 1f; t += (Time.deltaTime / time)){
			Color _newColor = buttonText.color;
			_newColor.a = Mathf.Lerp(_alphaText, alpha, t);
			buttonText.color = _newColor;
			yield return null;
		}

		if(buttonText.color.a == 0f){
			buttonText.gameObject.SetActive(false);
		}
	}
}
