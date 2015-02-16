using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Player))]
public class MouseClickHandler : MonoBehaviour {
	private const float delayBetweenMouseClicks = 0.5f;
	private float lastTimeMouseWasClicked; //For handling waiting animation states
	private string lastInventoryItemClicked = "";

	private enum hitTypes{
		walkableFloor = 0,
		interactiveObject,
		interactiveNPC,
		inventoryItem,
		noType
	}
	
	private void Start () {
		lastTimeMouseWasClicked = Time.time;
	}

	private void Update () {
		if(!Player.Instance.IsSpeaking() && !Player.Instance.IsInConversation() && !Player.Instance.IsInteracting() && !CutScenesManager.IsPlaying()){
			if(Input.GetMouseButtonDown(0)){
				Player.Instance.SetWaitingInactive();

				lastTimeMouseWasClicked = Time.time;
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
					Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					Player.Instance.GoTo(_mousePosition);
					break;
				default:
					break;
				}
			}
			else if(Input.GetMouseButtonDown(1)){
				Player.Instance.SetWaitingInactive();

				lastTimeMouseWasClicked = Time.time;
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

	public float GetLastTimeMouseWasClicked(){
		return lastTimeMouseWasClicked;
	}
}
