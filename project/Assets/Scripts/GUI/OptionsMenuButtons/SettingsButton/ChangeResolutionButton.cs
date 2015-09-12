using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeResolutionButton : UIGenericButton {
	[SerializeField]
	private int changeScreenWidth;
	[SerializeField]
	private int changeScreenHeight;

	public delegate void UpdatedScreenSize(int width, int height);
	public static event UpdatedScreenSize updatedScreenSize;

	protected override void Start(){
		base.Start();
		buttonText.text = changeScreenWidth + "x" + changeScreenHeight;
	}

	protected override void ActionOnClick (){
		GameState.ChangeScreenSettings(changeScreenWidth, changeScreenHeight, Screen.fullScreen);
		updatedScreenSize(changeScreenWidth, changeScreenHeight);
	}	
}
