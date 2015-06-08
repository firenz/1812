using UnityEngine;
using System.Collections;

public static class ScaleAllGUI {

	public static float originalWidth = 640.0f;
	public static float originalHeight = 480.0f;
	private static Vector3 scale = new Vector3(0, 0, 0);

	static ScaleAllGUI(){
		scale.x = (float)Screen.width/originalWidth;
		scale.y = (float)Screen.height/originalHeight;
		scale.z = 1;
	}

	public static void ChangeResolution(){
		scale.x = (float)Screen.width/originalWidth;
		scale.y = (float)Screen.height/originalHeight;
	}

	public static int ScalateFontSize(int currentFontSize){
		float newFontSize = scale.x * currentFontSize;
		return (int)newFontSize;
	}

	public static Rect ScalateRect(Rect currentRect){
		Rect newRect = new Rect(currentRect.position.x, currentRect.position.y, currentRect.width * scale.x, currentRect.height * scale.y);
		return newRect;
	}

}
