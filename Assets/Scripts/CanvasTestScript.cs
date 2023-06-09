using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTestScript : MonoBehaviour
{

    public KeyCode transitionOne;
    public KeyCode transitionTwo;
    public KeyCode transitionThree;
    public KeyCode transitionFour;
    public KeyCode transitionFive;
    public KeyCode transitionSix;

    public GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
    }

    public void DetectInput()
    {
        if (Input.GetKey(transitionOne) && gameManager.CurrentState != GameManager.CurrentGameState.TitleScreen)
        {
            gameManager.ActivateTitleScreen();
        }
        if (Input.GetKey(transitionTwo) && gameManager.CurrentState != GameManager.CurrentGameState.MainMenu)
        {
            gameManager.ActivateMainMenuScreen();
        }
        if (Input.GetKey(transitionThree) && gameManager.CurrentState != GameManager.CurrentGameState.Options)
        {
            gameManager.ActivateOptionsScreen();
        }
        if (Input.GetKey(transitionFour) && gameManager.CurrentState != GameManager.CurrentGameState.Credits)
        {
            gameManager.ActivateCreditsScreen();
        }
        if (Input.GetKey(transitionFive) && gameManager.CurrentState != GameManager.CurrentGameState.Gameplay)
        {
            gameManager.ActivateGameScreen();
        }
        if (Input.GetKey(transitionSix) && gameManager.CurrentState != GameManager.CurrentGameState.GameOver)
        {
            gameManager.ActivateGameOverScreen();
        }
    }
}
