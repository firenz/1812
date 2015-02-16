using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameState{
	public static string lastPlayableLevel;
	public static Vector2 lastPlayerPosition{
		get{
			switch(Application.loadedLevelName){
			case "DemoScene_00" :
				return LevelProfessorOfficeData.playerPosition;
			case "DemoScene_01" :
				if(CutSceneData.isPlayedIntro == false){
					return Vector2.zero;
				}
				return LevelCorridorData.playerPosition;
				//More to come...
				
			default:
				return Vector2.zero;
			}
		}
	}

	public static Vector2 lastTargetedPositionByMouse{
		get{
			switch(Application.loadedLevelName){
			case "DemoScene_00" :
				return LevelProfessorOfficeData.lastTargetedPositionByMouse;
			case "DemoScene_01" :
				return LevelCorridorData.lastTargetedPositionByMouse;
				//More to come...
				
			default:
				return Vector2.zero;
			}
		}
	}

	public static class SystemData{
		public struct ScreenSettings{
			public static int width;
			public static int height;
			public static bool fullscreen;
		}
		
		//Here struct for AudioSettings...
		
		public static string currentLanguage;
		public static ScreenSettings screenSettings;
	}
	
	public static class CutSceneData{
		public static bool isPlayedIntro;
	}
	
	public static class LevelProfessorOfficeData{
		public static Vector2 playerPosition;
		public static Vector2 lastTargetedPositionByMouse;
		public static bool isWindowOpened;
		public static bool isLittleFlagsPickedFromFloor;
	}
	
	public static class LevelCorridorData{
		public static Vector2 playerPosition;
		public static Vector2 lastTargetedPositionByMouse;
		public static int timesTrashBinWasExaminated;
	}
	
	//More classes here when new leves are created
	//...
	
	
	public static void InitializeState(){
		lastPlayableLevel = "DemoScene_01";
		
		SystemData.ScreenSettings.width = 640;
		SystemData.ScreenSettings.height = 480;
		SystemData.ScreenSettings.fullscreen = false;
		SystemData.currentLanguage = "ES";
		
		CutSceneData.isPlayedIntro = false;
		
		LevelCorridorData.playerPosition = new Vector2(228.5002f, 51.34025f);
		LevelCorridorData.timesTrashBinWasExaminated = 0;
		
		LevelProfessorOfficeData.playerPosition = new Vector2(236.0f, 68.5f);
		LevelProfessorOfficeData.isWindowOpened = false;
		LevelProfessorOfficeData.isLittleFlagsPickedFromFloor = false;
	}

	public static void SaveGameState(){

		switch(Application.loadedLevelName){
		case "DemoScene_00" :
			LevelProfessorOfficeData.playerPosition = Player.Instance.transform.position;
			LevelProfessorOfficeData.lastTargetedPositionByMouse = Player.Instance.LastTargetedPosition();
			LevelProfessorOfficeData.isWindowOpened = GameObject.Find("Window").GetComponent<Window>().isOpened;
			LevelProfessorOfficeData.isLittleFlagsPickedFromFloor = GameObject.Find("LittleFlags").GetComponent<LittleFlags>().IsInactive();
			break;
		case "DemoScene_01" :
			LevelCorridorData.playerPosition = Player.Instance.transform.position;
			LevelCorridorData.lastTargetedPositionByMouse = Player.Instance.LastTargetedPosition();
			LevelCorridorData.timesTrashBinWasExaminated = GameObject.Find("TrashBin").GetComponent<TrashBin>().TimesInteracted();
			break;
			
			//More to come...
			
		default:
			break;
		}
		
	}
	
	
	public static void LoadGameState(string levelToLoad){
		switch(levelToLoad){
		case "DemoScene_00" :
			lastPlayableLevel = levelToLoad;
			Inventory.Instance.Enable();
			break;
		case "DemoScene_01" :
			lastPlayableLevel = levelToLoad;

			if(CutSceneData.isPlayedIntro){
				Inventory.Instance.Enable();
			}
			else{
				Inventory.Instance.Disable();
			}

			/* To be implemented
			if(!GameState.CutSceneData.isPlayedIntro){
				disableGameGUI();
			}
			else{
				enableGameGUI();
			}
			*/
			
			break;
			
			//More to come...
			
		default:
			break;
		}
	}
}
