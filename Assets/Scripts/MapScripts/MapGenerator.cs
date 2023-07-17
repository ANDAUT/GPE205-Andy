using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    private Room[,] grid;

    //The string is what you input, the int is what comes out (might set it to private)
    public string MapSeed;
    public int currentSeed;

    //Generator type is an enum that holds all of the possible seed types, be it set, random, or 'map of the day'
    public enum GeneratorType {RandomSeed, SetSeed, MapOfTheDay};
    public GeneratorType mapType;


    //Temporary code for spawning our map via key
    public KeyCode spawnMapKey;
    private bool alreadySpawned;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        alreadySpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        TheSpawnKey();
    }

    public void spawnTheMap()
    {
        if(alreadySpawned == false)
        {
            GenerateMap();
            alreadySpawned = true;
        }
    }

    public void TheSpawnKey()
    {
        if (Input.GetKey(spawnMapKey) && alreadySpawned == false)
        {
            GenerateMap();
            alreadySpawned = true;
            Debug.Log("Map has been spawned!");
        }
    }

    public int DateToInt(DateTime dateToUse)
    {
        //This adds our date and returns it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    //this returns a random room
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public virtual void ChangeMapType(GeneratorType newType)
    {
        //Changes the current state
        mapType = newType;
    }

    public void GenerateMap()
    {

        //oh god this is deprecated
        if (mapType == GeneratorType.MapOfTheDay)
        {
            currentSeed = DateToInt(DateTime.Now.Date);
            UnityEngine.Random.InitState(currentSeed);

        }
        else if (mapType == GeneratorType.SetSeed)
        {
            currentSeed = MapSeed.GetHashCode();
            UnityEngine.Random.InitState(currentSeed);

        }
        else if (mapType == GeneratorType.RandomSeed)
        {
            currentSeed = UnityEngine.Random.Range(0, 10000000);
            UnityEngine.Random.InitState(currentSeed);
        }

        //This clears out the grid - column being x, and row being y
        grid = new Room[cols, rows];

        //For each grid row... (establish the int, check if this is true, then increment by 1)
        //standard for loop stuff; 2d array
        for(int currentRow = 0; currentRow < rows; currentRow++)
        {
            for(int currentCol = 0; currentCol < cols; currentCol++)
            {
                //This figures out the location
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                //This creates a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                //This sets its parent

                tempRoomObj.transform.parent = this.transform;

                //This gives it a meaningful name
                tempRoomObj.name = "Room_" + currentCol + "," + currentRow;

                //This gets the room object
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                //and this saves it to the grid array
                grid[currentCol, currentRow] = tempRoom;

                //This checks the room's location, opening any necessary doors
                //First off, check if we're on the bottom row, if so, open the north door!
                if (currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                } else if (currentRow == rows - 1)
                {
                    //If not, lets open the south door instead (if we're on the top row)
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    //If we're in the MIDDLE, open both instead
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }

                if(currentCol == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                } else if (currentCol == cols - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }



            }
        }
    }
}
