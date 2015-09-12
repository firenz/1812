using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutUCALogo : MonoBehaviour {
	private Image iconUCA;
	private Image backgroundUCA;

	public delegate void FadeOutEnds();
	public static event FadeOutEnds onFadeOutEnd;

	private void Start () {
		iconUCA = this.transform.FindChild("IconUCA").GetComponent<Image>();
		backgroundUCA = this.transform.FindChild("BackgroundUCA").GetComponent<Image>();

		StartCoroutine(WaitForFadeOut(2f, 1f));
	}

	private IEnumerator WaitForFadeOut(float waitUntilFadeOutTime, float fadeOuttime){
		CustomCursorController.Instance.HideCursor();
		yield return new WaitForSeconds(waitUntilFadeOutTime);

		float _alphaIconUCA = iconUCA.color.a;
		float _alphaBgUCA = backgroundUCA.color.a;

		for(float t = 0f; t < 1f; t += (Time.deltaTime / fadeOuttime)){
			Color _newColor1 = iconUCA.color;
			Color _newColor2 = backgroundUCA.color;
			_newColor1.a = Mathf.Lerp(_alphaIconUCA, 0f, t);
			_newColor2.a = Mathf.Lerp(_alphaBgUCA, 0f, t);
			iconUCA.color = _newColor1;
			backgroundUCA.color = _newColor2;
			yield return null;
		}

		this.transform.SetAsFirstSibling();
		CustomCursorController.Instance.UnhideCursor();

		yield return new WaitForSeconds(0.2f);

		onFadeOutEnd();
	}
}
