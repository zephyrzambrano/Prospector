﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Prospector : MonoBehaviour {

	static public Prospector S;

	[Header("Set in Inspector")]
	public TextAsset deckXML;
    public TextAsset layoutXML;

	[Header("Set Dynamically")]
	public Deck	deck;
    public Layout layout;
    public List<CardProspector> drawPile;

	void Awake(){
		S = this; // Set up a Singleton for Prospector
	}

    void Start() {
        deck = GetComponent<Deck>(); // Get the deck
        deck.InitDeck(deckXML.text); // Pass DeckXML
        Deck.Shuffle(ref deck.cards); // This shuffles the deck

        layout = GetComponent<Layout>(); // Get the Layout component
        layout.ReadLayout(layoutXML.text); // Pass LayoutXML to it
        drawPile = ConvertListCardsToListCardProspectors(deck.cards);
    }

    List<CardProspector> ConvertListCardsToListCardProspectors(List<Card> lCD) {
        List<CardProspector> lCP = new List<CardProspector>();
        CardProspector tCP;
        foreach (Card tCD in lCD) {
            tCP = tCD as CardProspector;
            lCP.Add(tCP);
        }
        return (lCP);
    }
}