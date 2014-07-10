using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
	public GameObject prefab;
	
	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < 15; i++) {
			GameObject cards = (GameObject)Instantiate(prefab);
			cards.transform.parent = GameObject.Find("Scroll Panel").transform;
		}
		
		List<CardMetadata> allCards = new List<CardMetadata> ();
		allCards.AddRange (DMO.cardDictionary.Values);
		int rand = Random.Range (0, allCards.Count - 1);
		
		GameObject card = GameObject.Find ("Card");
		Texture cardTexture = (Texture)Resources.Load ("CardImages/" + allCards [rand].sets [allCards [rand].setIndex].name + "/" + allCards [rand].name);
		card.GetComponent<dfTextureSprite> ().Texture = cardTexture;
		
		GameObject card_atlased = GameObject.Find ("Card_Atlased");
		GameObject oAtlas = (GameObject)Resources.Load ("SetAtlases/" + allCards [rand].sets [allCards [rand].setIndex].name);
		dfAtlas setAtlas = oAtlas.GetComponent<dfAtlas>();
		dfSprite cardSprite = card_atlased.GetComponent<dfSprite> ();
		cardSprite.Atlas = setAtlas;
		cardSprite.SpriteName = allCards [rand].name;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void SampleEvent ()
	{
	}
}
