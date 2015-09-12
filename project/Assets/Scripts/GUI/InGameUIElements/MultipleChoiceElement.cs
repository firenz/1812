using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class MultipleChoiceElement : MonoBehaviour {
	[SerializeField]
	private Color mouseOverColor;
	[SerializeField]
	private string sfxOnPlay = "BlipSelect";

	private Color defaultColor;
	private int choiceID = 0;
	private Text multipleChoiceText = null;

	private const int maxCharactersLine = 62;

	public delegate void SelectedChoiceResultEvent(int choiceID);
	public static event SelectedChoiceResultEvent selectedChoiceResult;
	
	private void Awake() {
		multipleChoiceText = this.GetComponent<Text>();
		defaultColor = multipleChoiceText.color;
	}

	public void Initialize(string choiceNameID, int numberID){
		string stringID = "CHOICE_" + numberID;
		string _formattedString = LocatedTextManager.GetLocatedTextFormatted("CHOICES", choiceNameID, stringID, 62, 1)[0];

		multipleChoiceText.text = _formattedString;
        choiceID = numberID;
	}

	public void OnClick(){
		StartCoroutine(WaitForSFXAndExecuteActionOnClick());
	}

	private IEnumerator WaitForSFXAndExecuteActionOnClick(){
		AudioManager.PlaySFX(sfxOnPlay);
		do{
			yield return null;
		}while(AudioManager.IsPlayingSFX(sfxOnPlay));

		selectedChoiceResult(choiceID);
	}

	public void OnMouseOver(){
		CustomCursorController.Instance.ChangeCursorOverUIElement();
		multipleChoiceText.color = mouseOverColor;
	}

	public void OnMouseExit(){
		CustomCursorController.Instance.ChangeCursorToDefault();
		multipleChoiceText.color = defaultColor;
	}	
}
