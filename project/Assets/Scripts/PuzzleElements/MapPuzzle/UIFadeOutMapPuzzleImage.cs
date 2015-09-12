using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFadeOutMapPuzzleImage : MonoBehaviour {
	[SerializeField]
	private string sceneProfessorOffice = "ProfessorOffice";

	private Image fadeInImage;

	private void OnEnable(){
		PuzzleMapManager.puzzleCompleted += FadeIn;
	}

	private void OnDisable(){
		PuzzleMapManager.puzzleCompleted -= FadeIn;
	}

	private void Start () {
		fadeInImage = this.transform.FindChild("FadeOut").GetComponent<Image>();
		Color _changeAlphaColor = fadeInImage.color;
		_changeAlphaColor.a = 0f;
		fadeInImage.color = _changeAlphaColor;
		fadeInImage.gameObject.SetActive(false);
	}

	private void FadeIn() {
		fadeInImage.gameObject.SetActive(true);
		this.transform.SetAsLastSibling();
		StartCoroutine(FadeAlphaColorAndExitScene(1f, 0.5f));
	}

	private IEnumerator FadeAlphaColorAndExitScene(float alpha, float time){
		float _alphaImage = fadeInImage.color.a;
		
		for(float t = 0f; t < 1f; t += (Time.deltaTime / time)){
			Color _newColor = fadeInImage.color;
			_newColor.a = Mathf.Lerp(_alphaImage, alpha, t);
			fadeInImage.color = _newColor;
			yield return null;
		}

		Inventory.Instance.RemoveItem("LittleFlagsInventory");
		GameController.WarpToLevel(sceneProfessorOffice);
	}
}
