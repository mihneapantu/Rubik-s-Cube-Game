using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automate : MonoBehaviour
{

    //Script that will autmotically performs moves on cube
    //that exists in a list of moves
    public static List<string> moveList = new List <string> () {};
    private CubeState cubeState; //we need the contents of the side we are rotating
    private ReadCube readCube;

    //List with all moves:
    private readonly List<string> allMoves = new List<string>()
    {
        "U", "D", "F", "B", "R", "L",
        "U'", "D'", "F'", "B'", "R'", "L'",
        "U2", "D2", "F2", "B2", "R2", "L2",
    };


    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveList.Count > 0 && !CubeState.autoRotating && CubeState.started)
        {
            //do the move at the first index
            DoMove(moveList[0]);


            //remove the move at the first index
            moveList.Remove( moveList[0] ); 
            
        }
    }

    //Change the angle according to the move we are doing:
    void DoMove(string move)
    {
        readCube.ReadState();

        //U,D,R,L,F,B
        CubeState.autoRotating = true;
        if (move == "U")
        {
            RotateSide(cubeState.up, -90); //clockwise move U
        }
        if (move == "U'")
        {
            RotateSide(cubeState.up, 90);
        }
        if (move == "U2")
        {
            RotateSide(cubeState.up, -180);
        }

        if (move == "D")
        {
            RotateSide(cubeState.down, -90);
        }
        if (move == "D'")
        {
            RotateSide(cubeState.down, 90);
        }
        if (move == "D2")
        {
            RotateSide(cubeState.down, -180);
        }

        if (move == "R")
        {
            RotateSide(cubeState.right, -90);
        }
        if (move == "R'")
        {
            RotateSide(cubeState.right, 90);
        }
        if (move == "R2")
        {
            RotateSide(cubeState.right, -180);
        }

        if (move == "L")
        {
            RotateSide(cubeState.left, -90);
        }
        if (move == "L'")
        {
            RotateSide(cubeState.left, 90);
        }
        if (move == "L2")
        {
            RotateSide(cubeState.left, -180);
        }

        if (move == "F")
        {
            RotateSide(cubeState.front, -90);
        }
        if (move == "F'")
        {
            RotateSide(cubeState.front, 90);
        }
        if (move == "F2")
        {
            RotateSide(cubeState.front, -180);
        }

        if (move == "B")
        {
            RotateSide(cubeState.back, -90);
        }
        if (move == "B'")
        {
            RotateSide(cubeState.back, 90);
        }
        if (move == "B2")
        {
            RotateSide(cubeState.back, -180);
        }
    }


        //Rotate a side
    void RotateSide(List<GameObject> side, float angle)
    {
        //automatically rotate the side by angle
        //we need to apply the pivotrotation script to the side we are rotating
        PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
        pr.StartAutoRotate(side,angle); 

    }


    public void Shuffle()
    {
        //Method that creates a list of shuffled moves.
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(15, 25);
        for (int i = 0; i < shuffleLength; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]);
        }
        moveList = moves;
    }


}
