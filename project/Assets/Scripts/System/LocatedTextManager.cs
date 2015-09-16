using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

/// <summary>
/// Clase estatica encargada de obtener los textos del videojuego contenidos en los archivos <c>LocatedTexts.xml</c>.
/// <seealso cref="XMLFileReader">Para saber como se gestionan los documentos XML y su lectura/escritura.</seealso>
/// </summary>
public static class LocatedTextManager {
	/// <summary>
	/// ID del idioma actual en tiempo de ejecucion.
	/// </summary>
	private static string currentLanguage;

	/// <summary>
	/// Atributo que guarda el documento XML abierto actualmente para trabajar con el usando los metodos de esta clase.
	/// </summary>
	private static XmlDocument locatedTextDoc;

	/// <summary>
	/// Metodo delegado para cuando se cambia el idioma actual del juego.
	/// </summary>
	public delegate void CurrentLanguageChanged();

	/// <summary>
	/// Evento que llama al metodo delegado <c>LocatedTextManager.CurrentLanguageChanged</c> para que se ejecute.
	/// </summary>
	public static event CurrentLanguageChanged currentLanguageChanged;

	/// <summary>
	/// Al cerrarse la aplicacion, se cierra el documento XML abierto contenido en <c>LocatedTextManager.locatedTextDoc</c>
	/// para evitar que siga abierto en memoria.
	/// </summary>
	private static void OnApplicationQuit(){
		XMLFileReader.CloseXMLFile(out locatedTextDoc);
	}

	/// <summary>
	/// Metodo para inicializar el documento XML con los textos localizados del idioma del juego actual.
	/// </summary>
	public static void Initialize() {
		//Get current language from the saved state of the game preferences
		currentLanguage = GameState.SystemData.currentLanguage;

		//This line is for loading the localized XMLs from an external folder
		//(after creating a build and adding the folder with XMLs manually)
		//(used when project is finished for easing work to the people who wants to make his owns localized texts)
		string _xmlPath = Application.dataPath + "/Data/Localization/" + currentLanguage + "/LocatedTexts.xml";

		//Then xml in path is loaded
		locatedTextDoc = new XmlDocument();
		XMLFileReader.OpenXMLFile(out locatedTextDoc, _xmlPath);

	}

	/// <summary>
	/// Metodo para obtener textos localizados especificos del documento XML.
	/// </summary>
	/// <returns>El texto localizado.</returns>
	/// <param name="groupId">ID del grupo al que pertenece el texto localizado que queremos obtener.</param>
	/// <param name="elementId">ID del elemento al que pertenece el texto localizado que queremos obtener.</param>
	/// <param name="textId">ID del texto especifico al que pertenece el texto localizado que queremos obtener.</param>
	public static List<string> GetLocatedText(string groupId , string elementId, string textId){
		string _xmlPathToNode = "/locatedtexts/group[@id='" + groupId + "']/element[@id='" + elementId + "']/string[@id='" + textId + "']";
		string _rawText = XMLFileReader.GetNodeInfo(locatedTextDoc, _xmlPathToNode);

		_rawText = _rawText.Replace("\t", "");
		_rawText = _rawText.Replace("\r", "");
		_rawText = _rawText.TrimStart(' ', '\n');
		_rawText = _rawText.TrimEnd(' ', '\n');
		List<string> _locatedText = _rawText.Split('\n').Select(p => p.Trim()).ToList();

		return _locatedText;
	}

