using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Player))]
public class MouseClickHandler : MonoBehaviour {
	private const float delayBetweenMouseClicks = 0.5f;
	private float lastTimeMouseWasClicked; //For handling waiting animation states
	private string lastInventoryItemClicked = "";
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
		GameObject _nameInteractiveElementText = GameObject.Find("NameInteractiveElementText");
		//_nameInteractiveElementText.GetComponent<Rect>().y = (10f / 100f)*(Screen.height * 0.5f);
		DisplayNameText = _nameInteractiveElementText.GetComponent<Text>();
		DisplayNameText.text = "";
		
	}

	private void Update () {
		DisplayNameText.GetComponent<Text>().text = "";
		GameObject _firstGameObject = FirstObjectOnMouseOver();
		if(_firstGameObject != null){
			CheckMouseIfPassingOverInteractiveElements(_firstGameObject.GetComponent<InteractiveElement>());
		}

		if(Input.GetMouseButtonDown(0)){
			if((Time.time - lastTimeMouseWasClicked) > 0.55f){
				Debug.Log("timeBetweenLastClick: " + (Time.time - lastTimeMouseWasClicked).ToString());
				lastTimeMouseWasClicked = Time.time;
				Debug.Log("lastTimeMouseWasClicked: " + lastTimeMouseWasClicked.ToString());
				Debug.Log("isInConversation: " + Player.Instance.isInConversation.ToString());
				Debug.Log("isDoingAnAction: " + Player.Instance.isDoingAction.ToString());
				Debug.Log("======================================");
				if(!Player.Instance.isDoingAction && !Player.Instance.isSpeaking && !CutScenesManager.IsPlaying()){
					lastTimeMouseWasClicked = Time.time;
					Player.Instance.SetWaitingInactive();
					/*
					GameObject _hitObject;
					hitTypes _currentHitType = hitTypes.noType;
					
					_currentHitType = HandleHitTypeOnMultipleColliders(out _hitObject);

					switch(_currentHitType){
					case hitTypes.inventoryItem:
						if(!Player.Instance.IsUsingItemInventory()){
							lastInventoryItemClicked = _hitObject.name;
							Player.Instance.SetUsingInventoryActive();
						}
						//_hitObject.GetComponent<InteractiveElement>().LeftClickAction();
						break;
					case hitTypes.interactiveNPC: case hitTypes.interactiveObject:
						if(Player.Instance.IsUsingItemInventory()){
							_hitObject.GetComponent<InteractiveElement>().ActionOnItemInventoryUsed(lastInventoryItemClicked);
							Player.Instance.SetUsingInventoryInactive();
						}
						else{
							_hitObject.GetComponent<InteractiveElement>().LeftClickAction();
						}
						break;
					case hitTypes.walkableFloor:
						if(Player.Instance.IsUsingItemInventory()){
							Player.Instance.SetUsingInventoryInactive();
						}
						else{
							Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
							Player.Instance.GoTo(_mousePosition);
						}
						break;
					default:
						if(Player.Instance.IsUsingItemInventory()){
							Player.Instance.SetUsingInventoryInactive();
						}
						break;
					}
					*/

					Debug.Log("Object tag: " + _firstGameObject.tag);
					if(_firstGameObject != null){
						string _firstGameObjectTag = _firstGameObject.tag;

						if(_firstGameObjectTag == "NavigationPolygon"){
							Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
							Player.Instance.GoTo(_mousePosition);
						}
						else{
							InteractiveElement _interactiveElementClicked = _firstGameObject.GetComponent<InteractiveElement>();
							
							if(_firstGameObjectTag == "ItemInventory"){
								if(!Player.Instance.IsUsingItemInventory()){
									lastInventoryItemClicked = _firstGameObject.name;
									Player.Instance.SetUsingInventoryActive();
								}
								else{
									Player.Instance.SetUsingInventoryInactive();
									_interactiveElementClicked.ActionOnItemInventoryUsed(lastInventoryItemClicked);
								}
							}
							else{
								if(Player.Instance.IsUsingItemInventory()){
									Player.Instance.SetUsingInventoryInactive();
								}
							}
							
							if(_firstGameObjectTag == "NPC" || _firstGameObjectTag == "InteractivePoint"){
								if(Player.Instance.IsUsingItemInventory()){
									Player.Instance.SetUsingInventoryInactive();
									_interactiveElementClicked.ActionOnItemInventoryUsed(lastInventoryItemClicked);
								}
								else{
									_interactiveElementClicked.RightClickAction();
								}

							}
							
						}
					}
					else{
						if(Player.Instance.IsUsingItemInventory()){
							Player.Instance.SetUsingInventoryInactive();
						}
					}

				}
			}
		}
		else if(Input.GetMouseButtonDown(1)){
			if((Time.time - lastTimeMouseWasClicked) > 0.55f){
				//lastTimeMouseWasClicked = Time.time;
				if(!Player.Instance.isDoingAction && !Player.Instance.isSpeaking && !CutScenesManager.IsPlaying()){
					Player.Instance.SetWaitingInactive();
					lastTimeMouseWasClicked = Time.time;
					/*
					GameObject _hitObject;
					hitTypes _currentHitType = hitTypes.noType;
					
					_currentHitType = HandleHitTypeOnMultipleColliders(out _hitObject);
					
					switch(_currentHitType){
					case hitTypes.inventoryItem: case hitTypes.interactiveNPC: case hitTypes.interactiveObject:
						_hitObject.GetComponent<InteractiveElement>().RightClickAction();
						break;
					case hitTypes.walkableFloor:
						Player.Instance.Speak("GUI", "DEFAULT", "NOTHING_OF_INTEREST");
						break;
					default:
						break;
					}
					*/

					InteractiveElement _interactiveElementClicked = _firstGameObject.GetComponent<InteractiveElement>();

					if(_interactiveElementClicked != null){
						_interactiveElementClicked.RightClickAction();
					}
					else{
						Player.Instance.Speak("GUI", "DEFAULT", "NOTHING_OF_INTEREST");
					}
				}
			}
		}		
	}

	//This method is needed because Unity doesnt handle well 2D Colliders which are superposed, so what this 
	//method does is select the collider with the highest priority in game
	private hitTypes HandleHitTypeOnMultipleColliders(out GameObject hitObject){
		Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D[] _hits = null;
		_hits = Physics2D.RaycastAll(new Vector2(_mousePosition.x, _mousePosition.y), Vector2.zero, 0f);
		int _currentHitSelected = 0;
		hitTypes _currentHitSelectedType = hitTypes.noType;

		if(_hits != null){
			for(int hitIterator = 0; hitIterator < _hits.Length; hitIterator++){
				switch(_hits[hitIterator].collider.tag){
				case "NPC":
					_currentHitSelectedType = hitTypes.interactiveNPC;
					_currentHitSelected = hitIterator;

					break;
				case "InteractivePoint":
					if( _currentHitSelectedType != hitTypes.interactiveNPC){
						_currentHitSelectedType = hitTypes.interactiveObject;
						_currentHitSelected = hitIterator;

					}
					break;
				case "ItemInventory":
					if(_currentHitSelectedType != hitTypes.interactiveNPC && _currentHitSelectedType != hitTypes.interactiveObject){
						_currentHitSelectedType = hitTypes.inventoryItem;
						_currentHitSelected = hitIterator;
					}
					break;
				case "NavigationPolygon" : case "NavigationLinks":
					if(_currentHitSelectedType != hitTypes.inventoryItem && _currentHitSelectedType != hitTypes.interactiveNPC && _currentHitSelectedType != hitTypes.interactiveObject){
						_currentHitSelectedType = hitTypes.walkableFloor;
						_currentHitSelected = hitIterator;
					}
					break;
				default:
					break;
				}
			}
		}

		if(_currentHitSelectedType == hitTypes.noType){
			hitObject = null;
		}
		else{
			hitObject = _hits[_currentHitSelected].collider.gameObject;
		}
		
		return _currentHitSelectedType;
	}

	public void CheckMouseIfPassingOverInteractiveElements(InteractiveElement InteractiveElementPassedByMouse){

		if(InteractiveElementPassedByMouse != null){
			DisplayNameText.GetComponent<Text>().text = InteractiveElementPassedByMouse.GetName();
		}
	}

	private GameObject FirstObjectOnMouseOver(){
		GameObject _firstGameObject = null;
		Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D[] _hits = null;
		_hits = Physics2D.RaycastAll(new Vector2(_mousePosition.x, _mousePosition.y), Vector2.zero, 0f, Physics2D.DefaultRaycastLayers, -2, 4);

		if(_hits.Length > 0){
			//if(_hits.Length > 1){
			//	Debug.Log("Hits: " + _hits.Length.ToString());
			//}
			_firstGameObject = _hits[0].collider.gameObject;
			//Debug.Log("First Z: " + _firstGameObject.transform.position.z.ToString());

			for(int i = 0; i < _hits.Length; i++){
				if(_firstGameObject.transform.position.z < _hits[i].transform.position.z){
					_firstGameObject = _hits[i].collider.gameObject;
				}
			}
		}

		return _firstGameObject;
	}

	public float GetLastTimeMouseWasClicked(){
		return lastTimeMouseWasClicked;
	}
}
