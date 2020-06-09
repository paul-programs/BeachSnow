using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManagerScript : MonoBehaviour {
    public enum GameState { intro, menu, inGame, gameOver }

    GameState gameState = GameState.intro;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	//create method for initializing player and levels for ingame start up
	//develop polling for input from player
	//based on input do different things
	//if into then skip to menu, if menu then go into game or settings, if gameover then go into game
}
