using UnityEngine;
using System.Collections;

public class CustomCursorController : PersistentSingleton<CustomCursorController> {
	private GameObject interactionCursor;
	private Sprite currentCursorSprite;
	private Sprite defaultCursorSprite;
	private Sprite interactiveElementCursorSprite;
	private Sprite overInteractiveElementCursorSprite;
	private Sprite overUIButtonCursorSprite;
	private Sprite exitRoomCursorSprite;
	public bool isOverUIButton = false;

	protected override void InitializeOnAwake (){
		Cursor.visible = false;
		interactionCursor = this.transform.FindChild("Cursor").gameObject;
		defaultCursorSprite = interactionCursor.GetComponent<SpriteRenderer>().sprite;
		currentCursorSprite = defaultCursorSprite;
		interactiveElementCursorSprite = Resources.Load<Sprite>("Graphics/GUI/InteractionCursorv2");
		overInteractiveElementCursorSprite = Resources.Load<Sprite>("Graphics/GUI/InteractionCursorOver");
		overUIButtonCursorSprite = Resources.Load<Sprite>("Graphics/GUI/MenuButtonOverCursor");
		exitRoomCursorSprite = Resources.Load<Sprite>("Graphics/GUI/ExitRoomCursor");
		ChangeCursorToDefault();
	}

	private void Update(){
		Vector2 _mouseWorldPosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
		interactionCursor.transform.position = _mouseWorldPosition; 
	}

	public void ChangeCursorToDefault(){
		interactionCursor.GetComponent<SpriteRenderer>().sprite = currentCursorSprite;
		transform.eulerAngles = Vector3.zero;
	}
	
	public void ChangeCursorTexture(Sprite newSprite){
		interactionCursor.GetComponent<SpriteRenderer>().sprite = newSprite;
	}
	
	public void ChangeCursorOverInteractiveElement(){
		interactionCursor.GetComponent<SpriteRenderer>().sprite = overInteractiveElementCursorSprite;
	}
	
	public void ChangeCursorInteractiveElement(){
		interactionCursor.GetComponent<SpriteRenderer>().sprite = interactiveElementCursorSprite;
	}
	
	public void ChangeCursorOverWarpElement(float rotation = 0f){
		interactionCursor.GetComponent<SpriteRenderer>().sprite = exitRoomCursorSprite;
		this.transform.eulerAngles = new Vector3(0f, 0f, rotation);
	}

	public void ChangeCursorOverUIButton(){
		if(overUIButtonCursorSprite == null){
			Debug.LogError("cursor sprite is null");
		}
		else{
			interactionCursor.GetComponent<SpriteRenderer>().sprite = overUIButtonCursorSprite;
		}
	}
	
	public void ChangeDefaultCursorToInteractive(){
		interactionCursor.GetComponent<SpriteRenderer>().sprite = interactiveElementCursorSprite;
		currentCursorSprite =  interactiveElementCursorSprite;
	}
	
	public void ChangeInteractiveCursorToDefault(){
		interactionCursor.GetComponent<SpriteRenderer>().sprite = defaultCursorSprite;
		currentCursorSprite = defaultCursorSprite;
	}

	public void HideCursor(){
		interactionCursor.GetComponent<SpriteRenderer>().color = Color.clear;
	}

	public void UnhideCursor(){
		interactionCursor.GetComponent<SpriteRenderer>().color = Color.white;
	}

}
