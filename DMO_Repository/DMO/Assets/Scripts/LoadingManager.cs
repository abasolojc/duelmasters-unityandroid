using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class LoadingManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		DMO.cardDictionary = new Dictionary<string, CardMetadata> ();
		LoadAllSets ();
		Debug.Log ("Card count: " + DMO.cardDictionary.Values.Count.ToString ());
		Application.LoadLevel ("_test");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void LoadSet (string xmlFileName)
	{
		TextAsset xmlSource = (TextAsset)Resources.Load ("Sets/" + xmlFileName, typeof(TextAsset));
		XmlNodeList cardNodeList;
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (xmlSource.text);
		
		string setName = xmlDoc.GetElementsByTagName ("cardset") [0].Attributes ["setname"].Value;
		Set newSet = new Set (setName, xmlFileName);
		
		cardNodeList = xmlDoc.GetElementsByTagName ("card");
		for (int i = 0; i < cardNodeList.Count; i++) {
			string candidateName = cardNodeList [i].Attributes ["name"].Value.ToUpper ();
			if (DMO.cardDictionary.ContainsKey (candidateName)) {
				DMO.cardDictionary [candidateName].AddSet (newSet);
			} else {
				DMO.cardDictionary.Add (candidateName,
				new CardMetadata (cardNodeList [i], newSet));
			}
		}
	}
	
	void LoadAllSets ()
	{
		TextAsset xmlSource = (TextAsset)Resources.Load ("DMO", typeof(TextAsset));
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (xmlSource.text);
		
		XmlNodeList setNodeList = xmlDoc.GetElementsByTagName ("sets") [0].ChildNodes;
		for (int i = 0; i < setNodeList.Count; i++) {
			string xmlFileName = setNodeList [i].InnerText;
			LoadSet (xmlFileName);
		}
	}
}
