using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GadipediaSearchResultElementButton : UIGenericButton {
	public string articleID { get; private set;}

	private Text briefArticleText;

	public delegate void ShowArticleContent(string ID);
	public static event ShowArticleContent showArticleContent;

	protected override void OnEnable(){
		base.OnEnable();
		ReturnToButton.resetUIPositions += DestroySearchElement;
		ReturnToSearchPanelButton.returnToSearchPanel += DestroySearchElement;
	}
	
	protected override void OnDisable(){
		base.OnDisable();
		ReturnToButton.resetUIPositions -= DestroySearchElement;
		ReturnToSearchPanelButton.returnToSearchPanel -= DestroySearchElement;
	}

	protected override void Start(){
		thisButton = this.GetComponent<Button>();
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
		briefArticleText = this.transform.FindChild("BriefText").GetComponent<Text>();
	}

	public void Initialize(string ID){
		Start();

		articleID = ID;
		try{
			buttonText.text = LocatedTextManager.GetLocatedText("GADIPEDIA_ARTICLES", ID, "NAME")[0];
			briefArticleText.text = LocatedTextManager.GetLocatedText("GADIPEDIA_ARTICLES", ID, "BRIEF")[0];
		}catch(NullReferenceException){
			buttonText.text = "Article name";
			briefArticleText.text = "Brief text";
		}
	}

	protected override void ActionOnClick (){
		showArticleContent(articleID);
	}

	private void DestroySearchElement(){
		Destroy(this.gameObject);
	}
}
