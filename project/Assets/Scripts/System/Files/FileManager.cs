using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Security.Cryptography;

public static class FileManager{
	public static string path;

	public static void InitializeDataPath(){
		path = Application.dataPath; 
		path = path.Replace("\\", "/");
	}

	public static bool CheckDirectory(string directory) {
		directory = directory.Replace("\\", "/");

		if(Directory.Exists(path + "/" + directory)){
			return true;
		}
		else{
			return false;
		}
	}
	
	public static void CreateDirectory(string directory){
		directory = directory.Replace("\\", "/");

		if(CheckDirectory(directory) == false){
			Directory.CreateDirectory(path + "/" + directory);
		}
		else{
			Debug.LogError("Error: You are trying to create the directory " + directory + " but it already exists!");
		}
	}
	
	public static void MoveDirectory(string originalDestination, string newDestination){
		originalDestination = originalDestination.Replace("\\", "/");
		newDestination = newDestination.Replace("\\", "/");

		if(CheckDirectory(originalDestination) == true && CheckDirectory(newDestination) == false){
			Directory.Move(path + "/" + originalDestination, path + "/" + newDestination);
		}
		else{
			Debug.LogError("Error: You are trying to move a directory but it either does not exist or a folder of the same name already exists");
		}
	}
	
	public static string[] FindSubDirectories(string directory, string searchPattern = "*"){
		directory = directory.Replace("\\", "/");
		string[] subdirectoryList = Directory.GetDirectories(path + "/" + directory, searchPattern);

		for(int i = 0; i < subdirectoryList.Length; i++){
			subdirectoryList[i] = subdirectoryList[i].Replace("\\", "/");
		}

		return subdirectoryList;
	}
	
	public static string[] FindFiles(string directory, string searchPattern = "*"){
		directory = directory.Replace("\\", "/");
		string[] fileList = Directory.GetFiles(path + "/" + directory, searchPattern);

		for(int i = 0; i < fileList.Length; i++){
			fileList[i] = fileList[i].Replace("\\", "/");
		}

		return fileList;
	}
	
	public static bool CheckFile(string filePath){
		filePath = filePath.Replace("\\", "/");

		if(File.Exists(path + "/" + filePath)){
			return true;
		}
		else{
			return false;
		}
	}
	
	public static void CreateFile(string directory, string filename, string filetype, string fileData){
		directory = directory.Replace("\\", "/");
		if(CheckDirectory(directory) == true){
			if(CheckFile(path + "/" + directory + "/" + filename) == false){
				File.WriteAllText(path + "/" + directory + "/" + filename + "." + filetype, fileData);
			}
			else{
				Debug.LogError("The file " + filename + " already exists in " + path + "/" + directory);
			}
		}
		else{
			Debug.LogError("Unable to create file as the directory " + directory + " does not exist");
		}
	}
	
	public static string ReadFile(string directory, string filename, string filetype){
		directory = directory.Replace("\\", "/");
		if(CheckDirectory(directory) == true){
			if(CheckFile(directory + "/" + filename + "." + filetype) == true){
				string fileContents = File.ReadAllText(path + "/" + directory + "/" + filename + "." + filetype);
				return fileContents;
			}
			else{
				Debug.LogError("The file " + filename + " does not exist in " + path + "/" + directory);
				return null;
			}
		}
		else{
			Debug.LogError("Unable to read the file as the directory " + directory + " does not exist");
			return null;
		}
	}
	
	public static void DeleteFile(string filePath){
		filePath = filePath.Replace("\\", "/");
		if(File.Exists(path + "/" + filePath)){
			File.Delete(path + "/" + filePath);
		}
		else{
			Debug.LogError("Unable to delete file as it does not exist");
		}
	}
	
	public static void UpdateFile(string directory, string filename, string filetype, string fileData, string mode){
		directory = directory.Replace("\\", "/");

		if(CheckDirectory(directory) == true){
			if(CheckFile(directory + "/" + filename + "." + filetype) == true){
				if(mode == "replace"){
					File.WriteAllText(path + "/" + directory + "/" + filename + "." + filetype, fileData);
				}
				else if(mode == "append"){
					File.AppendAllText(path + "/" + directory + "/" + filename + "." + filetype, fileData);
				}
			}
			else{
				Debug.LogError("The file " + filename + " does not exist in " + path + "/" + directory);
			}
		}
		else{
			Debug.LogError("Unable to create file as the directory " + directory + " does not exist");
		}
	}

	public static void CreateXMLFile(string directory, string filename, string filetype, string fileData, string mode){
		directory = directory.Replace("\\", "/");
		
		if(CheckDirectory(directory) == true){
			if(mode == "plaintext"){
				File.WriteAllText(path + "/" + directory + "/" + filename + "." + filetype, fileData);
			}
			
			if(mode == "encrypt"){
				fileData = EncryptData(fileData);
				File.WriteAllText(path + "/" + directory + "/" + filename + "." + filetype, fileData);
			}
		}
		else{
			Debug.LogError("Unable to create file as the directory " + directory + " does not exist");
		}
	}

	public static string EncryptData(string toEncrypt){
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes("1812laaventura");
		
		byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
		RijndaelManaged rDel = new RijndaelManaged();
		
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		
		rDel.Padding = PaddingMode.PKCS7;
		
		ICryptoTransform cryptoTransform = rDel.CreateEncryptor();
		byte[] resultArray = cryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
		
		return Convert.ToBase64String(resultArray, 0, resultArray.Length);
	}
	
	public static string DecryptData(string toDecrypt){
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes("1812laaventura");
		
		byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
		RijndaelManaged rDel = new RijndaelManaged();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		
		rDel.Padding = PaddingMode.PKCS7;
		
		ICryptoTransform cryptoTransform = rDel.CreateDecryptor();
		
		byte[] resultArray = cryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
		
		return UTF8Encoding.UTF8.GetString(resultArray);
	}

	public static string XmlDocToString(XmlDocument xmlDoc){
		StringWriter _stringWriter = new StringWriter();
		XmlTextWriter _xmlWriter = new XmlTextWriter(_stringWriter);
		_xmlWriter.Formatting = Formatting.Indented;
		xmlDoc.WriteTo(_xmlWriter);

		string _newString = _stringWriter.ToString();

		_stringWriter.Close();
		_xmlWriter.Close();

		return _newString;
    }
}
