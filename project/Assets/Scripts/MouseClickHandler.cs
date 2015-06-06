﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Player))]
public class MouseClickHandler : MonoBehaviour {
	private const float delayBetweenMouseClicks = 0.5f;
	private float lastTimeMouseWasClicked; //For handling waiting animation states
	private string lastInventoryItemClicked = "";
	private string lastInventoryItemClickedDisplayName = "";
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
	}

	private void Update () {
		DisplayNameText.GetComponent<Text>().text = "";
		GameObject _firstGameObject = FirstObjectOnMouseOver();
		if(_firstGameObject != null){
			string _firstGameObjectTag = _firstGameObject.tag;
			if(_firstGameObjectTag != "NavigationPolygon"){
				CheckMouseIfPassingOverInteractiveElements(_firstGameObject.GetComponent<InteractiveElement>());
			}
			else{
				CustomCursorController.Instance.ChangeCursorToDefault();
			}
		}
		else{
			if(!CustomCursorController.Instance.isOverUIButton){
				CustomCursorController.Instance.ChangeCursorToDefault();
			}
		}

		if(Input.GetMouseButtonDown(0)){
			if((Time.time - lastTimeMouseWasClicked) > 0.55f){
				lastTimeMouseWasClicked = Time.time;
				/*
				Debug.Log("timeBetweenLastClick: " + (Time.time - lastTimeMouseWasClicked).ToString());
				lastTimeMouseWasClicked = Time.time;
				Debug.Log("lastTimeMouseWasClicked: " + lastTimeMouseWasClicked.ToString());
				Debug.Log("isInConversation: " + Player.Instance.isInConversation.ToString());
				Debug.Log("isDoingAnAction: " + Player.Instance.isDoingAction.ToString());
				Debug.Log("======================================");
				*/

				if(!Player.Instance.isDoingAction && !Player.Instance.isSpeaking && MultipleChoiceManager.Instance.IsSelectionEnded() && !CutScenesManager.IsPlaying()){
					lastTimeMouseWasClicked = Time.time;
					Player.Instance.SetWaitingInactive();

					if(_firstGameObject != null){
						string _firstGameObjectTag = _firstGameObject.tag;

						if(_firstGameObjectTag == "NavigationPolygon"){
							Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
							Player.Instance.GoTo(_mousePosition);
						}
						else{
							InteractiveElement _interactiveElementClicked = _firstGameObject.GetComponent<InteractiveElement>();

							if(_firstGameObjectTag == "NPC" || _firstGameObjectTag == "InteractivePoint"){
								if(Player.Instance.IsUsingItemInventory()){
									Player.Instance.SetUsingInventoryInactive();
									_interactiveElementClicked.ActionOnItemInventoryUsed(lastInventoryItemClicked);
								}
								else{
									_interactiveElementClicked.LeftClickAction();
								}
								
							}

							if(_firstGameObjectTag == "ItemInventory"){
								if(!Player.Instance.IsUsingItemInventory()){
									lastInventoryItemClicked = _firstGameObject.name;
									lastInventoryItemClickedDisplayName = _firstGameObject.GetComponent<InteractiveElement>().GetName();
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
				if(!Player.Instance.isDoingAction && !Player.Instance.isSpeaking && MultipleChoiceManager.Instance.IsSelectionEnded() && !CutScenesManager.IsPlaying()){
					Player.Instance.SetWaitingInactive();
					lastTimeMouseWasClicked = Time.time;

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

	public void CheckMouseIfPassingOverInteractiveElements(InteractiveElement InteractiveElementPassedByMouse){
		if(MultipleChoiceManager.Instance.IsSelectionEnded()){
			if(Player.Instance.IsUsingItemInventory()){
				if(InteractiveElementPassedByMouse.tag == "NPC" || InteractiveElementPassedByMouse.tag == "InteractivePoint"){
					DisplayNameText.GetComponent<Text>().text = "Usar " + lastInventoryItemClickedDisplayName + " con " + InteractiveElementPassedByMouse.GetName();
				}
				else{
					DisplayNameText.GetComponent<Text>().text = "Usar " + lastInventoryItemClickedDisplayName + " con ";
				}
			}
			else{
				if(InteractiveElementPassedByMouse != null){
					if(InteractiveElementPassedByMouse.tag == "GUI"){
						Debug.Log("Interactive elemented passed is GUI");
					}
					else{
						DisplayNameText.GetComponent<Text>().text = InteractiveElementPassedByMouse.GetName();
						InteractiveElementPassedByMouse.GetComponent<InteractiveElement>().ChangeCursorOnMouseOver();
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
					_firstGameObject = _hits[i].collider.gameObject;
				}
			}

			if(Player.Instance.IsUsingItemInventory() && _firstGameObject.tag == "ItemInventory"){
				_firstGameObject = null;
			}
		}

		return _firstGameObject;
	}

	public float GetLastTimeMouseWasClicked(){
		return lastTimeMouseWasClicked;
	}
}