using UnityEngine;
using System.Collections;

public class ReturnToResultsPanelButton : UIGenericButton {
	
	public delegate void ReturnToResultsPanel();
	public static event ReturnToResultsPanel returnToResultsPanel;
	
	protected override void ActionOnClick (){
		returnToResultsPanel();
	}
}
