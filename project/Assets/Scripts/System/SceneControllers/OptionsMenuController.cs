using UnityEngine;
using System.Collections;

public class OptionsMenuController : MonoBehaviour {
	
	private void Start () {
		CustomCursorController.Instance.ChangeIngameCursorToMenu();
		Inventory.Instance.Disable();
		AudioManager.PlayMusic("Tranquility", true);
	}
}
