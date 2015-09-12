using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Mono.Data.SqliteClient;

public class GadipediaSearchManager : MonoBehaviour {
	private string dbPath = "/Data/DB/gadipedia.db";
	private string dbSource;
	private IDbConnection dbConnection;
	private IDbCommand dbCommand;
	private IDataReader dbReader;
	private List<string> foundedIDs;
	private Dictionary<string, int> articleIDs;
	private bool isSearchinInDB = false;
	
	private InputField searchInputField;
	private string defaulfInputFieldText;
	private Transform searchMenu;
	private Transform searchingBlock;

	public delegate void BeginSearchResults();
	public static event BeginSearchResults beginSearchResults;
	public delegate void EndSearchResults();
	public static event EndSearchResults endSearchResults;
	public delegate void CreateArticleResultElement(string articleID);
	public static event CreateArticleResultElement createArticle;

	private void OnEnable(){
		SearchButton.calculateResultsOnClick += SearchForResults;
		ReturnToButton.resetUIPositions += ResetSearchPanel;
		ReturnToSearchPanelButton.returnToSearchPanel += PutFrontSearchPanel;
		LocatedTextManager.currentLanguageChanged += ChangeLanguage;
	}

	private void OnDisable(){
		SearchButton.calculateResultsOnClick -= SearchForResults;
		ReturnToButton.resetUIPositions -= ResetSearchPanel;
		ReturnToSearchPanelButton.returnToSearchPanel -= PutFrontSearchPanel;
		LocatedTextManager.currentLanguageChanged -= ChangeLanguage;
	}

	private void OnApplicationQuit(){
		CloseDBConnection();
    }

	private void OnDestroy(){
		CloseDBConnection();
	}

	private void Start () {
		dbSource = "URI=file:" + Application.dataPath + dbPath + ",version=3";
		OpenDBConnection();
		foundedIDs = new List<string>();
		articleIDs = new Dictionary<string, int>();

		searchMenu = this.transform.FindChild("SearchPanel");
		searchingBlock = this.transform.FindChild("SearchingBlock");
		searchInputField = searchMenu.FindChild("InputField").GetComponent<InputField>();
		defaulfInputFieldText = LocatedTextManager.GetLocatedText("OPTIONS_MENU", "GADIPEDIA", "INPUT_FIELD")[0];
		searchInputField.text = defaulfInputFieldText;
	}

	public void SearchForResults(){
		CustomCursorController.Instance.ChangeCursorToDefault();
		StartCoroutine(WaitForResults());
	}

	private IEnumerator WaitForResults(){
		string _searchText = searchInputField.text;
		_searchText = _searchText.Trim();

		if(_searchText != ""){
			string _lowercase = _searchText.ToLower();
			string _noAccents = RemoveDiacritics(_lowercase);
			string[] _keywords = _noAccents.Split(new char[] {' ', ',', '.', ':', '\t', '\n'});
			
			if(_keywords.Length != 0){
				beginSearchResults();
				searchingBlock.SetAsLastSibling();

				foreach(string keyword in _keywords){
					StartCoroutine(FindArticleIDs(keyword));
					do{
						yield return null;
					}while(isSearchinInDB);
				}

				foreach(string ID in foundedIDs){
					if(articleIDs.ContainsKey(ID)){
						int _counter = articleIDs[ID];
						_counter++;
						articleIDs[ID] = _counter;
					}
					else{
						articleIDs.Add(ID, 1);
					}
				}
                
                foreach(KeyValuePair<string, int> article in articleIDs.OrderByDescending(i => i.Value)){
                    CreateSearchResultElement(article.Key);
					yield return new WaitForSeconds(0.1f);
                }

				endSearchResults();
				searchingBlock.SetAsFirstSibling();
            }

			articleIDs.Clear();
			foundedIDs.Clear();
            ShowResultsPanel();
        }
	}

	private IEnumerator FindArticleIDs(string keyword){
		isSearchinInDB = true;

		string _keywordID = LocatedTextManager.GetGadipediaKeywordID(keyword);
		dbCommand = dbConnection.CreateCommand();
		dbCommand.CommandText = "SELECT article_id FROM keywords_search WHERE keyword='" + _keywordID + "'";
		dbReader = dbCommand.ExecuteReader();
		yield return new WaitForSeconds(0.2f);

		while(dbReader.Read()){
			foundedIDs.Add(dbReader.GetString(0));
			yield return new WaitForSeconds(0.1f);
        }
        
		CloseDBReader();
        yield return new WaitForSeconds(0.2f);
		isSearchinInDB = false;
    }
	
	private string RemoveDiacritics(string text){
		string _normalizedString = text.Normalize(NormalizationForm.FormD);
		StringBuilder _stringBuilder = new StringBuilder();
		
		foreach (char character in _normalizedString){
			UnicodeCategory _unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);
			if (_unicodeCategory != UnicodeCategory.NonSpacingMark){
				_stringBuilder.Append(character);
			}
		}
		
		return _stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

	private void OpenDBConnection(){
		CloseDBConnection();

		dbConnection = (IDbConnection) new SqliteConnection(dbSource);
		dbConnection.Open();
	}

	private void CloseDBReader(){
		if(dbReader != null){
			dbReader.Close();
			dbReader = null;
		}
		if(dbCommand != null){
			dbCommand.Dispose();
			dbCommand = null;
		}
	}

	private void CloseDBConnection(){
		CloseDBReader();

        if(dbConnection != null){
            dbConnection.Close();
            dbConnection = null;
        }
    }

	private void CreateSearchResultElement(string id){
		createArticle(id);
	}

	private void ShowResultsPanel(){
		searchMenu.SetAsFirstSibling();
		this.GetComponent<GadipediaResultsManager>().ShowPanel();
		searchInputField.text = defaulfInputFieldText;
	}

	private void PutFrontSearchPanel(){
		searchMenu.SetAsLastSibling();
	}

	private void ResetSearchPanel(){
		searchInputField.text = defaulfInputFieldText;
		searchMenu.SetAsLastSibling();
		searchingBlock.SetAsFirstSibling();
	}

	private void ChangeLanguage(){
		defaulfInputFieldText = LocatedTextManager.GetLocatedText("OPTIONS_MENU", "GADIPEDIA", "INPUT_FIELD")[0];
		searchInputField.text = defaulfInputFieldText;
	}
}
