using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

public static class XMLFileReader {
	private static XmlDocument root;
	
	public static void OpenXMLFile(string xmlPath) {
		//This lines are for loading the localized XMLs from resources folder (used now for testing purposes)
		TextAsset newTextAsset = (TextAsset)Resources.Load(xmlPath, typeof(TextAsset));
		root = new XmlDocument();
		root.LoadXml(newTextAsset.text);

		//This lines are for loading the localized XMLs from an external folder 
		//(after creating a build and adding the folder with XMLs manually)
		//(used when project is finished for easing work to the people who wants to make his owns localized texts)

		//root = new XmlDocument();
		//root.Load(xmlPath);
	}
	
	public static void OpenXMLFile(TextAsset textAsset){
		root = new XmlDocument();
		root.LoadXml(textAsset.text);
	}
	
	public static void CloseXMLFile(string xmlPath) {
		root = null;
		GC.Collect();
		GC.WaitForPendingFinalizers();
	}
	
	public static string GetNodeInfo(string xmlPathToNode){
		string nodeText;

		try{
			nodeText = root.SelectSingleNode(xmlPathToNode).InnerText;
		} catch (NullReferenceException exception){
			nodeText = "[This string has either not been implemented or needs to be translated.]";
			Debug.LogError("Text string not found.\nException: " + exception.ToString());
		}

		return nodeText;
	}
}
