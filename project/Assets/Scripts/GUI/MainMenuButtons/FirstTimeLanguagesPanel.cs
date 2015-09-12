using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class FirstTimeLanguagesPanel : MonoBehaviour {
	public int countLanguages { get; private set;}

	[SerializeField]
	private string pathLocalizationDirectories = "Data/Localization";
	[SerializeField]
	private string pathPrefabs = "Prefabs/UIElements/";
	[SerializeField]
	private Transform logoUCA;

	private string xmlLanguageMetaInfoIDPath = "/locatedtexts/meta/Language";
	private string[] directoriesPath;
	private List<string> languagesID;
	private Transform contentPanel;

	private void OnEnable(){
		ChangeCurrentLanguageButton.disableLanguagePanel += DisableLanguagePanel;
	}

	private void OnDisable(){
		ChangeCurrentLanguageButton.disableLanguagePanel -= DisableLanguagePanel;
	}

	private void Start() {
		if(GameState.SystemData.isFirstTimeGameLaunched){
			logoUCA.gameObject.SetActive(false);
			countLanguages = 0;
			languagesID = new List<string>();
			contentPanel = this.transform.FindChild("Panel");
			directoriesPath = FileManager.FindSubDirectories(pathLocalizationDirectories);

			FindLocalizedLanguages();
		}
		else{
			this.gameObject.SetActive(false);
			logoUCA.gameObject.SetActive(true);
		}
	}

	private void FindLocalizedLanguages(){
		foreach(string directory in directoriesPath){
			string _tmpLanguageID = directory.Replace("\\", "/");
			_tmpLanguageID = _tmpLanguageID.Replace(FileManager.path + "/Data/Localization/" , "");
			string _localizedTextPath = "Data/Localization/" +  _tmpLanguageID + "/LocatedTexts.xml";
			_localizedTextPath = _localizedTextPath.Replace("\\", "/");

			if(FileManager.CheckFile(_localizedTextPath)){
				languagesID.Add(_tmpLanguageID);

				XmlDocument _localizedTextDoc;
				XMLFileReader.OpenXMLFile(out _localizedTextDoc, Application.dataPath + "/" + _localizedTextPath);
				string _nodeText = XMLFileReader.GetNodeInfo(_localizedTextDoc, xmlLanguageMetaInfoIDPath);
				XMLFileReader.CloseXMLFile(out _localizedTextDoc);

				CreateLanguageButton(_nodeText, languagesID[countLanguages]);
				countLanguages++;
			}
		}
	}

	private void CreateLanguageButton(string languageName, string languageID){
		GameObject _newEntry = Instantiate(Resources.Load<GameObject>(pathPrefabs + "MainMenuLanguageButton"));
		_newEntry.name = "LanguageButton_"+ languageID;
		_newEntry.tag = "LanguageButton";
		_newEntry.transform.SetParent(contentPanel, false);
		_newEntry.GetComponent<ChangeCurrentLanguageButton>().Initialize(languageName, languageID);
	}

	private void DisableLanguagePanel(){
		GameState.SystemData.isFirstTimeGameLaunched = false;
		this.gameObject.SetActive(false);
		logoUCA.gameObject.SetActive(true);
	}
}
