using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RelocateTextPosition : MonoBehaviour {
	public const float rightScreenMargin = 20f;
	public const float leftScreenMargin = 20f;
	
	private Transform parentTransform;
	private Transform thisTransform;
	private Transform dialogTextTransform;
	private Text thisText;
	private RectTransform thisRectTransform;
	private RectTransform parentRectTransform;
	private float baseScreenWidth;
	private float width;

	// Use this for initialization
	private void Start () {
		thisText = this.GetComponent<Text>();
		thisRectTransform = this.GetComponent<RectTransform>();
		thisTransform = this.transform;
		parentTransform = thisTransform.parent;
		parentRectTransform = parentTransform.GetComponent<RectTransform>();
		dialogTextTransform = parentTransform.parent.parent.FindChild("dialogText");
		width = thisRectTransform.rect.width + 20;
		baseScreenWidth = parentTransform.parent.GetComponent<CanvasScaler>().referenceResolution.x;
	}

	/*
	// Update is called once per frame
	private void Update () {
		if(thisText.text != ""){
			Relocate();
		}
	}
	*/

	private void Update(){
		Relocate();
	}

	public void Relocate(){
		parentRectTransform.transform.position = dialogTextTransform.position;
		//thisText.text = displayingText;

		width = thisRectTransform.rect.width + 20;
		float _positionX = parentRectTransform.anchoredPosition.x;
		float _textBeginPositionValue = _positionX - (width * 0.5f);
		float _textEndPositionValue = _positionX + (width * 0.5f);

		if(( _textEndPositionValue) > baseScreenWidth){
			Debug.Log(_textEndPositionValue + " > 640 se sale de la pantalla");
			float _newPositionX = baseScreenWidth - (width * 0.5f) - rightScreenMargin;
			parentRectTransform.anchoredPosition = new Vector2(_newPositionX , parentRectTransform.anchoredPosition.y);
		}
		else if(_textBeginPositionValue < 0){
			float _newPositionX = leftScreenMargin + (width * 0.5f);
			parentRectTransform.anchoredPosition = new Vector2(_newPositionX , parentRectTransform.anchoredPosition.y);
		}
		else{
			//Debug.Log("position + width: " + _textEndPositionValue);
		}
	}

	/*
	public void Relocate(){
		parentRectTransform.transform.position = dialogTextTransform.position;

		width = thisRectTransform.rect.width + 20;
		float _positionX = parentRectTransform.anchoredPosition.x;
		float _textBeginPositionValue = _positionX - (width * 0.5f);
		float _textEndPositionValue = _positionX + (width * 0.5f);

		//Debug.Log("width: " + width);
		//Debug.Log("position X: " + _positionX);
		//Debug.Log("text end position value: " + _textEndPositionValue);

		if(( _textEndPositionValue) > baseScreenWidth){
			Debug.Log(_textEndPositionValue + " > 640 se sale de la pantalla");
			float _newPositionX = baseScreenWidth - (width * 0.5f) - rightScreenMargin;
			parentRectTransform.anchoredPosition = new Vector2(_newPositionX , parentRectTransform.anchoredPosition.y);
		}
		else if(_textBeginPositionValue < 0){
			float _newPositionX = leftScreenMargin + (width * 0.5f);
			parentRectTransform.anchoredPosition = new Vector2(_newPositionX , parentRectTransform.anchoredPosition.y);
		}
		else{
			//Debug.Log("position + width: " + _textEndPositionValue);
		}
	}
	*/
}
