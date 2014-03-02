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
		LoadSet ("DM-1 Base Set");
		Debug.Log ("Card count: " + DMO.cardDictionary.Values.Count.ToString ());
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void LoadSet (string xmlFileName)
	{
		TextAsset xmlSource = (TextAsset)Resources.Load ("Sets/" + xmlFileName, typeof(TextAsset));
		XmlNodeList xmlNodeList;
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (xmlSource.text);
		
		string setName = xmlDoc.GetElementsByTagName ("cardset") [0].Attributes ["setname"].Value;
		Set newSet = new Set (setName, xmlFileName);
		
		xmlNodeList = xmlDoc.GetElementsByTagName ("card");
		for (int i = 0; i < xmlNodeList.Count; i++) {
			string candidateName = xmlNodeList [i].Attributes ["name"].Value.ToUpper ();
			if (DMO.cardDictionary.ContainsKey (candidateName)) {
				DMO.cardDictionary [candidateName].AddSet (newSet);
			} else {
				DMO.cardDictionary.Add (candidateName,
				new CardMetadata (xmlNodeList [i], newSet));
			}
		}
	}
}
