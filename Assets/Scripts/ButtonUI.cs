using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{

    public GameManager gameManager;
    public MapGenerator mapGen;

    [SerializeField] private string newGameLevel = "Main";
    public void LoadingScreenButton()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void TitleScreenButton()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateMainMenuScreen();
        }
    }

    public void PlayButton()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateGameScreen();
        }
    }

    public void OptionsButton()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateOptionsScreen();
        }
    }

    public void RandomOrNot(bool mapSet)
    {
        if (mapSet)
        {
            mapGen.ChangeMapType(MapGenerator.GeneratorType.MapOfTheDay);
        }
        else
        {
            mapGen.ChangeMapType(MapGenerator.GeneratorType.RandomSeed);
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(newGameLevel);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
