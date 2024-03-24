using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{

    CubeState cubeState;
    ReadCube readCube;
    int layerMask = 1 << 8;

    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //read the current state of the cube
            readCube.ReadState();

            //raycast from the mouse towards the cube to see if a face is hit
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Start point of the ray will be the mouse position.

            //If we hit a face, we save the face we hit:
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;

                //Create a list of all the sides (lists of face GameObjects):
                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.front,
                    cubeState.back,
                    cubeState.right,
                    cubeState.left
                };

                //If the face hit exists within a side
                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if(cubeSide.Contains(face)) //if the cubeside contains the face we clicked on
                                                //we make the pieces in the side children of the 
                                                //central piece by using PickUp method
                    {
                        cubeState.PickUp(cubeSide);
                        //start the side rotation logic
                        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
                    }
                }
            }

           
        }
        
    }
}
