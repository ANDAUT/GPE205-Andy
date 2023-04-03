using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Transform playerSpawnTransform;

    private void Start()
    {
        //Temporary until new knowledge acquired
        SpawnPlayer();
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

    //Prefabs
    public GameObject Player0;
    public GameObject UATank;

    public void SpawnPlayer()
    {
        //Spawns the player Controller with values: (0, 0, 0)
        GameObject newPlayerObj = Instantiate(Player0, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawns the pawn and connects it to its respective controller
        GameObject newPawnObj = Instantiate(UATank, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

        // Gets both the player controller and pawn components
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }
    
}
