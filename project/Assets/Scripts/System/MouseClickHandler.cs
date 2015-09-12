using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Player))]
public class MouseClickHandler : MonoBehaviour {
	//For controlling how quickly the mouse is clicked
	public float lastTimeMouseWasClicked{ get; private set;}
	private const float delayBetweenMouseClicks = 0.55f;

	//For updating the game gui
	private GameObject lastItemInventoryClicked;
	private string useText;
	private string withText;
	private string lastItemInventoryName= "";
	private string lastInventoryItemDisplayName = "";

	protected Text DisplayNameText;

	private void Start () {
		lastTimeMouseWasClicked = Time.time;
		DisplayNameText = GameObject.Find("NameInteractiveElementText").GetComponent<Text>();
		DisplayNameText.GetComponent<Text>().text = "";
		Inventory.Instance.UpdateAllItems();
		useText = LocatedTextManager.GetLocatedText("GUI", "DEFAULT", "USE_IN_DISPLAYNAME")[0];
		withText = LocatedTextManager.GetLocatedText("GUI", "DEFAULT", "WITH_IN_DISPLAYNAME")[0];
	}

	private void Update(){
		if(!CutScenesManager.IsPlaying() && MultipleChoiceManager.Instance.isSelectionEnded){
			if(CustomCursorController.Instance.isCursorHidden){
				CustomCursorController.Instance.UnhideCursor();
			}

			DisplayNameText.text = "";
			CustomCursorController.Instance.ChangeCursorToDefault();
			GameObject _firstGameObjectHit = FirstObjectOnMouseOver();
			string _tagFirstGO;
			
			try{
				_tagFirstGO = _firstGameObjectHit.tag;
			}
			catch(NullReferenceException){
				_tagFirstGO = "";
			}

			UpdateDisplayingNameAndCursor(_firstGameObjectHit, _tagFirstGO);

			if(Input.GetMouseButtonDown(0)){
				HandleLeftClick(_firstGameObjectHit, _tagFirstGO);
			}
			if(Input.GetMouseButtonDown(1)){
				HandleRightClick(_firstGameObjectHit, _tagFirstGO);
			}
		}
		else if(CutScenesManager.IsPlaying() && MultipleChoiceManager.Instance.isSelectionEnded){
			CustomCursorController.Instance.HideCursor();
		}
	}

	private void UpdateDisplayingNameAndCursor(GameObject firstGameObjectHit, string tagFirstGameObjectHit){
		if(firstGameObjectHit != null){
			switch(tagFirstGameObjectHit){
			case "NavigationCollider":
				if(Player.Instance.isUsingItemInventory){
					DisplayNameText.text = useText + " " + lastInventoryItemDisplayName + " " + withText;
				}
				CustomCursorController.Instance.ChangeCursorToDefault();
				break;
			case "NPC": case "InteractivePoint": case "ItemInventory":
				string _nameInteractiveElement = firstGameObjectHit.GetComponent<InteractiveElement>().displayName;
				
				if(Player.Instance.isUsingItemInventory){
					DisplayNameText.text = useText + " " + lastInventoryItemDisplayName + " " + withText + " " + _nameInteractiveElement;
				}
				else{
					DisplayNameText.text = _nameInteractiveElement;
					firstGameObjectHit.GetComponent<InteractiveElement>().ChangeCursorOnMouseOver();
				}
				break;
				
			case "GUI":
				CustomCursorController.Instance.ChangeCursorOverUIElement();
				break;
			default:
				if(Player.Instance.isUsingItemInventory){
					DisplayNameText.text = useText + " " + lastInventoryItemDisplayName + " " + withText;
				}
				break;
			}
		}
		else{
			if(EventSystem.current.IsPointerOverGameObject()){
				CustomCursorController.Instance.ChangeCursorOverUIElement();
			}
			else{
				if(Player.Instance.isUsingItemInventory){
					DisplayNameText.text = useText + " " + lastInventoryItemDisplayName + " " + withText;
				}
				CustomCursorController.Instance.ChangeCursorToDefault();
			}
		}
	}

