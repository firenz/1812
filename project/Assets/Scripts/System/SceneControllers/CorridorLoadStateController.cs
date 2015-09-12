using UnityEngine;
using System.Collections;

public class CorridorLoadStateController : MonoBehaviour {
	
	private void Start () {
		GameState.lastPlayedLevel = "Corridor";
		CustomCursorController.Instance.ChangeMenuCursorToIngame();
		
		if(!GameState.CutSceneData.isPlayedIntroCorridorPart){
			Inventory.Instance.Disable();
		}
		else if(GameState.MapPuzzleData.isMapPuzzleSolved && !GameState.CutSceneData.isPlayedCutsceneLibrarianMetAfterPuzzleIsSolved){
			Inventory.Instance.Disable();
		}
		else{
			if(Inventory.Instance.IsClosed()){
				Inventory.Instance.Enable();
				Inventory.Instance.Activate();
			}
		}

		if(GameState.LevelCorridorData.isDialogueChoiceMadeWithLibrarianFinished){
			AudioManager.PlayMusic("Joy", true);
		}
		else{
			AudioManager.PlayMusic("Time to Explore", true);
		}
	}
}
