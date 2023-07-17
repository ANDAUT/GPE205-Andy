using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Transform playerSpawnTransform;
    public MapGenerator mapGen;

    public enum CurrentGameState { TitleScreen, MainMenu, Options, Credits, Gameplay, GameOver };
    public CurrentGameState CurrentState;

    // Game States
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;


    private void Start()
    {
        if(TitleScreenStateObject != null)
        {
            ActivateTitleScreen();
        }

        //Temporary until new knowledge acquired
        //SpawnPlayer();
        //mapGen.GenerateMap();
    }

    // Awake means the code will run even before any other codes begin firing their Start() functions
    private void Awake()
    {
        // Checks for an instance, if there isn't one, then...
        if (instance == null)
        {
            // It declares 'this' as the instance
            instance = this;
            // This line prevents this from being destroyed on load
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If there is an instance, however, the gameObject this script is tied to will be destroyed
            Destroy(gameObject);
        }
    }

    //Player List / Count
    public List<PlayerController> players;
    //AI List / Count
    public List<PlayerController> cpus;

    //Prefabs
    public GameObject PlayerController;
    public GameObject PlayerTank;

    public void SpawnPlayer()
    {
        if(playerSpawnTransform != null)
        {
            //Spawns the player Controller with values: (0, 0, 0)
            GameObject newPlayerObj = Instantiate(PlayerController, Vector3.zero, Quaternion.identity) as GameObject;

            // Spawns the pawn and connects it to its respective controller
            GameObject newPawnObj = Instantiate(PlayerTank, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

            // Gets both the player controller and pawn components
            Controller newController = newPlayerObj.GetComponent<Controller>();
            Pawn newPawn = newPawnObj.GetComponent<Pawn>();

            newController.pawn = newPawn;
            newController.score = 0;
            newPawn.controller = newController;
        }
        
    }

    public void SpawnRandomPlatform()
    {
        //to be filled...
    }

    private void DeactivateAllStates()
    {
        //Deactivates every game state that exists
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }

    public void ActivateTitleScreen()
    {
        //Deactivates all states
        DeactivateAllStates();

        //and then proceed to activate the title screen
        TitleScreenStateObject.SetActive(true);
        CurrentState = CurrentGameState.TitleScreen;

        //Anything special for a title screen may be added here too
    }

    public void ActivateMainMenuScreen()
    {
        //Deactivates all states
        DeactivateAllStates();

        //and then proceed to activate the main menu screen
        MainMenuStateObject.SetActive(true);
        CurrentState = CurrentGameState.MainMenu;

        //Anything special for a title screen may be added here too
    }

    public void ActivateOptionsScreen()
    {
        //Deactivates all states
        DeactivateAllStates();

        //and then proceed to activate the options screen
        OptionsScreenStateObject.SetActive(true);
        CurrentState = CurrentGameState.Options;

        //Anything special for a title screen may be added here too
    }

    public void ActivateCreditsScreen()
    {
        //Deactivates all states
        DeactivateAllStates();

        //and then proceed to activate the credits screen
        CreditsScreenStateObject.SetActive(true);
        CurrentState = CurrentGameState.Credits;

        //Anything special for a title screen may be added here too
    }

    public void ActivateGameScreen()
    {
        //Deactivates all states
        DeactivateAllStates();

        //and then proceed to activate the gameplay screen
        GameplayStateObject.SetActive(true);
        CurrentState = CurrentGameState.Gameplay;


        //Anything special for a title screen may be added here too

        SpawnPlayer();
        mapGen.GenerateMap();
    }

    public void ActivateGameOverScreen()
    {
        //Deactivates all states
        DeactivateAllStates();

        //and then proceed to activate the game over screen
        GameOverScreenStateObject.SetActive(true);
        CurrentState = CurrentGameState.GameOver;

        //Anything special for a title screen may be added here too
    }
}
