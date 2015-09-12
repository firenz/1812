using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Transform))]
public class ModalWindowHandler : Singleton<ModalWindowHandler> {
	public bool isAnswerSelected { get; private set;}
	public bool isYesClicked { get; private set;}
	public bool isNoClicked { get; private set;}

	public delegate void BeginQuestion();
	public static event BeginQuestion beginQuestion;
	public delegate void EndedQuestion();
	public static event EndedQuestion endedQuestion;

	private Text modalWindowQuestionText;
	private Transform modalWindowTransform;
	private Button yesButton;
	private Button noButton;

	private void OnEnable(){
		YesButton.yesClicked += YesOnClick;
		NoButton.noClicked += NoOnclick;
	}

	private void OnDisable(){
		YesButton.yesClicked -= YesOnClick;
		NoButton.noClicked -= NoOnclick;
	}
	
	protected override void InitializeOnAwake(){}

	private void Start(){
		isAnswerSelected = false;
		isYesClicked = false;
		isNoClicked = false;
		modalWindowTransform = this.transform;
		modalWindowQuestionText = this.transform.FindChild("Text").GetComponent<Text>();
		yesButton = this.transform.FindChild("YesButton").GetComponent<Button>();
		noButton = this.transform.FindChild("NoButton").GetComponent<Button>();

		modalWindowTransform.SetAsFirstSibling();
		yesButton.interactable = false;
		noButton.interactable = false;
	}

	public void YesOnClick(){
		isYesClicked = true;
		isAnswerSelected = true;
		yesButton.interactable = false;
		noButton.interactable = false;
	}

	public void NoOnclick(){
		isNoClicked = true;
		isAnswerSelected = true;
		yesButton.interactable = false;
		noButton.interactable = false;
	}

	public void Initialize(string modalQuestionID){
		modalWindowQuestionText.text = LocatedTextManager.GetLocatedText("OPTIONS_MENU", "MODAL_WINDOW", modalQuestionID)[0];
		yesButton.interactable = true;
		noButton.interactable = true;
		modalWindowTransform.SetAsLastSibling();
		isAnswerSelected = false;
		beginQuestion();
		CustomCursorController.Instance.ChangeCursorToDefault();
	}

	public void Disable(){
		yesButton.interactable = false;
		noButton.interactable = false;
		modalWindowTransform.SetAsFirstSibling();
		isYesClicked = false;
		isNoClicked = false;
		isAnswerSelected = false;
		endedQuestion();
	}
}
