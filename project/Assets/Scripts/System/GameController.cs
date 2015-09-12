using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : PersistentSingleton<GameController> {

	protected override void InitializeOnAwake (){
		Application.targetFrameRate = 30;
		QualitySettings.vSyncCount = 1;
		
		InitialGameState();
	}

	private void InitialGameState(){
		FileManager.InitializeDataPath();
		this.GetComponent<SettingsFileManager>().Initialize();
	}

	public static void WarpToLevel(string destinationLevel, bool isWarpedThroughMapsMenu = false){
		GameState.SaveGameState(isWarpedThroughMapsMenu);
		Application.LoadLevel(destinationLevel);
	}

	public static string UnitySceneNameToSceneNameID(string unitySceneName){
		string _sceneNameID = "";
		
		switch(unitySceneName){
		case "ProfessorOffice":
			_sceneNameID = "PROFESSOR_OFFICE";
			break;
		case "Corridor":
			_sceneNameID = "CORRIDOR";
			break;
		}
		
		return _sceneNameID;
	}
	
	public static string SceneNameIDToUnitySceneName(string sceneNameID){
		string _unitySceneName = "";
		
		switch(sceneNameID){
		case "CORRIDOR":
                _unitySceneName = "Corridor";
                break;
            case "PROFESSOR_OFFICE":
                _unitySceneName = "ProfessorOffice";
                break;
        }
        
        return _unitySceneName;
	}
}