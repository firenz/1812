using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GadipediaEntry : MonoBehaviour {
	public string currentID { get; private set;}

	private Text articleContentText;
	private Text articleNameLabel;
	private Transform articleEntryPanel;
	private UIGenericScrollbar entryScrollbar;

	private void OnEnable(){
		GadipediaSearchResultElementButton.showArticleContent += ShowEntry;
		ReturnToButton.resetUIPositions += ResetEntry;
		ReturnToResultsPanelButton.returnToResultsPanel += ResetEntry;
	}

	private void OnDisable(){
		GadipediaSearchResultElementButton.showArticleContent -= ShowEntry;
		ReturnToButton.resetUIPositions -= ResetEntry;
		ReturnToResultsPanelButton.returnToResultsPanel -= ResetEntry;
	}
	
	private void Start () {
		currentID = "";
		articleEntryPanel = this.transform.FindChild("ArticlePanel");
		articleNameLabel = articleEntryPanel.FindChild("EntryLabel").GetComponent<Text>();
		articleContentText = articleEntryPanel.FindChild("ArticleText/ScrollView/Text").GetComponent<Text>();
		entryScrollbar = articleEntryPanel.FindChild("ArticleText/Scrollbar").GetComponent<UIGenericScrollbar>();

		articleNameLabel.text = "";
		articleContentText.text = "";
		articleEntryPanel.SetAsFirstSibling();
	}

	public void ShowEntry(string ID){
		List<string> _articleContent = new List<string>();
		string _finalArticleContentText = "";
		currentID = ID;
		articleNameLabel.text = LocatedTextManager.GetLocatedText("GADIPEDIA_ARTICLES", ID, "NAME")[0];
		_articleContent = LocatedTextManager.GetLocatedText("GADIPEDIA_ARTICLES", ID, "CONTENT");

		foreach(string line in _articleContent){
			_finalArticleContentText += line + "\n";
		}

		articleContentText.text = _finalArticleContentText;
		int _countCharactersArticleContent = _finalArticleContentText.Length;

		if(_countCharactersArticleContent < 182){
			entryScrollbar.gameObject.SetActive(false);
		}
		else{
			entryScrollbar.ResetPosition();
		}

		articleEntryPanel.SetAsLastSibling();
	}

	private void ResetEntry(){
		currentID = "";
		articleEntryPanel.SetAsFirstSibling();
		articleNameLabel.text = "";
		articleContentText.text = "";
		entryScrollbar.gameObject.SetActive(true);
		entryScrollbar.ResetPosition();
	}
}
