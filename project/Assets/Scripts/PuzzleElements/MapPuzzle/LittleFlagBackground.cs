using UnityEngine;
using System.Collections;

public class LittleFlagBackground : MonoBehaviour {
	public PuzzleMapManager.FlagTypes currentFlagsType;

	private GameObject flag;
	private GameObject hole;
	private SpriteRenderer flagFabricSprite;

	private void Start() {
		flag = this.transform.FindChild("LittleFlag").gameObject;
		flagFabricSprite = flag.transform.FindChild("Fabric").GetComponent<SpriteRenderer>();
		hole = this.transform.FindChild("Hole").gameObject;
		ChangeTypeFlag(currentFlagsType);
	}

	private void ChangeTypeFlag(PuzzleMapManager.FlagTypes newFlagType){
		switch(newFlagType){
		case PuzzleMapManager.FlagTypes.noFlag:
			SetHoleMode();
			break;
		case PuzzleMapManager.FlagTypes.bayFlag:
			SetFlagMode(PuzzleMapManager.Instance.bayFlagColor);
			break;
		case PuzzleMapManager.FlagTypes.riverOrMarshlandFlag:
			SetFlagMode(PuzzleMapManager.Instance.riverOrMarshlandFlagColor);
			break;
		case PuzzleMapManager.FlagTypes.bridgeFrontAssedyWarFlag:
			SetFlagMode(PuzzleMapManager.Instance.bridgeFrontAssedyWarFlagColor);
			break;
		case PuzzleMapManager.FlagTypes.frenchAdvancedPartyFlag:
			SetFlagMode(PuzzleMapManager.Instance.frenchAdvancedPartyFlagColor);
			break;
		case PuzzleMapManager.FlagTypes.frenchBatteryFlag:
			SetFlagMode(PuzzleMapManager.Instance.frenchBatteryFlagColor);
			break;
		case PuzzleMapManager.FlagTypes.frenchFortificationFlag:
			SetFlagMode(PuzzleMapManager.Instance.frenchFortificationFlagColor);
			break;
		case PuzzleMapManager.FlagTypes.frenchMilitaryCampFlag:
			SetFlagMode(PuzzleMapManager.Instance.frenchMilitaryCampFlagColor);
			break;
		case PuzzleMapManager.FlagTypes.spanishBatteryFlag:
			SetFlagMode(PuzzleMapManager.Instance.spanishBatteryFlagColor);
			break;
		case PuzzleMapManager.FlagTypes.spanishFortificationFlag:
			SetFlagMode(PuzzleMapManager.Instance.spanishFortificationFlagColor);
			break;
		default:
			break;
		}
	}
	
	private void SetHoleMode(){
		flag.SetActive(false);
		if(!hole.activeSelf){
			hole.SetActive(true);
		}
	}
	
	private void SetFlagMode(Color newFlagColor){
		hole.SetActive(false);
		if(!flag.activeSelf){
			flag.SetActive(true);
		}
		flagFabricSprite.color = newFlagColor;
	}	
}
