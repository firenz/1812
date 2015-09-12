using UnityEngine;
using System.Collections;

public class ReturnToSearchPanelButton : UIGenericButton {

	public delegate void ReturnToSearchPanel();
	public static event ReturnToSearchPanel returnToSearchPanel;

	protected override void ActionOnClick (){
		returnToSearchPanel();
	}
}
