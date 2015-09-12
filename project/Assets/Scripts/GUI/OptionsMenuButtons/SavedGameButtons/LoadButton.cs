using UnityEngine;
using System.Collections;

public class LoadButton : UIGenericButton {
	
	public delegate void LoadSlot();
	public static event LoadSlot loadSlot;
	
	protected override void ActionOnClick (){
		loadSlot();
    }
}
