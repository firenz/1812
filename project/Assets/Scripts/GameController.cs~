using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GameController : PersistentSingleton<GameController> {
	private static bool isGamePaused = false;

	//To be implemented
	//public delegate void EnableGameGUI();
	//public static event EnableGameGUI enableGameGUI;
	//public delegate void DisableGameGUI();
	//public static event DisableGameGUI disableGameGUI;
	//public delegate void PauseInteractiveElement();
	//public static event PauseInteractiveElement pauseInteractiveElement;
	//public delegate void UnPauseInteractiveElement();
	//public static event UnPauseInteractiveElement UnPauseInteractiveElement;

	protected override void InitializeOnAwake (){
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 1;

		//this.gameObject.AddComponent<ExtraScriptIfNeeded>();
		//...
		
		InitialGameState();
	}

	private void InitialGameState(){
		FileManager.InitializeDataPath();
		GameState.InitializeState();
		SettingsFileManager.Instance.LoadSettingsFile();
		GameState.ChangeScreenSettings(GameState.SystemData.ScreenSettings.width, GameState.SystemData.ScreenSettings.height, GameState.SystemData.ScreenSettings.fullscreen);
		LocalizedTextManager.Initialize();
		//...
	}	
	

	public static void WarpToLevel(string destinationLevel){
		GameState.SaveGameState();
		Application.LoadLevel(destinationLevel);
		GameState.LoadGameState(destinationLevel);
		//Inventory.Instance.UpdateAllItems();
	}

	public static string UnitySceneNameToSceneNameID(string unitySceneName){
		string _sceneNameID = "";
		
		switch(unitySceneName){
		case "DemoScene_00":
			_sceneNameID = "PROFESSOR_OFFICE";
			break;
		case "DemoScene_01":
			_sceneNameID = "CORRIDOR";
			break;
		}
		
		return _sceneNameID;
	}
	
	public static string SceneNameIDToUnitySceneName(string sceneNameID){
		string _unitySceneName = "";
		
		switch(sceneNameID){
		case "CORRIDOR":
                _unitySceneName = "DemoScene_01";
                break;
            case "PROFESSOR_OFFICE":
                _unitySceneName = "DemoScene_00";
                break;
        }
        
        return _unitySceneName;
	}

	public static void PauseEverything(){
		isGamePaused = true;
		Time.timeScale = 0; //To be changed or removed
	}

	public static void UnPauseEverything(){
		isGamePaused = false;
		Time.timeScale = 1; //To be changed or removed
	}

	public static bool IsGamePaused(){
		return isGamePaused;
	}
}