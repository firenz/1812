using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeResolutionButton : UIGenericButton {
	public int changeScreenWidth;
	public int changeScreenHeight;

	public delegate void DisableResolutionPanel();
	public static event DisableResolutionPanel disableResolutionPanel;

	// Use this for initialization
	private void Start(){
		string _changeResolution = changeScreenWidth + "x" + changeScreenHeight;
		this.transform.FindChild("Text").GetComponent<Text>().text = _changeResolution;
	}

	public void OnClick(){
		Screen.SetResolution(changeScreenWidth, changeScreenHeight, Screen.fullScreen);

		//To be implemented Modal Window with countdown asking if the resolution chosen is correct

		GameState.ChangeScreenSettings(changeScreenWidth, changeScreenHeight, Screen.fullScreen);
		GameObject.Find("CurrentResolutionButton").GetComponent<CurrentResolutionButton>().ChangeDisplayingResolution(changeScreenWidth, changeScreenHeight);
		SettingsFileManager.Instance.SaveSettingsFile();
		disableResolutionPanel();
	}

}
