using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInfoFlagType : MonoBehaviour {
	[SerializeField]
	private PuzzleMapManager.FlagTypes currentInfoFlagType;

	private Image infoColor;
	private Text infoText;

	private void Start () {
		infoColor = this.transform.FindChild("Image").GetComponent<Image>();
		infoText = this.transform.FindChild("Text").GetComponent<Text>();
		ChangeInfoType(currentInfoFlagType);
	}

	private void ChangeInfoType(PuzzleMapManager.FlagTypes infoFlagType){
		switch(infoFlagType){
		case PuzzleMapManager.FlagTypes.bayFlag:
			SetInfoType(PuzzleMapManager.Instance.bayFlagColor, "BAY");
			break;
		case PuzzleMapManager.FlagTypes.riverOrMarshlandFlag:
			SetInfoType(PuzzleMapManager.Instance.riverOrMarshlandFlagColor, "MARSHLAND");
			break;
		case PuzzleMapManager.FlagTypes.bridgeFrontAssedyWarFlag:
			SetInfoType(PuzzleMapManager.Instance.bridgeFrontAssedyWarFlagColor, "BRIDGE");
			break;
		case PuzzleMapManager.FlagTypes.frenchAdvancedPartyFlag:
			SetInfoType(PuzzleMapManager.Instance.frenchAdvancedPartyFlagColor, "FRENCH_ADVANCE");
			break;
		case PuzzleMapManager.FlagTypes.frenchBatteryFlag:
			SetInfoType(PuzzleMapManager.Instance.frenchBatteryFlagColor, "FRENCH_BATTERY");
			break;
		case PuzzleMapManager.FlagTypes.frenchFortificationFlag:
			SetInfoType(PuzzleMapManager.Instance.frenchFortificationFlagColor, "FRENCH_FORTIFICATION");
			break;
		case PuzzleMapManager.FlagTypes.frenchMilitaryCampFlag:
			SetInfoType(PuzzleMapManager.Instance.frenchMilitaryCampFlagColor, "FRENCH_CAMP");
			break;
		case PuzzleMapManager.FlagTypes.spanishBatteryFlag:
			SetInfoType(PuzzleMapManager.Instance.spanishBatteryFlagColor, "SPANISH_BATTERY");
			break;
		case PuzzleMapManager.FlagTypes.spanishFortificationFlag:
			SetInfoType(PuzzleMapManager.Instance.spanishFortificationFlagColor, "SPANISH_FORTIFICATION");
			break;
		default:
			break;
		}
	}

	private void SetInfoType(Color color, string infoID){
		infoColor.color = color;
		infoText.text = LocatedTextManager.GetLocatedText("SCENE_MAP_PUZZLE", "MAP_INFO", infoID)[0];
	}
}
