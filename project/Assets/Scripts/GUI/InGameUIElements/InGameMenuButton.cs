using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameMenuButton : UIGenericButton {
	[SerializeField]
	private string pathGraphics = "Graphics/GUI/";
	[SerializeField]
	private string nameOptionsMenuScene = "OptionsMenu";
	
	private Sprite menuDisabled;
	private Sprite menuDisabledSelected;
	private Sprite menuEnabled;
	private Sprite menuEnabledSelected;
	private Sprite currentImage;
	private SpriteState buttonStatesEnabled;
	private SpriteState buttonStatesDisabled;
	
	protected override void Start () {
		base.Start();

		menuDisabled = Resources.Load<Sprite>(pathGraphics + "MenuButtonDisabled");
		menuDisabledSelected = Resources.Load<Sprite>(pathGraphics + "MenuButtonDisabledSelected");
		menuEnabled = Resources.Load<Sprite>(pathGraphics + "MenuButtonUnselected");
		menuEnabledSelected = Resources.Load<Sprite>(pathGraphics + "MenuButtonSelected");

		buttonStatesEnabled = new SpriteState();
		buttonStatesDisabled = new SpriteState();

		buttonStatesEnabled.disabledSprite = menuDisabled;
		buttonStatesEnabled.highlightedSprite = menuEnabledSelected;
		buttonStatesEnabled.pressedSprite = menuEnabledSelected;
		buttonStatesEnabled.disabledSprite = menuDisabled;
		buttonStatesDisabled.highlightedSprite = menuDisabledSelected;
		buttonStatesDisabled.pressedSprite = menuDisabledSelected;
	}

	private void Update(){
		if(!CutScenesManager.IsPlaying()){
			if(!this.GetComponent<Image>().enabled){
				UnhideButton();
			}
			if(!Player.Instance.isDoingAction && !Player.Instance.isUsingItemInventory && !CutScenesManager.IsPlaying()){
				EnableButton();
			}
			else{
				DisableButton();
			}
		}
		else{
			if(this.GetComponent<Image>().enabled){
				HideButton();
			}
		}
	}

	protected override void ActionOnClick(){
		GameController.WarpToLevel(nameOptionsMenuScene);
	}

	public override void EnableButton (){
		base.EnableButton ();
		thisButton.image.sprite = menuEnabled;
		thisButton.spriteState = buttonStatesEnabled;
	}

	public override void DisableButton (){
		base.DisableButton ();
		thisButton.image.sprite = menuDisabled;
		thisButton.spriteState = buttonStatesDisabled;
	}

	private void UnhideButton(){
		this.GetComponent<Image>().enabled = true;
	}

	private void HideButton(){
		this.GetComponent<Image>().enabled = false;
	}
}
