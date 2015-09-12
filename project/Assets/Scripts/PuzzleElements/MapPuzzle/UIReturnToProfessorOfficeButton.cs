using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIReturnToProfessorOfficeButton : UIGenericButton {
	[SerializeField]
	private string sceneProfessorOffice = "ProfessorOffice";

	protected override void ActionOnClick (){
		GameController.WarpToLevel(sceneProfessorOffice);
	}
}
