using UnityEngine;
using System;
using System.Collections;
using System.Xml;

public static class XMLFileReader {
	
	public static void OpenXMLFile(out XmlDocument root, string xmlPath) {

		//This lines are for loading the localized XMLs from resources folder (used now for testing purposes)
		//TextAsset newTextAsset = (TextAsset)Resources.Load(xmlPath, typeof(TextAsset));
		//root = new XmlDocument();
		//root.LoadXml(newTextAsset.text);


		//This lines are for loading the localized XMLs from an external folder 
		//(after creating a build and adding the folder with XMLs manually)
		//(used when project is finished for easing work to the people who wants to make his owns located texts)
		xmlPath = xmlPath.Replace("\\","/");
		root = new XmlDocument();
		root.Load(xmlPath);
	}
	
	public static void OpenXMLFile(XmlDocument root, TextAsset textAsset){
		root = new XmlDocument();
		root.LoadXml(textAsset.text);
	}
	
	public static void CloseXMLFile(out XmlDocument root) {
		root = null;
		GC.Collect();
		GC.WaitForPendingFinalizers();
	}
	
	public static string GetNodeInfo(XmlDocument root, string xmlPathToNode){
		string _nodeText = "text";

		try{
			_nodeText = root.SelectSingleNode(xmlPathToNode).InnerText;
		} catch (NullReferenceException){
			_nodeText = "[This string has either not been implemented or needs to be translated.]";
			Debug.LogError("Text string not found with path: " + xmlPathToNode);
		}

		return _nodeText;
	}

	public static XmlNodeList GetChildNodes(XmlDocument root, string xmlPathToNode){
		try{
			XmlNodeList _childNodes = root.SelectSingleNode(xmlPathToNode).ChildNodes;
			return _childNodes;
		}catch(NullReferenceException){
			Debug.LogError("Path to node not found");
			return null;
		}
	}

	public static int GetChildNodeCount(XmlDocument root, string xmlPathToNode){
		int _childNodeCount;

		try{
			_childNodeCount = root.SelectSingleNode(xmlPathToNode).ChildNodes.Count;
		}catch(NullReferenceException){
			Debug.LogError("Path to node not found");
			_childNodeCount = 0;
        }

		return _childNodeCount;
	}
}
