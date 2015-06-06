using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameMenuButton : UIGenericButton {
	private Sprite menuDisabled;
	private Sprite menuDisabledSelected;
	private Sprite menuEnabled;
	private Sprite menuEnabledSelected;
	private Sprite currentImage;
	private Button ingameMenuButton;
	private SpriteState buttonStatesEnabled;
	private SpriteState buttonStatesDisabled;
	private bool isInGameMenuButtonElegible = true;

	// Use this for initialization
	private void Start () {
		menuDisabled = Resources.Load<Sprite>("Graphics/GUI/MenuButtonDisabled");
		menuDisabledSelected = Resources.Load<Sprite>("Graphics/GUI/MenuButtonDisabledSelected");
		menuEnabled = Resources.Load<Sprite>("Graphics/GUI/MenuButtonUnselected");
		menuEnabledSelected = Resources.Load<Sprite>("Graphics/GUI/MenuButtonSelected");
		ingameMenuButton = this.GetComponent<Button>();
		buttonStatesEnabled = new SpriteState();
		buttonStatesEnabled.disabledSprite = menuDisabled;
		buttonStatesEnabled.highlightedSprite = menuEnabledSelected;
		buttonStatesEnabled.pressedSprite = menuEnabledSelected;
		buttonStatesDisabled = new SpriteState();
		buttonStatesEnabled.disabledSprite = menuDisabled;
		buttonStatesDisabled.highlightedSprite = menuDisabledSelected;
		buttonStatesDisabled.pressedSprite = menuDisabledSelected;
	}
	
	// Update is called once per frame
	protected override void Update(){
		base.Update();

		if(Player.Instance.isDoingAction || Player.Instance.isSpeaking || !MultipleChoiceManager.Instance.IsSelectionEnded() || CutScenesManager.IsPlaying()){
			ingameMenuButton.image.sprite = menuDisabled;
			ingameMenuButton.spriteState = buttonStatesDisabled;
			isInGameMenuButtonElegible = false;
		}
		else{
			ingameMenuButton.image.sprite = menuEnabled;
			ingameMenuButton.spriteState = buttonStatesEnabled;
			isInGameMenuButtonElegible = true;
		}
	}

	public void OnMouseClick(){
		if(isInGameMenuButtonElegible){
			GameController.WarpToLevel("DemoScene_02");
		}
	}
}