	/// <summary>
	/// Metodo para obtener textos localizados espeficos del documento XML pero que esten formateados de
	/// forma que haya un maximo de caracteres por linea, y un maximo de lineas de texto para cada
	/// cadena de caracteres que pertenezca a la lista a devolver.
	/// </summary>
	/// <returns>Texto localizado en cadenas de caracteres con caracteres y saltos de linea predefinidos, contenidos
	/// a su vez en una lista de cadenas.</returns>
	/// <param name="groupID">ID del grupo al que pertenece el texto localizado que queremos obtener.</param>
	/// <param name="elementID">ID del elemento al que pertenece el texto localizado que queremos obtener.</param>
	/// <param name="textID">ID del texto especifico al que pertenece el texto localizado que queremos obtener.</param>
	/// <param name="maxCharsPerLine">Maximo de caracteres por linea.</param>
	/// <param name="maxDisplayingLines">Maximo numero de saltos de linea por cadena de texto.</param>
	public static List<string> GetLocatedTextFormatted(string groupID, string elementID, string textID, int maxCharsPerLine = 0, int maxDisplayingLines = 0){
		List<string> _locatedText = GetLocatedText(groupID, elementID, textID);
		List<string> _resultingText = new List<string>();
		string _resultingString = "";

		if(maxCharsPerLine == 0 && maxDisplayingLines == 0){
			_resultingText = _locatedText;
			return _resultingText;
		}

		for(int i = 0; i < _locatedText.Count; i++){
			string _currentLine = _locatedText[i];
			_currentLine = _currentLine.Trim(' ', '\r','\t', '\n');
			List<string> _wordsInLine = _currentLine.Split(' ').ToList();
			int _currentWordsLength = 0;
			int _nextWordLength = 0;
			int _currentNewLine = 0;
			int _lastIndexInLine = _wordsInLine.Count -1;
			_resultingString = "";
            
			for(int j = 0; j < _wordsInLine.Count; j++){
				string _nextWord = _wordsInLine[j];
				_nextWordLength = _nextWord.Length;

				if(_currentNewLine < maxDisplayingLines){
					if((_currentWordsLength + _nextWordLength) <= maxCharsPerLine){
						if(j != _lastIndexInLine){
							_resultingString += _nextWord + " ";	
							_currentWordsLength += _nextWordLength + 1;
						}
						else{
							_resultingString += _nextWord;
                            _resultingText.Add(_resultingString);
                            _currentNewLine = 0;
                            _currentWordsLength = 0;
                            _resultingString = "";
                        }
					}
					else{
						if(j != _lastIndexInLine){
							_resultingString += "\n" + _nextWord + " ";
							_currentWordsLength = _nextWordLength + 1;
                            _currentNewLine++;
						}
						else{
							_resultingString += "\n" + _nextWord;
							_resultingText.Add(_resultingString);
							_currentWordsLength = 0;
                            _currentNewLine = 0;
							_resultingString = "";
                        }
                    }
                }
				else{
					if((_currentWordsLength + _nextWordLength) <= maxCharsPerLine){
						if(j != _lastIndexInLine){
							_resultingString += _nextWord + " ";	
							_currentWordsLength += _nextWordLength + 1;
						}
						else{
							_resultingString += _nextWord;
							_resultingText.Add(_resultingString);
							_currentNewLine = 0;
							_currentWordsLength = 0;
							_resultingString = "";
						}
					}
					else{
						if(j != _lastIndexInLine){
							_resultingText.Add(_resultingString);
							_resultingString = _nextWord + " ";
							_currentWordsLength = _nextWordLength + 1;
							_currentNewLine = 0;
						}
						else{
							_resultingText.Add(_resultingString);
							_resultingText.Add(_nextWord);
							_resultingString = "";
							_currentWordsLength = 0;
                            _currentNewLine = 0;
						}
                    }
                }
            }

            _wordsInLine.Clear();
        }

        return _resultingText;
    }
    
    /// <summary>
    /// Obtiene el numero de nodos hijo <c>string</c> que hay en el documento XML para el ID de grupo y elemento dados.
	/// </summary>
	/// <seealso cref="">Usado para saber cuantas opciones hay en el sistema de elecciones de respuesta.</seealso>
    /// <returns>Numero de elementos.</returns>
    /// <param name="groupId">ID del grupo al que pertenece la lista de nodos que queremos contar.</param>
    /// <param name="elementId">ID del elemento que hace de nodo padre de la lista de nodos que queremos contar.</param>
    public static int GetTextNodesCount(string groupId, string elementId){
        string _xmlPathToNode = "/locatedtexts/group[@id='" + groupId + "']/element[@id='" + elementId + "']";
        return XMLFileReader.GetChildNodeCount(locatedTextDoc, _xmlPathToNode);
    }
    
	/// <summary>
	/// Metodo de inicializacion del documento XML a ser tratado por esta clase, para un idioma dado.
	/// </summary>
	/// <param name="language">Idioma actual del juego.</param>
    public static void InitializeLanguageSettings(string language){
		XMLFileReader.CloseXMLFile(out locatedTextDoc);
		currentLanguage = language;
		Initialize();
	}

	/// <summary>
	/// Metodo que cambia el languaje actual del juego.
	/// </summary>
	/// <param name="newLanguage">Nuevo idioma actual del juego.</param>
	public static void ChangeCurrentLanguage(string newLanguage){
		XMLFileReader.CloseXMLFile(out locatedTextDoc);
		currentLanguage = newLanguage;
		GameState.SystemData.currentLanguage = newLanguage;
		SettingsFileManager.Instance.SaveSettingsFile();
		Initialize();
		currentLanguageChanged();
	}

	/// <summary>
	/// Metodo que obtiene el ID de un nodo <c>string</c> contenido en el nodo de palabras claves de
	/// la Gadipedia del documento XML actual.
	/// </summary>
	/// <seealso cref="GadipediaSearchManager">Usado en esta clase para obtener los IDs de las palabras clave y cotejarlas
	/// con la base de datos de la Gadipedia.</seealso>
	/// <returns>Cadena con la palabra clave de la que queremos obtener el ID.</returns>
	/// <param name="valueKeyword">ID del nodo <c>string</c> dentro del documento XML, que contiene la palabra clave.</param>
	public static string GetGadipediaKeywordID(string valueKeyword){
		string _xmlPathToNode = "/locatedtexts/group[@id='GADIPEDIA_ARTICLES']/element[@id='KEYWORDS']";
		string _keywordID = "";

		XmlNodeList _keywords = XMLFileReader.GetChildNodes(locatedTextDoc, _xmlPathToNode);
		int _indexNodes = 0;

		while((_indexNodes < _keywords.Count) && (_keywords[_indexNodes].InnerText != valueKeyword)){
			_indexNodes++;
		}

		if(_indexNodes < _keywords.Count){
			_keywordID = _keywords[_indexNodes].Attributes["id"].Value;
		}

		return _keywordID;
	}

	/// <summary>
	/// Metodo que obtiene el idioma actual del juego.
	/// </summary>
	/// <returns>El idioma actual del juego.</returns>
	public static string GetCurrentLanguage(){
		return currentLanguage;
	}
}
