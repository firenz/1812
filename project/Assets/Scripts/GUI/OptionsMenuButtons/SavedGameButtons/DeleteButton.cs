using UnityEngine;
using System.Collections;

public class DeleteButton : UIGenericButton {

	public delegate void DeleteSlot();
	public static event DeleteSlot deleteSlot;

	protected override void ActionOnClick (){
		deleteSlot();
	}
}
