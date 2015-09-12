using UnityEngine;
using System.Collections;

public class FallenLittleFlag : MonoBehaviour {
	public PuzzleMapManager.FlagTypes currentFlagsType;

	public delegate void CheckPuzzleStateOnClick();
	public static event CheckPuzzleStateOnClick checkPuzzleOnClick;
	
	private GameObject flag;
	private PolygonCollider2D flagCollider;
	private PolygonCollider2D holeCollider;
	private SpriteRenderer flagFabricSprite;
	private string sfxOnClick;

	private void OnEnable(){
		PuzzleMapManager.puzzleCompleted += DisableCollidersFallenFlag;
	}

	private void OnDisable(){
		PuzzleMapManager.puzzleCompleted -= DisableCollidersFallenFlag;
	}
	
	private void Start () {
		flag = this.transform.FindChild("LittleFlag").gameObject;
		flagCollider = flag.GetComponent<PolygonCollider2D>();
		holeCollider = this.transform.FindChild("Hole").GetComponent<PolygonCollider2D>();
		flagFabricSprite = flag.transform.FindChild("Fabric").GetComponent<SpriteRenderer>();
		sfxOnClick = PuzzleMapManager.sfxOnClickFlag;

		ChangeTypeFlag(currentFlagsType);
	}

	public void OnMouseOver(){
		CustomCursorController.Instance.ChangeCursorOverUIElement();
	}

	public void OnMouseExit(){
		CustomCursorController.Instance.ChangeCursorToDefault();
	}

	public void OnMouseClick(){
		StartCoroutine(ActionOnMouseClick());
	}

	private IEnumerator ActionOnMouseClick(){
		AudioManager.PlaySFX(sfxOnClick);
		do{
			yield return null;
		}while(AudioManager.IsPlayingSFX(sfxOnClick));

		int _currentTypeFlagID = (int)currentFlagsType;
		int _nextTypeFlagID = (_currentTypeFlagID + 1) % PuzzleMapManager.maxFlagTypes;
		PuzzleMapManager.FlagTypes _nextTypeFlag = (PuzzleMapManager.FlagTypes)_nextTypeFlagID;
		
		ChangeTypeFlag(_nextTypeFlag);
		
		currentFlagsType = _nextTypeFlag;
		checkPuzzleOnClick();
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
	}
	
	private void SetFlagMode(Color newFlagColor){
		if(!flag.activeSelf){
			flag.SetActive(true);
		}
		flagFabricSprite.color = newFlagColor;
	}

	private void DisableCollidersFallenFlag(){
		flagCollider.enabled = false;
		holeCollider.enabled = false;
	}
}
