using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndingController : MonoBehaviour {
	public const float secondsDisplayingText = 2.5f;

	private Canvas endingCanvas;
	private Text endingText;
	private bool skipDisplayingTextLine = false;

	private void Start () {
		Inventory.Instance.Disable();
		CustomCursorController.Instance.HideCursor();
		endingCanvas = GameObject.Find("EndingCanvas").GetComponent<Canvas>();
		endingText = endingCanvas.GetComponentInChildren<Text>();
		Color _alphaText = endingText.color;
		_alphaText.a = 0f;
		endingText.color = _alphaText;

		AudioManager.PlayMusic("Joy", true);

		StartCoroutine(Ending());
	}

	private void Update () {
		if(Input.GetMouseButtonDown(0) || Input.anyKeyDown){
			skipDisplayingTextLine = true;
		}
	}

	private IEnumerator Ending(){
		yield return new WaitForSeconds(0.5f);

		endingText.text = LocatedTextManager.GetLocatedText("CUTSCENES", "CUTSCENE_ENDING", "THE_END")[0];

		yield return StartCoroutine(FadeAlphaColor(1f, 0.8f));

		yield return new WaitForSeconds(0.2f);

		do{
			yield return null;
		}while(!skipDisplayingTextLine);

		yield return StartCoroutine(FadeAlphaColor(0f, 0.4f));
		yield return new WaitForSeconds(0.2f);

		GameController.WarpToLevel("MainMenu");
	}

	private IEnumerator FadeAlphaColor(float alpha, float time){
		float _alphaText = endingText.color.a;
		
		for(float t = 0f; t < 1f; t += (Time.deltaTime / time)){
			Color _newColor = endingText.color;
			_newColor.a = Mathf.Lerp(_alphaText, alpha, t);
			endingText.color = _newColor;
			yield return null;
		}
	}
}
