using System.Collections.Generic;
using System.Xml;

public class CardMetadata
{
	public enum Civilization
	{
		Water,
		Light,
		Nature,
		Fire,
		Darkness
	}

	public enum Type
	{
		Creature,
		Spell
	}

	public enum Race
	{
		Angel_Command,
		Armored_Dragon,
		Armored_Wyvern,
		Berserker,
		Demon_Command,
		Giant_Insect,
		Guardian,
		Horned_Beast,
		Leviathan,
		Liquid_People
	}

	public enum Rarity
	{
		S
	}
	
	public string name;
//	public List<Civilization> civilization;
	public List<string> civilization;
//	public Type type;
	public string type;
//	public List<Race> race;
	public List<string> race;
	public int cost;
	public int power;
	public string rarity;
	public string collector_number;
	public string artist;
	public string flavor_text;
	public string rules_text;
	public List<Set> sets;
	public int setIndex;
	
	public CardMetadata (
		string name,
		List<string> civilization,
		string type,
		List<string> race,
		int cost,
		int power,
		string rarity,
		string collector_number,
		string artist,
		string flavor_text,
		string rules_text,
		Set origSet
		)
	{
		this.name = name;
		this.civilization = civilization;
		this.type = type;
		this.race = race;
		this.cost = cost;
		this.power = power;
		this.rarity = rarity;
		this.collector_number = collector_number;
		this.artist = artist;
		this.flavor_text = flavor_text;
		this.rules_text = rules_text;
		this.sets = new List<Set> ();
		this.sets.Add (origSet);
		setIndex = 0;
	}
	
	public CardMetadata (XmlNode cardNode, Set origSet)
	{
		this.name = cardNode.Attributes ["name"].Value;
		List<string> civilization = new List<string> ();
		civilization.Add (cardNode.Attributes ["civilization"].Value);
		this.civilization = civilization;
		this.type = cardNode.Attributes ["type"].Value;
		List<string> race = new List<string> ();
		race.Add (cardNode.Attributes ["race"].Value);
		this.race = race;
		this.cost = int.Parse (cardNode.Attributes ["cost"].Value);
		this.power = int.Parse (cardNode.Attributes ["power"].Value);
		this.rarity = cardNode.Attributes ["rarity"].Value;
		this.collector_number = cardNode.Attributes ["collector_number"].Value;
		this.artist = cardNode.Attributes ["artist"].Value;
		string flavor_text = "";
		string rules_text = "";
		XmlNodeList miscNodeList = cardNode.ChildNodes;
		for (int i = 0; i < miscNodeList.Count; i++) {
			switch (miscNodeList [i].Name) {
			case "flavor_text":
				flavor_text = miscNodeList [i].InnerXml;
				break;
			case "rules_text":
				rules_text = miscNodeList [i].InnerXml;
				break;
			}
		}
		this.flavor_text = flavor_text;
		this.rules_text = rules_text;
		this.sets = new List<Set> ();
		sets.Add (origSet);
		setIndex = 0;
	}

	public void AddSet (Set newSet)
	{
		sets.Add (newSet);
	}
}

public struct Set
{
	public string name;
	public string xmlFileName;
	
	public Set (string name, string xmlFileName)
	{
		this.name = name;
		this.xmlFileName = xmlFileName;
	}
}