	private void HandleLeftClick(GameObject firstGameObjectHit, string tagFirstGameObjectHit){
		if((Time.time - lastTimeMouseWasClicked) > delayBetweenMouseClicks){
			lastTimeMouseWasClicked = Time.time;
			
			if(!Player.Instance.isDoingAction && !Player.Instance.isSpeaking && MultipleChoiceManager.Instance.isSelectionEnded && !CutScenesManager.IsPlaying()){
				lastTimeMouseWasClicked = Time.time;
				Player.Instance.SetWaitingInactive();
				
				if(firstGameObjectHit != null){
					
					switch(tagFirstGameObjectHit){
					case "NavigationCollider":
						if(Player.Instance.isUsingItemInventory){
						}
						else{
							Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
							Player.Instance.GoTo(_mousePosition);
						}
						break;
					case "NPC": case "InteractivePoint":
						InteractiveElement _interactiveElement = firstGameObjectHit.GetComponent<InteractiveElement>();
						if(Player.Instance.isUsingItemInventory){
							
							_interactiveElement.ActionOnItemInventoryUsed(lastItemInventoryClicked);
						}
						else{
							_interactiveElement.LeftClickAction();
						}
						break;
					case "ItemInventory":
						if(Player.Instance.isUsingItemInventory){
							firstGameObjectHit.GetComponent<InteractiveElement>().ActionOnItemInventoryUsed(lastItemInventoryClicked);
						}
						else{
							lastItemInventoryClicked = firstGameObjectHit;
							lastItemInventoryName = firstGameObjectHit.name;
							lastInventoryItemDisplayName = firstGameObjectHit.GetComponent<InteractiveElement>().displayName;
							lastItemInventoryClicked.GetComponent<ItemInventory>().Select();
						}
						break;
					default:
						break;
					}
				}
			}
		}
	}

	private void HandleRightClick(GameObject firstGameObjectHit, string tagFirstGameObjectHit){
		if((Time.time - lastTimeMouseWasClicked) > delayBetweenMouseClicks){
			lastTimeMouseWasClicked = Time.time;
			
			if(!Player.Instance.isDoingAction && MultipleChoiceManager.Instance.isSelectionEnded && !CutScenesManager.IsPlaying()){
				lastTimeMouseWasClicked = Time.time;
				Player.Instance.SetWaitingInactive();
				
				if(firstGameObjectHit != null){
					if(Player.Instance.isUsingItemInventory){
						lastItemInventoryClicked.GetComponent<ItemInventory>().Deselect();
					}
					else{
						switch(tagFirstGameObjectHit){
						case "NPC": case "InteractivePoint": case "ItemInventory":
							firstGameObjectHit.GetComponent<InteractiveElement>().RightClickAction();
							break;
						default:
							Player.Instance.Speak("GUI", "DEFAULT", "NOTHING_OF_INTEREST");
							break;
						}
					}
				}
				else{
					if(Player.Instance.isUsingItemInventory){
						lastItemInventoryClicked.GetComponent<ItemInventory>().Deselect();
					}
				}
			}
		}
	}

	private GameObject FirstObjectOnMouseOver(){
		GameObject _firstGameObject = null;
		Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D[] _hits = null;
		_hits = Physics2D.RaycastAll(new Vector2(_mousePosition.x, _mousePosition.y), Vector2.zero, 10f, Physics2D.DefaultRaycastLayers, -2, 4);
		if(_hits.Length > 0){
			_firstGameObject = _hits[0].collider.gameObject;
			for(int i = 0; i < _hits.Length; i++){
				if(_firstGameObject.transform.position.z < _hits[i].transform.position.z){
					_firstGameObject = _hits[i].collider.gameObject;
				}
			}

			if(Player.Instance.isUsingItemInventory && _firstGameObject.tag == "ItemInventory"){
				if(lastItemInventoryName == _firstGameObject.name){
					_firstGameObject = null;
				}
			}
		}

		return _firstGameObject;
	}
}
