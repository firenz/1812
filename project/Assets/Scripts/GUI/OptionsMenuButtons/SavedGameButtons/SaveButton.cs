using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveButton : UIGenericButton  {
	
	public delegate void SaveSlot();
	public static event SaveSlot saveSlot;
	
	protected override void ActionOnClick (){
		saveSlot();
    }
}
