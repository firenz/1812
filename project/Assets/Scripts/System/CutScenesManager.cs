using UnityEngine;
using System.Collections;

/// <summary>
/// Gestor y controlador de escenas cinematicas.
/// </summary>
public class CutScenesManager : MonoBehaviour {
	private static bool isPlaying = false;

	private string localizedTextGroup = "CUTSCENES";

	private void Update () {
		if(!isPlaying){
			switch(Application.loadedLevelName){
			case "Corridor":
				if(!GameState.CutSceneData.isPlayedIntroCorridorPart){
					StartCoroutine(PlayIntroCorridorPart());
				}
				else if(GameState.CutSceneData.isPlayedCutsceneAfterMapPuzzleIsSolved 
				        && !GameState.CutSceneData.isPlayedCutsceneLibrarianMetAfterPuzzleIsSolved){
					StartCoroutine(PlayCutsceneAfterSolvingMapPuzzleInCorridor());
				}
				break;
			case "ProfessorOffice":
				if(!GameState.CutSceneData.isPlayedIntroProfessorOfficePart){
					StartCoroutine(PlayIntroProfessorOfficePart());
				}
				else if(GameState.MapPuzzleData.isMapPuzzleSolved 
				        && !GameState.CutSceneData.isPlayedCutsceneAfterMapPuzzleIsSolved){
					StartCoroutine(PlayCutsceneAfterSolvingMapPuzzleInProfessorOffice());
				}
				break;
			default:
				break;
			}
		}
	}

	private IEnumerator PlayIntroCorridorPart(){
		isPlaying = true;

		Player.Instance.GoTo(GameObject.Find("CutscenePoint_0").transform.position);
		do{
			yield return null;
		}while(Player.Instance.isWalking);

		Player.Instance.Speak(localizedTextGroup, "CUTSCENE_1", "PLAYER_DIALOGUE_1");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);

		Player.Instance.GoTo(GameObject.Find("CutscenePoint_1").transform.position);
		do{
			yield return null;
		}while(Player.Instance.isWalking);

		Player.Instance.Speak(localizedTextGroup, "CUTSCENE_1", "PLAYER_DIALOGUE_2");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);

		Player.Instance.GoTo(GameObject.Find("CutscenePoint_0").transform.position);
		do{
			yield return null;
		}while(Player.Instance.isWalking);

		Player.Instance.Speak(localizedTextGroup, "CUTSCENE_1", "PLAYER_DIALOGUE_3");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);

		Player.Instance.GoTo(GameObject.Find("CorridorDoor").GetComponent<CorridorDoor>().interactivePosition);
		do{
			yield return null;
		}while(Player.Instance.isWalking);

		Player.Instance.Speak(localizedTextGroup, "CUTSCENE_1", "PLAYER_DIALOGUE_4");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);

		yield return new WaitForSeconds(0.2f);

		AudioManager.PlaySFX("OpenWindow_v2");
		do{
			yield return null;
		}while(AudioManager.IsPlayingSFX("OpenWindow_v2"));

		Inventory.Instance.AddItem("FailedTestInventory");
		GameState.CutSceneData.isPlayedIntroCorridorPart = true;
		isPlaying = false;
		GameController.WarpToLevel("ProfessorOffice");
	}

	private IEnumerator PlayIntroProfessorOfficePart(){
		isPlaying = true;

		Player.Instance.Speak(localizedTextGroup, "CUTSCENE_2", "PLAYER_DIALOGUE_1");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);

		Window _windowScript = GameObject.Find("Window").GetComponent<Window>();

		Player.Instance.GoTo(_windowScript.interactivePosition);
		do{
			yield return null;
		}while(Player.Instance.isWalking);

		Player.Instance.Manipulate(_windowScript.GetComponent<Window>());
		do{
			yield return null;
		}while(Player.Instance.isDoingAction);

		do{
			yield return null;
		}while(!GameState.LevelProfessorOfficeData.isLittleFlagsFellIntoFloor);

		yield return new WaitForSeconds(0.2f);
		Player.Instance.LookToTheRight();
		yield return new WaitForSeconds(0.2f);

		Player.Instance.Speak(localizedTextGroup, "CUTSCENE_2", "PLAYER_DIALOGUE_2");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);

		yield return new WaitForSeconds(0.2f);
		Player.Instance.LookToTheLeft();
		yield return new WaitForSeconds(0.5f);

		Player.Instance.Manipulate(_windowScript);
		do{
			yield return null;
		}while(Player.Instance.isDoingAction);
		yield return new WaitForSeconds(1f);

		Player.Instance.GoTo(GameObject.Find("CutscenePoint_0").transform.position);
		do{
			yield return null;
		}while(Player.Instance.isWalking);

		yield return new WaitForSeconds(0.2f);
		Player.Instance.LookToTheLeft();
		yield return new WaitForSeconds(0.2f);

		Player.Instance.Speak(localizedTextGroup, "CUTSCENE_2", "PLAYER_DIALOGUE_3");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);

		Inventory.Instance.Enable();
		Inventory.Instance.Activate();
		GameState.CutSceneData.isPlayedIntroProfessorOfficePart = true;
		isPlaying = false;
	}

	private IEnumerator PlayCutsceneAfterSolvingMapPuzzleInProfessorOffice(){
		isPlaying = true;

		Inventory.Instance.Disable();

		Player.Instance.Speak(localizedTextGroup, "CUTSCENE_3", "PLAYER_DIALOGUE_1");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);

		Player.Instance.GoTo(GameObject.Find("ProfessorOfficeDoor").GetComponent<Door>().interactivePosition);
		do{
			yield return null;
		}while(Player.Instance.isWalking);

		yield return new WaitForSeconds(0.2f);

		Player.Instance.Speak(localizedTextGroup, "CUTSCENE_3", "PLAYER_DIALOGUE_2");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);

		yield return new WaitForSeconds(0.2f);
		
		AudioManager.PlaySFX("OpenWindow_v2");
		do{
			yield return null;
		}while(AudioManager.IsPlayingSFX("OpenWindow_v2"));

		GameState.CutSceneData.isPlayedCutsceneAfterMapPuzzleIsSolved = true;
		isPlaying = false;
		GameController.WarpToLevel("Corridor");
	}

	private IEnumerator PlayCutsceneAfterSolvingMapPuzzleInCorridor(){
		isPlaying = true;

		Librarian _librarian = GameObject.Find("Librarian").GetComponent<Librarian>();

		Player.Instance.LookToTheRight();
		yield return new WaitForSeconds(0.3f);

		_librarian.Speak(localizedTextGroup, "CUTSCENE_4", "LIBRARIAN_DIALOGUE_1");
		do{
			yield return null;
		}while(_librarian.isSpeaking);

		yield return new WaitForSeconds(0.2f);
		
		Player.Instance.Speak(localizedTextGroup, "CUTSCENE_4", "PLAYER_DIALOGUE_1");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);

		Inventory.Instance.Enable();
		Inventory.Instance.Activate();
		GameState.CutSceneData.isPlayedCutsceneLibrarianMetAfterPuzzleIsSolved = true;
		isPlaying = false;
	}

	public static bool IsPlaying(){
		return isPlaying;
	}
}
