using UnityEngine;
using System.Collections;

public class ProfessorOfficeLoadStateController : MonoBehaviour {

	private void Start () {
		GameState.lastPlayedLevel = "ProfessorOffice";
		CustomCursorController.Instance.ChangeMenuCursorToIngame();
		
		if(!GameState.CutSceneData.isPlayedIntroProfessorOfficePart){
			Inventory.Instance.Disable();
		}
		else if(GameState.MapPuzzleData.isMapPuzzleSolved && !GameState.CutSceneData.isPlayedCutsceneAfterMapPuzzleIsSolved){
			Inventory.Instance.Disable();
		}
		else{
			if(Inventory.Instance.IsClosed()){
				Inventory.Instance.Enable();
				Inventory.Instance.Activate();
			}
		}
		
		if(GameState.CutSceneData.isPlayedIntroProfessorOfficePart 
		   && GameState.MapPuzzleData.isMapPuzzleSolved 
		   && !GameState.CutSceneData.isPlayedCutsceneAfterMapPuzzleIsSolved){
			AudioManager.PlayMusic("Joy", true);
		}
		else{
			AudioManager.PlayMusic("Time to Explore", true);
		}
	}
}
