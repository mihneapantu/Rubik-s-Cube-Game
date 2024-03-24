using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    //Sides of the cube:
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();

    public static bool autoRotating = false;
    public static bool started = false; //variable to make sure the game loaded correctly


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(List<GameObject> cubeSide)
    {
        foreach(GameObject face in cubeSide)
        {
            //Attach the parent of each face (the little cube) to the
            //parent of the 4th index (the little cube in the middle)
            //unless we are already analyzing the middle cube.

            if(face != cubeSide[4]) //check to see that it is not the middle cube
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;

            }

        }

    }

    //method to put down those pieces:

    public void PutDown(List<GameObject> littleCubes, Transform pivot)
    {
        foreach(GameObject littleCube in littleCubes)
        {
            if(littleCube != littleCubes[4])
            {
                littleCube.transform.parent.transform.parent = pivot;
            }
        }
    }
    //Output the current state of the cube in a string:
    //One side at a time
    string GetSideString(List<GameObject> side)
    {
        string sideString = "";
        foreach (GameObject face in side)
        {
            sideString += face.name[0].ToString(); //Add the face name to the string
        }
        return sideString;
    }

    //Build the full string in order URFDLB

    public string GetStateString()
    {
        string stateString = "";
        stateString += GetSideString(up);
        stateString += GetSideString(right);
        stateString += GetSideString(front);
        stateString += GetSideString(down);
        stateString += GetSideString(left);
        stateString += GetSideString(back);
        return stateString;


    }
}
