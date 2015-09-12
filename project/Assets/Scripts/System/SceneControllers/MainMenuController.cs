using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	private void OnEnable(){
		FadeOutUCALogo.onFadeOutEnd += PlayAudioWhenFadeOutEnds;
	}

	private void OnDisable(){
		FadeOutUCALogo.onFadeOutEnd -= PlayAudioWhenFadeOutEnds;
	}

	private void Start () {
		CustomCursorController.Instance.ChangeIngameCursorToMenu();
		Inventory.Instance.Disable();	
	}

	private void PlayAudioWhenFadeOutEnds(){
		AudioManager.PlayMusic("Tranquility", true);
	}
}
