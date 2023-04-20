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
    public float roomHeight = 20.0f;
    private Room[,] grid;

    public int mapSeed;
    public bool isMapOfTheDay;
    public bool isRandom;

    public KeyCode spawnMapKey;
    private bool alreadySpawned;

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

    public void GenerateMap()
    {
        //oh god this is deprecated
        UnityEngine.Random.seed = DateToInt(DateTime.Now);
        if (isMapOfTheDay)
        {
            mapSeed = DateToInt(DateTime.Now.Date);
        }

        if (isRandom)
        {
            mapSeed = DateToInt(DateTime.Now);
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
