using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {
	//Suits
	public Sprite suitClub;
	public Sprite suitDiamond;
	public Sprite suitHeart;
	public Sprite suitSpade;
	
	public Sprite[] faceSprites;
	public Sprite[] rankSprites;
	
	public Sprite cardBack;
	public Sprite cardBackGold;
	public Sprite cardFront;
	public Sprite cardFrontGold;
	
	
	// Prefabs
	public GameObject prefabSprite;
	public GameObject prefabCard;

	public bool _____________________;

	public PT_XMLReader					xmlr;
	// add from p 569
	public List<string>					cardNames;
	public List<Card>					cards;
	public List<Decorator>				decorators;
	public List<CardDefinition>			cardDefs;
	public Transform					deckAnchor;
	public Dictionary<string, Sprite>	dictSuits;


	// called by Prospector when it is ready
	public void InitDeck(string deckXMLText) {
		// from page 576
		if( GameObject.Find("_Deck") == null) {
			GameObject anchorGO = new GameObject("_Deck");
			deckAnchor = anchorGO.transform;
		}
		
		// init the Dictionary of suits
		dictSuits = new Dictionary<string, Sprite>() {
			{"C", suitClub},
			{"D", suitDiamond},
			{"H", suitHeart},
			{"S", suitSpade}
		};
		
		
		
		// -------- end from page 576
		ReadDeck (deckXMLText);
		MakeCards();
	}


	// ReadDeck parses the XML file passed to it into Card Definitions
	public void ReadDeck(string deckXMLText)
	{
		xmlr = new PT_XMLReader ();
		xmlr.Parse (deckXMLText);

		// print a test line
		string s = "xml[0] decorator [0] ";
		s += "type=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("type");
		s += " x=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("x");
		s += " y=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("y");
		s += " scale=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("scale");
		print (s);
		
		//Read decorators for all cards
		// these are the small numbers/suits in the corners
		decorators = new List<Decorator>();
		// grab all decorators from the XML file
		PT_XMLHashList xDecos = xmlr.xml["xml"][0]["decorator"];
		Decorator deco;
		for (int i=0; i<xDecos.Count; i++) {
			// for each decorator in the XML, copy attributes and set up location and flip if needed
			deco = new Decorator();
			deco.type = xDecos[i].att ("type");
			deco.flip = (xDecos[i].att ("flip") == "1");   // too cute by half - if it's 1, set to 1, else set to 0
			deco.scale = float.Parse (xDecos[i].att("scale"));
			deco.loc.x = float.Parse (xDecos[i].att("x"));
			deco.loc.y = float.Parse (xDecos[i].att("y"));
			deco.loc.z = float.Parse (xDecos[i].att("z"));
			decorators.Add (deco);
		}
		
		// read pip locations for each card rank
		// read the card definitions, parse attribute values for pips
		cardDefs = new List<CardDefinition>();
		PT_XMLHashList xCardDefs = xmlr.xml["xml"][0]["card"];
		
		for (int i=0; i<xCardDefs.Count; i++) {
			// for each carddef in the XML, copy attributes and set up in cDef
			CardDefinition cDef = new CardDefinition();
			cDef.rank = int.Parse(xCardDefs[i].att("rank"));
			
			PT_XMLHashList xPips = xCardDefs[i]["pip"];
			if (xPips != null) {			
				for (int j = 0; j < xPips.Count; j++) {
					deco = new Decorator();
					deco.type = "pip";
					deco.flip = (xPips[j].att ("flip") == "1");   // too cute by half - if it's 1, set to 1, else set to 0
					
					deco.loc.x = float.Parse (xPips[j].att("x"));
					deco.loc.y = float.Parse (xPips[j].att("y"));
					deco.loc.z = float.Parse (xPips[j].att("z"));
					if(xPips[j].HasAtt("scale") ) {
						deco.scale = float.Parse (xPips[j].att("scale"));
					}
					cDef.pips.Add (deco);
				} // for j
			}// if xPips
			
			// if it's a face card, map the proper sprite
			// foramt is ##A, where ## in 11, 12, 13 and A is letter indicating suit
			if (xCardDefs[i].HasAtt("face")){
				cDef.face = xCardDefs[i].att ("face");
			}
			cardDefs.Add (cDef);
		} // for i < xCardDefs.Count
	} // ReadDeck
	
	public CardDefinition GetCardDefinitionByRank(int rnk) {
		foreach(CardDefinition cd in cardDefs) {
			if (cd.rank == rnk) {
					return(cd);
			}
		} // foreach
		return (null);
	}//GetCardDefinitionByRank
	
	
	public void MakeCards() {
		// stub Add the code from page 577 here
		
	} // makeCards
	
} // Deck class
