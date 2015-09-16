using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RelocateTextPosition : MonoBehaviour {
	public const float defaultScreenMargin = 20f;

	private Transform panelTransform;
	private Transform dialogTextTransform;
	private Text dialogueText;
	private RectTransform actorCanvasRectTransform;
	private RectTransform panelRectTransform;
	private float halfScreenWidth;
	
	private void Start () {
		halfScreenWidth = Screen.width * 0.5f;
		panelRectTransform = this.GetComponent<RectTransform>();
		panelTransform = this.transform;
		dialogueText = this.GetComponentInChildren<Text>();
		Canvas _actorCanvas = panelTransform.parent.GetComponent<Canvas>();
		actorCanvasRectTransform = _actorCanvas.GetComponent<RectTransform>();
		dialogTextTransform = panelTransform.parent.parent.FindChild("dialogText");
	}

	private void Update(){
		Relocate();
	}

	private void Relocate(){
		if(dialogueText.text != ""){
			float _newScreenMargin = RecalculateScreenMarginSize();

			Vector2 _dialogueTextViewportPosition = Camera.main.WorldToViewportPoint(dialogTextTransform.position);
			Vector2 _dialogueTextScreenPosition = new Vector2(
				((_dialogueTextViewportPosition.x * actorCanvasRectTransform.sizeDelta.x) - (actorCanvasRectTransform.sizeDelta.x * 0.5f)),
				((_dialogueTextViewportPosition.y * actorCanvasRectTransform.sizeDelta.y) - (actorCanvasRectTransform.sizeDelta.y * 0.5f)));
			float _halfCurrentPanelWidth = (panelRectTransform.sizeDelta.x) * 0.5f;

			if((_dialogueTextScreenPosition.x + _halfCurrentPanelWidth) >= (halfScreenWidth - _newScreenMargin)){
				panelRectTransform.anchoredPosition = new Vector2((halfScreenWidth - _halfCurrentPanelWidth) - _newScreenMargin, panelRectTransform.anchoredPosition.y);
			}
			else if((_dialogueTextScreenPosition.x - _halfCurrentPanelWidth) <= ((halfScreenWidth * -1f) + _newScreenMargin)){
				panelRectTransform.anchoredPosition = new Vector2((halfScreenWidth * -1f) + (_halfCurrentPanelWidth + _newScreenMargin), panelRectTransform.anchoredPosition.y);
			}
			else{
				panelTransform.position = dialogTextTransform.position;
			}
		}
		else{
			panelTransform.position = dialogTextTransform.position;
		}
	}

	private float RecalculateScreenMarginSize(){
		if(Screen.width == 800){
			return defaultScreenMargin;
		}
		else{
			float _newMarginSize = (halfScreenWidth * defaultScreenMargin) / 400f;
			return _newMarginSize;
		}
	}
}
