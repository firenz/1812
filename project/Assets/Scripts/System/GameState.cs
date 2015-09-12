using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameState{
	public static string lastPlayedLevel;
	public static Vector2 lastPlayerPosition{
		get{
			switch(lastPlayedLevel){
			case "ProfessorOffice" :
				return LevelProfessorOfficeData.playerPosition;
			case "Corridor" :
				if(CutSceneData.isPlayedIntroCorridorPart == false){
					return Vector2.zero;
				}
				return LevelCorridorData.playerPosition;
				
			default:
				return Vector2.zero;
			}
		}

		private set {}
	}

	public static Vector2 lastTargetedPositionByMouse{
		get{
			switch(lastPlayedLevel){
			case "ProfessorOffice" :
				return LevelProfessorOfficeData.lastTargetedPositionByMouse;
			case "Corridor" :
				return LevelCorridorData.lastTargetedPositionByMouse;
				
			default:
				return Vector2.zero;
			}
		}

		private set {}
	}

	public static class SystemData{
		public struct ScreenSettings{
			public static int width;
			public static int height;
			public static bool fullscreen;
		}

		public struct AudioVolumeSettings{
			public static int music;
			public static int sfx;
		}

		public static bool isFirstTimeGameLaunched;
		public static string currentLanguage;
		public static ScreenSettings screenSettings;
	}
	
	public static class CutSceneData{
		public static bool isPlayedIntroCorridorPart;
		public static bool isPlayedIntroProfessorOfficePart;
		public static bool isPlayedCutsceneAfterPickedFlagsFromFloor;
		public static bool isPlayedCutsceneAfterMapPuzzleIsSolved;
		public static bool isPlayedCutsceneLibrarianMetAfterPuzzleIsSolved;
	}
	
	public static class LevelProfessorOfficeData{
		public static Vector2 playerPosition;
		public static Vector2 lastTargetedPositionByMouse;
		public static Vector2 defaultPosition;
		public static bool isWindowOpened;
		public static bool isLittleFlagsFellIntoFloor;
		public static bool isLittleFlagsPickedFromFloor;
	}
	
	public static class LevelCorridorData{
		public static Vector2 playerPosition;
		public static Vector2 lastTargetedPositionByMouse;
		public static Vector2 defaultPosition;
		public static int timesTrashBinWasExaminated;
		public static bool isDialogueChoiceMadeWithLibrarianFinished;
	}

	public static class MapPuzzleData{
		public static bool isMapPuzzleSolved;
	}
	
	public static void InitializeSettingsState(){
		SystemData.isFirstTimeGameLaunched = true;
		SystemData.ScreenSettings.width = Screen.width;
		SystemData.ScreenSettings.height = Screen.height;
		SystemData.ScreenSettings.fullscreen = Screen.fullScreen;
		SystemData.AudioVolumeSettings.music = 75;
		SystemData.AudioVolumeSettings.sfx = 75;
		SystemData.currentLanguage = "ES";
	}

	public static void InitializeGameState(){
		lastPlayedLevel = "MainMenu";

		CutSceneData.isPlayedIntroCorridorPart = false;
		CutSceneData.isPlayedIntroProfessorOfficePart = false;
		CutSceneData.isPlayedCutsceneAfterPickedFlagsFromFloor = false;
		CutSceneData.isPlayedCutsceneAfterMapPuzzleIsSolved = false;
		CutSceneData.isPlayedCutsceneLibrarianMetAfterPuzzleIsSolved = false;
		
		LevelCorridorData.playerPosition = new Vector2(228.5002f, 51.34025f);
		LevelCorridorData.defaultPosition = new Vector2(58.7f, 63.4f);
		LevelCorridorData.timesTrashBinWasExaminated = 0;
		LevelCorridorData.isDialogueChoiceMadeWithLibrarianFinished = false;
		
		LevelProfessorOfficeData.playerPosition = new Vector2(236.0f, 68.5f);
		LevelProfessorOfficeData.defaultPosition = LevelProfessorOfficeData.playerPosition;
		LevelProfessorOfficeData.isWindowOpened = false;
		LevelProfessorOfficeData.isLittleFlagsFellIntoFloor = false;
		LevelProfessorOfficeData.isLittleFlagsPickedFromFloor = false;
		
		MapPuzzleData.isMapPuzzleSolved = true;
	}

	public static void SaveGameState(bool isWarpedThroughMapsMenu = false){
		if(isWarpedThroughMapsMenu){
			switch(lastPlayedLevel){
			case "ProfessorOffice" :
				LevelProfessorOfficeData.playerPosition = LevelProfessorOfficeData.defaultPosition;
				LevelProfessorOfficeData.lastTargetedPositionByMouse = LevelProfessorOfficeData.defaultPosition;
	
				break;
			case "Corridor" :
				LevelCorridorData.playerPosition = LevelCorridorData.defaultPosition;
				LevelCorridorData.lastTargetedPositionByMouse = LevelProfessorOfficeData.defaultPosition;
				break;
			default:
				break;
			}
		}
		else{
			switch(Application.loadedLevelName){
			case "ProfessorOffice" :
				LevelProfessorOfficeData.playerPosition = Player.Instance.transform.position;
				LevelProfessorOfficeData.lastTargetedPositionByMouse = Player.Instance.originalTargetedPosition;
				break;
			case "Corridor" :
				LevelCorridorData.playerPosition = Player.Instance.transform.position;
				LevelCorridorData.lastTargetedPositionByMouse = Player.Instance.originalTargetedPosition;
				break;
			default:
				break;
			}
		}
	}

	public static void ChangeCurrentLanguage(string newLanguage){
		SystemData.currentLanguage = newLanguage;
		LocatedTextManager.ChangeCurrentLanguage(newLanguage);
		SettingsFileManager.Instance.SaveSettingsFile();
	}

	public static void ChangeScreenSettings(int newScreenWidth, int newScreenHeight, bool newFullscreen){
		SystemData.ScreenSettings.width = newScreenWidth;
		SystemData.ScreenSettings.height = newScreenHeight;
		SystemData.ScreenSettings.fullscreen = newFullscreen;
		Screen.SetResolution(newScreenWidth, newScreenHeight, newFullscreen); 
		SettingsFileManager.Instance.SaveSettingsFile();
	}
}
