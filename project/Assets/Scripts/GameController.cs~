using UnityEngine;
using UnityEditor;
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
		GameState.InitializeState();
		LocalizedTextManager.Initialize();
		//...
	}


	public static void WarpToLevel(string destinationLevel){
		GameState.SaveGameState();
		Application.LoadLevel(destinationLevel);
		GameState.LoadGameState(destinationLevel);
	}

	public static void WarpToLevelFromMenu(string destinationLevel){
		Application.LoadLevel(destinationLevel);
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