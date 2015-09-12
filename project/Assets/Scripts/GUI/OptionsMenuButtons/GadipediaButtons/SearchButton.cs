using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SearchButton : UIGenericButton {

	public delegate void CalculateResults();
	public static event CalculateResults calculateResultsOnClick;

	protected override void OnEnable(){
		base.OnEnable();
		GadipediaSearchManager.beginSearchResults += DisableButton;
		GadipediaSearchManager.endSearchResults += EnableButton;
	}
	
	protected override void OnDisable(){
		base.OnDisable();
		GadipediaSearchManager.beginSearchResults -= DisableButton;
		GadipediaSearchManager.endSearchResults -= EnableButton;
	}

	protected override void ActionOnClick (){
		calculateResultsOnClick();
	}


}
