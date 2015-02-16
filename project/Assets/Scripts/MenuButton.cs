using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	/*
	void OnEnable(){
		GameStateManager.disableButton += DisableButton;
		GameStateManager.enableButton += EnableButton;
		CutScenesController.disableButton += DisableButton;
		CutScenesController.enableButton += EnableButton;
	}

	void OnDisable(){
		GameStateManager.disableButton -= DisableButton;
		GameStateManager.enableButton -= EnableButton;
		CutScenesController.disableButton -= DisableButton;
		CutScenesController.enableButton -= EnableButton;
	}
	*/

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnableButton(){
		this.transform.position = new Vector2(this.transform.position.x, 222f);
		//this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
		//this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
	}

	public void DisableButton(){
		this.transform.position = new Vector2(this.transform.position.x, 255f);
		//this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
		//this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
	}

	void OnMouseOver(){
		this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
		if(Input.GetMouseButtonDown(0)){
			//Application.LoadLevel("DemoScene_02");
			//GameStateManager.Instance.ChangeScene(Application.loadedLevel, 2);
			this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	void OnMouseExit(){
		this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
	}
}
