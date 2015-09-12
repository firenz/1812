using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewGameMainMenuButton : UIGenericButton {

	protected override void ActionOnClick() {
		GameState.InitializeGameState();
		GameController.WarpToLevel("Corridor");
	}	
}
