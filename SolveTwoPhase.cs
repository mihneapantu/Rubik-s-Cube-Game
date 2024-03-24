using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class SolveTwoPhase : MonoBehaviour
{
    //Make sure we read the cube and we know the state of the cube:
    public CubeState cubeState;
    public ReadCube readCube;
    private bool doOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CubeState.started && doOnce)
        {
            doOnce = false;
            Solver();
        }
        
    }

    public void Solver()
    {
        readCube.ReadState(); //First we read the cube

        //get the state of the cube as a string
        string moveString = cubeState.GetStateString();
        //print(moveString);

        //solve the cube
        string info = "";
        
        //First build the tables
        //string solution = SearchRunTime.solution(moveString, out info, buildTables: true);

        //Every other time:
        string solution = Search.solution(moveString, out info);


        //convert the solved moves from sting to list
        List <string> solutionList = StringToList(solution);

        //Automate the list
        Automate.moveList = solutionList;
        //print(info);

    }

    //Create the list for Solver method
    List<string> StringToList(string solution)
    {
        List<string> solutionList = new List<string>(solution.Split(new string[] { " " },
            System.StringSplitOptions.RemoveEmptyEntries));

        return solutionList;
    }




}
