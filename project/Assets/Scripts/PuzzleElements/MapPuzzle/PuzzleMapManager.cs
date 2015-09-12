using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class PuzzleMapManager : Singleton<PuzzleMapManager> {

	public enum FlagTypes{
		noFlag,
		bayFlag,
		riverOrMarshlandFlag,
		spanishBatteryFlag,
		spanishFortificationFlag,
		frenchAdvancedPartyFlag,
		frenchBatteryFlag,
		frenchFortificationFlag,
		frenchMilitaryCampFlag,
		bridgeFrontAssedyWarFlag
	}

	[SerializeField]
	public Color bayFlagColor = Color.cyan;
	[SerializeField]
	public Color riverOrMarshlandFlagColor = Color.green;
	[SerializeField]
	public Color spanishBatteryFlagColor = Color.red;
	[SerializeField]
	public Color spanishFortificationFlagColor = Color.grey;
	[SerializeField]
	public Color frenchAdvancedPartyFlagColor = Color.red;
	[SerializeField]
	public Color frenchBatteryFlagColor = Color.blue;
	[SerializeField]
	public Color frenchFortificationFlagColor = Color.gray;
	[SerializeField]
	public Color frenchMilitaryCampFlagColor = Color.cyan;
	[SerializeField]
	public Color bridgeFrontAssedyWarFlagColor = Color.yellow;

	public static int maxFlagTypes { get; private set;}
	public static string sfxOnClickFlag = "BlipSelect";

	private FallenLittleFlag flag0;
	private FallenLittleFlag flag1;
	private FallenLittleFlag flag2;
	private FallenLittleFlag flag3;
	private FallenLittleFlag flag4;
	private FallenLittleFlag flag5;

	public delegate void PuzzleCompleted();
	public static event PuzzleCompleted puzzleCompleted;

	private void OnEnable(){
		FallenLittleFlag.checkPuzzleOnClick += CheckPuzzleState;
	}

	private void OnDisable(){
		FallenLittleFlag.checkPuzzleOnClick -= CheckPuzzleState;
	}

	protected override void InitializeOnAwake (){
		maxFlagTypes = Enum.GetNames(typeof(FlagTypes)).Length;
	}

	private void Start(){
		flag0 = GameObject.Find("Flag0").GetComponent<FallenLittleFlag>();
		flag1 = GameObject.Find("Flag1").GetComponent<FallenLittleFlag>();
		flag2 = GameObject.Find("Flag2").GetComponent<FallenLittleFlag>();
		flag3 = GameObject.Find("Flag3").GetComponent<FallenLittleFlag>();
		flag4 = GameObject.Find("Flag4").GetComponent<FallenLittleFlag>();
		flag5 = GameObject.Find("Flag5").GetComponent<FallenLittleFlag>();
	}

	private void Update(){
		CustomCursorController.Instance.ChangeCursorToDefault();
		FallenLittleFlag _littleFlagOverMouse = CheckIfMouseIsOverFallenLittleFlag();

		if(_littleFlagOverMouse != null){

			_littleFlagOverMouse.OnMouseOver();

			if(Input.GetMouseButtonDown(0)){
				_littleFlagOverMouse.OnMouseClick();
			}
			_littleFlagOverMouse = null;
		}
		else{
			if(EventSystem.current.IsPointerOverGameObject()){
				CustomCursorController.Instance.ChangeCursorOverUIElement();
			}
		}
	}

	private void CheckPuzzleState(){
		if(flag0.currentFlagsType == FlagTypes.spanishFortificationFlag 
		   && flag1.currentFlagsType == FlagTypes.spanishBatteryFlag
		   && flag2.currentFlagsType == FlagTypes.bridgeFrontAssedyWarFlag
		   && flag3.currentFlagsType == FlagTypes.frenchFortificationFlag
		   && flag4.currentFlagsType == FlagTypes.frenchMilitaryCampFlag
		   && flag5.currentFlagsType == FlagTypes.frenchAdvancedPartyFlag){

			GameState.MapPuzzleData.isMapPuzzleSolved = true;
			StartCoroutine(ExitMapPuzzle());
		}
	}

	private IEnumerator ExitMapPuzzle(){
		AudioManager.StopAllMusic();
		GameObject.Find("ReturnToProfessorOfficeFromMapPuzzleButton").SetActive(false);
		GameObject.Find("MapInfoPanel").transform.SetAsFirstSibling();
		CustomCursorController.Instance.HideCursor();
		yield return new WaitForSeconds(1.5f);
		puzzleCompleted();
	}

	private FallenLittleFlag CheckIfMouseIsOverFallenLittleFlag(){
		Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D _hit = new RaycastHit2D();
		int _puzzleElementsLayerID = LayerMask.NameToLayer("PuzzleElements"); //Obtaining the layer ID of PuzzleElements Layer
		int _puzzleElementsMask = 1 << _puzzleElementsLayerID; // This is used to generate the Mask derived from PuzzleElementns Layer
		_hit = Physics2D.Raycast(_mousePosition, Vector2.zero, Mathf.Infinity, _puzzleElementsMask, 100f, -100f);
		FallenLittleFlag _littleFlag = null;

		if(_hit.collider != null){
			if(_hit.collider.transform.parent.tag == "LittleFlagsPuzzle"){
				try{
					_littleFlag = _hit.collider.GetComponentInParent<FallenLittleFlag>();
				}catch(NullReferenceException){
					_littleFlag = null;
				}
			}
		}

		return _littleFlag;
	}
}
