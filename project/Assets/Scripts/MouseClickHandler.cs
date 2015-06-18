using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Player))]
public class MouseClickHandler : MonoBehaviour {
	public float lastTimeMouseWasClicked{ get; private set;} //For handling waiting animation states

	private const float delayBetweenMouseClicks = 0.5f;
	private GameObject lastItemInventoryClicked;
	private string useText;
	private string withText;
	private string lastItemInventoryName= "";
	private string lastInventoryItemDisplayName = "";

	protected Text DisplayNameText;

	private enum hitTypes{
		walkableFloor = 0,
		interactiveObject,
		interactiveNPC,
		inventoryItem,
		noType
	}

	private void Start () {
		lastTimeMouseWasClicked = Time.time;
		DisplayNameText = GameObject.Find("NameInteractiveElementText").GetComponent<Text>();
		DisplayNameText.GetComponent<Text>().text = "";
		Inventory.Instance.UpdateAllItems();
		useText = LocalizedTextManager.GetLocalizedText("GUI", "DEFAULT", "USE_IN_DISPLAYNAME")[0];
		withText = LocalizedTextManager.GetLocalizedText("GUI", "DEFAULT", "WITH_IN_DISPLAYNAME")[0];
	}

	private void Update(){
		DisplayNameText.text = "";
		GameObject _firstGameObjectHit = FirstObjectOnMouseOver();
		string _tagFirstGO;

		try{
			_tagFirstGO = _firstGameObjectHit.tag;
		}
		catch(NullReferenceException){
			_tagFirstGO = "";
		}

		if(_firstGameObjectHit != null){

			switch(_tagFirstGO){
			case "NavigationPolygon":
				if(Player.Instance.IsUsingItemInventory()){
					DisplayNameText.text = useText + " " + lastInventoryItemDisplayName + " " + withText;
				}
				CustomCursorController.Instance.ChangeCursorToDefault();
				break;
			case "NPC": case "InteractivePoint": case "ItemInventory":
				string _nameInteractiveElement = _firstGameObjectHit.GetComponent<InteractiveElement>().displayName;

				if(Player.Instance.IsUsingItemInventory()){
					DisplayNameText.text = useText + " " + lastInventoryItemDisplayName + " " + withText + " " + _nameInteractiveElement;
				}
				else{
					DisplayNameText.text = _nameInteractiveElement;
					_firstGameObjectHit.GetComponent<InteractiveElement>().ChangeCursorOnMouseOver();
				}
				break;
			default:
				if(!CustomCursorController.Instance.isOverUIButton){
					if(Player.Instance.isUsingItemInventory){
						DisplayNameText.text = useText + " " + lastInventoryItemDisplayName + " " + withText;
					}
					CustomCursorController.Instance.ChangeCursorToDefault();
				}
				break;
			}
		}
		else{
			if(!CustomCursorController.Instance.isOverUIButton){
				if(Player.Instance.IsUsingItemInventory()){
					DisplayNameText.text = useText + " " + lastInventoryItemDisplayName + " " + withText;
				}
				CustomCursorController.Instance.ChangeCursorToDefault();
			}
		}

		if(Input.GetMouseButtonDown(0)){
			if((Time.time - lastTimeMouseWasClicked) > 0.55f){
				lastTimeMouseWasClicked = Time.time;
				
				if(!Player.Instance.isDoingAction && !Player.Instance.isSpeaking && MultipleChoiceManager.Instance.IsSelectionEnded() && !CutScenesManager.IsPlaying()){
					lastTimeMouseWasClicked = Time.time;
					Player.Instance.SetWaitingInactive();

					if(_firstGameObjectHit != null){

						switch(_tagFirstGO){
						case "NavigationPolygon":
							if(Player.Instance.isUsingItemInventory){
								//lastItemInventoryClicked.GetComponent<ItemInventory>().Unselect();
							}
							else{
								Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
								Player.Instance.GoTo(_mousePosition);
							}
							break;
						case "NPC": case "InteractivePoint":
							InteractiveElement _interactiveElement = _firstGameObjectHit.GetComponent<InteractiveElement>();
							if(Player.Instance.isUsingItemInventory){
								//lastItemInventoryClicked.GetComponent<ItemInventory>().Unselect();
								_interactiveElement.ActionOnItemInventoryUsed(lastItemInventoryClicked);
							}
							else{
								_interactiveElement.LeftClickAction();
							}
							break;
						case "ItemInventory":
							if(Player.Instance.isUsingItemInventory){
								_firstGameObjectHit.GetComponent<InteractiveElement>().ActionOnItemInventoryUsed(lastItemInventoryClicked);
								//lastItemInventoryClicked.GetComponent<ItemInventory>().Unselect();
							}
							else{
								lastItemInventoryClicked = _firstGameObjectHit;
								lastItemInventoryName = _firstGameObjectHit.name;
								lastInventoryItemDisplayName = _firstGameObjectHit.GetComponent<InteractiveElement>().displayName;
								lastItemInventoryClicked.GetComponent<ItemInventory>().Select();
							}
							break;
						default:
							if(Player.Instance.isUsingItemInventory){
								//lastItemInventoryClicked.GetComponent<ItemInventory>().Unselect();
							}
							break;
						}
					}
					else{
						if(Player.Instance.isUsingItemInventory){
							//lastItemInventoryClicked.GetComponent<ItemInventory>().Unselect();
						}
					}
				}
			}
		}
		if(Input.GetMouseButtonDown(1)){
			if((Time.time - lastTimeMouseWasClicked) > 0.55f){
				lastTimeMouseWasClicked = Time.time;
				
				if(!Player.Instance.isDoingAction && !Player.Instance.isSpeaking && MultipleChoiceManager.Instance.IsSelectionEnded() && !CutScenesManager.IsPlaying()){
					lastTimeMouseWasClicked = Time.time;
					Player.Instance.SetWaitingInactive();
					
					if(_firstGameObjectHit != null){
						if(Player.Instance.isUsingItemInventory){
							lastItemInventoryClicked.GetComponent<ItemInventory>().Deselect();
						}
						else{
							switch(_tagFirstGO){
							case "NPC": case "InteractivePoint": case "ItemInventory":
								_firstGameObjectHit.GetComponent<InteractiveElement>().RightClickAction();
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

	}

	private GameObject FirstObjectOnMouseOver(){
		GameObject _firstGameObject = null;
		Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D[] _hits = null;
		_hits = Physics2D.RaycastAll(new Vector2(_mousePosition.x, _mousePosition.y), Vector2.zero, 0f, Physics2D.DefaultRaycastLayers, -2, 4);
		if(_hits.Length > 0){
			_firstGameObject = _hits[0].collider.gameObject;
			for(int i = 0; i < _hits.Length; i++){
				if(_firstGameObject.transform.position.z < _hits[i].transform.position.z){
					if(_hits[i].transform.gameObject.tag != "GUI"){
						_firstGameObject = _hits[i].collider.gameObject;
					}
				}
			}

			if(Player.Instance.IsUsingItemInventory() && _firstGameObject.tag == "ItemInventory"){
				if(lastItemInventoryName == _firstGameObject.name){
					_firstGameObject = null;
				}
			}
		}

		return _firstGameObject;
	}
	
}
