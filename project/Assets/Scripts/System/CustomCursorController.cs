using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CustomCursorController : PersistentSingleton<CustomCursorController> {
	public bool isCursorHidden { get; private set;}

	[SerializeField]
	private string pathCursorSprites = "Graphics/GUI/Cursor/";

	private Transform cursorTransform;
	private SpriteRenderer cursorRenderer;
	private Sprite currentCursorSprite;
	private Sprite menuCursorSprite;
	private Sprite ingameCursorSprite;
	private Sprite overInteractiveElementCursorSprite;
	private Sprite overUIElementCursorSprite;
	private Sprite exitSceneCursorSprite;

	protected override void InitializeOnAwake (){
		isCursorHidden = false;
		Cursor.visible = isCursorHidden;
		cursorTransform = this.transform.FindChild("Cursor");
		cursorRenderer = cursorTransform.GetComponent<SpriteRenderer>();
		menuCursorSprite = Resources.Load<Sprite>(pathCursorSprites + "MenuDefaultCursor");
		ingameCursorSprite = Resources.Load<Sprite>( pathCursorSprites + "IngameDefaultCursor");
		overInteractiveElementCursorSprite = Resources.Load<Sprite>( pathCursorSprites + "OverInteractiveElementCursor");
		overUIElementCursorSprite = Resources.Load<Sprite>(pathCursorSprites + "OverUIElementCursor");
		exitSceneCursorSprite = Resources.Load<Sprite>(pathCursorSprites + "ExitSceneCursor");

		currentCursorSprite = menuCursorSprite;
		ChangeCursorToDefault();
	}

	private void Update(){
		Vector2 _mouseWorldPosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
		cursorTransform.position = _mouseWorldPosition; 
	}

	public void ChangeCursorToDefault(){
		cursorRenderer.sprite = currentCursorSprite;
		transform.eulerAngles = Vector3.zero;
	}
	
	public void ChangeCursorSprite(Sprite newSprite){
		cursorRenderer.sprite = newSprite;
	}
	
	public void ChangeCursorOverInteractiveElement(){
		cursorRenderer.sprite = overInteractiveElementCursorSprite;
	}
	
	public void ChangeCursorIngameScene(){
		cursorRenderer.sprite = ingameCursorSprite;
	}
	
	public void ChangeCursorOverWarpElement(float rotation = 0f){
		cursorRenderer.sprite = exitSceneCursorSprite;
		this.transform.eulerAngles = new Vector3(0f, 0f, rotation);
	}

	public void ChangeCursorOverUIElement(){
		cursorRenderer.sprite = overUIElementCursorSprite;
	}
	
	public void ChangeMenuCursorToIngame(){
		cursorRenderer.sprite = ingameCursorSprite;
		currentCursorSprite =  ingameCursorSprite;
	}
	
	public void ChangeIngameCursorToMenu(){
		cursorRenderer.sprite = menuCursorSprite;
		currentCursorSprite = menuCursorSprite;
	}

	public void HideCursor(){
		isCursorHidden = true;
		cursorRenderer.color = Color.clear;
	}

	public void UnhideCursor(){
		isCursorHidden = false;
		cursorRenderer.color = Color.white;
	}

}
