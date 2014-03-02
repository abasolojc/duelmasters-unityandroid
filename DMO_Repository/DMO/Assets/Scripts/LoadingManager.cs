using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class LoadingManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		DMO.setDictionary = new Dictionary<string, Dictionary<string, CardMetadata>> ();
		LoadSet ("DM-1 Base Set");
		Debug.Log ("Card count: " + DMO.setDictionary ["DM-1 Base Set"].Values.Count.ToString ());
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void LoadSet (string setName)
	{
		Dictionary<string, CardMetadata> setCards = new Dictionary<string, CardMetadata> ();
		TextAsset xmlSource = (TextAsset)Resources.Load ("Sets/" + setName, typeof(TextAsset));
		XmlNodeList xmlNodeList;
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (xmlSource.text);
		xmlNodeList = xmlDoc.GetElementsByTagName ("card");
		for (int i = 0; i < xmlNodeList.Count; i++) {
			setCards.Add (xmlNodeList [i].Attributes ["name"].Value,
				new CardMetadata (xmlNodeList [i]));
		}
		DMO.setDictionary.Add (setName, setCards);
	}
}
