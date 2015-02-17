using UnityEngine;
using System.Collections;

public class CutScenesManager : MonoBehaviour {
	private static bool isPlaying = false;

	//To be implemented
	//public delegate void EnableGameGUI();
	//public static event EnableGameGUI enableGameGUI;
	//public delegate void DisableGameGUI();
	//public static event DisableGameGUI disableGameGUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!isPlaying){
			switch(Application.loadedLevelName){
			case "DemoScene_01":
				//if(!GameState.CutSceneData.isPlayedIntro){
				//	StartCoroutine(PlayIntro());
				//}
				break;
			case "DemoScene_00":
				break;
			default:
				break;
			}
		}
	}

	private IEnumerator PlayIntro(){
		isPlaying = true;
		GameState.CutSceneData.isPlayedIntro = true;
		Player.Instance.GoTo(new Vector2(41.6329f,64.3537f));
		
		do{
			yield return null;
		}while(Player.Instance.IsWalking());

		Player.Instance.Speak("INVENTORY", "FAILEDTEST", "DESCRIPTION");
		
		do{
			yield return null;
		}while(Player.Instance.IsSpeaking());
		isPlaying = false;
	}

	public static bool IsPlaying(){
		return isPlaying;
	}
}
