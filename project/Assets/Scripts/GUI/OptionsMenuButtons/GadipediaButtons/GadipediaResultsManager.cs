using UnityEngine;
using System.Collections;

public class GadipediaResultsManager : MonoBehaviour {
	[SerializeField]
	private string pathPrefab = "Prefabs/UIElements/";

	private const int maxArticlesInPanelWithoutScrollbar = 1;

	private GameObject resultsPanel;
	private GameObject resultsEntriesPanel;
	private Transform contentPanel;
	private UIGenericScrollbar resultsPanelScrollbar;

	private void OnEnable(){
		GadipediaSearchManager.createArticle += CreateSearchResultElement;
		ReturnToButton.resetUIPositions += ResetResultsPanel;
		ReturnToSearchPanelButton.returnToSearchPanel += ResetResultsPanel;
	}
	
	private void OnDisable(){
		GadipediaSearchManager.createArticle -= CreateSearchResultElement;
		ReturnToButton.resetUIPositions -= ResetResultsPanel;
		ReturnToSearchPanelButton.returnToSearchPanel -= ResetResultsPanel;
	}
	
	private void Start () {
		resultsPanel = this.transform.FindChild("ResultsPanel").gameObject;
		resultsEntriesPanel = resultsPanel.transform.FindChild("GadipediaEntriesPanel").gameObject;
		contentPanel = resultsEntriesPanel.transform.FindChild("ScrollView/ContentPanel");
		resultsPanelScrollbar = resultsPanel.transform.FindChild("GadipediaEntriesPanel/Scrollbar").GetComponent<UIGenericScrollbar>();

		ResetResultsPanel();
	}

	private void CreateSearchResultElement(string ID){
		GameObject _newArticle = Instantiate(Resources.Load<GameObject>(pathPrefab + "GadipediaSearchResultElementButton"));
		_newArticle.name = "Article_" + ID;
		_newArticle.transform.SetParent(contentPanel, false);
		_newArticle.GetComponent<GadipediaSearchResultElementButton>().Initialize(ID);
	}

	public void ShowPanel(){
		int _countSearchResults = contentPanel.transform.childCount;

		ResetResultsPanel();

		if(_countSearchResults <= maxArticlesInPanelWithoutScrollbar){
			resultsPanelScrollbar.gameObject.SetActive(false);
		}

		if(_countSearchResults == 0){
			resultsEntriesPanel.SetActive(false);
		}

		resultsPanel.transform.SetAsLastSibling();
	}

	public void ResetResultsPanel(){
		resultsPanel.transform.SetAsFirstSibling();
		resultsEntriesPanel.SetActive(true);
		resultsPanelScrollbar.gameObject.SetActive(true);
		resultsPanelScrollbar.ResetPosition();
	}
}
