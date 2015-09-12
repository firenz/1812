using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class MultipleChoiceManager : Singleton<MultipleChoiceManager> {
	public bool isSelectionEnded { get; private set;}

	[SerializeField]
	private string pathMultipleChoiceElement = "Prefabs/Other/";
	
	private int choiceResult = -1;
	private Image multipleChoicePanelBackground;
	private VerticalLayoutGroup multipleChoicePanelLayoutGroup;

	private void OnEnable(){
		MultipleChoiceElement.selectedChoiceResult += SelectedChoiceResultListener;
	}

	private void OnDisable(){
		MultipleChoiceElement.selectedChoiceResult -= SelectedChoiceResultListener;
	}
	
	protected override void InitializeOnAwake () {
		isSelectionEnded = true;
		multipleChoicePanelBackground = this.GetComponent<Image>();
		multipleChoicePanelLayoutGroup = this.GetComponent<VerticalLayoutGroup>();
		multipleChoicePanelLayoutGroup.enabled = false;
	}

	private void Update(){
		if(!isSelectionEnded){
			if(multipleChoicePanelBackground.enabled == false){
				multipleChoicePanelBackground.enabled = true;
			}
			CustomCursorController.Instance.ChangeCursorToDefault();
			if(EventSystem.current.IsPointerOverGameObject()){
				CustomCursorController.Instance.ChangeCursorOverUIElement();
			}
		}
		else{
			multipleChoicePanelBackground.enabled = false;
		}
	}

	public void CreateMultipleSelection(string choiceNameID){
		choiceResult = -1;
		isSelectionEnded = false;
		CustomCursorController.Instance.ChangeIngameCursorToMenu();
		int _totalChoices = -1;

		try{
			_totalChoices = LocatedTextManager.GetTextNodesCount("CHOICES", choiceNameID);
		}catch(NullReferenceException){
			Debug.LogError("Choice ID not found");
		}

		multipleChoicePanelLayoutGroup.enabled = true;

		for(int i = 0; i < _totalChoices; i++){
			GameObject _newMultipleChoiceElement = Instantiate(Resources.Load<GameObject>(pathMultipleChoiceElement + "MultipleChoiceElement"));
			_newMultipleChoiceElement.name = "CHOICE_" + i;
			_newMultipleChoiceElement.transform.SetParent(multipleChoicePanelLayoutGroup.transform, false);
			_newMultipleChoiceElement.GetComponent<MultipleChoiceElement>().Initialize(choiceNameID, i);
		}
	}

	public void SelectedChoiceResultListener(int choiceID){
		choiceResult = choiceID;
		EraseMultipleSelection();
	}

	public void EraseMultipleSelection(){
		foreach(Transform multipleChoiceElement in multipleChoicePanelLayoutGroup.transform){
			Destroy(multipleChoiceElement.gameObject);
		}

		CustomCursorController.Instance.ChangeMenuCursorToIngame();
		multipleChoicePanelLayoutGroup.enabled = false;
		isSelectionEnded = true;
	}

	public int GetSelectionResult(){
		if(isSelectionEnded){
			return choiceResult;
		}
		else{
			return -1;
		}
	}
}
