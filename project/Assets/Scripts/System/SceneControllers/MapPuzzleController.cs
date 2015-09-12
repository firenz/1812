using UnityEngine;
using System.Collections;

public class MapPuzzleController : MonoBehaviour {
	
	private void Start () {
		AudioManager.PlayMusic("Tranquility", true);
		CustomCursorController.Instance.ChangeIngameCursorToMenu();
		Inventory.Instance.Disable();
	}
}
