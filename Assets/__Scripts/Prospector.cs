﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Prospector : MonoBehaviour {

	static public Prospector 	S;

	[Header("Set in Inspector")]
	public TextAsset deckXML;
    public TextAsset layoutXML;

	[Header("Set Dynamically")]
	public Deck	deck;
    public Layout layout;

	void Awake(){
		S = this; // Set up a Singleton for Prospector
	}

	void Start() {
		deck = GetComponent<Deck> (); // Get the deck
		deck.InitDeck (deckXML.text); // Pass DeckXML
        Deck.Shuffle(ref deck.cards); // This shuffles the deck

        layout = GetComponent<Layout>(); // Get the Layout component
        layout.ReadLayout(layoutXML.text); // Pass LayoutXML to it
	}

}
