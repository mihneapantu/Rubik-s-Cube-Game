using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadCube : MonoBehaviour
{

    //Every object in a Scene has a Transform. It's used to store and manipulate the position,
    //rotation and scale of the object:
    public Transform tUp;
    public Transform tDown;
    public Transform tFront;
    public Transform tBack;
    public Transform tRight;
    public Transform tLeft;

    //private list of game objects to house the Rays for each side of the cube
    //each list will have 9 rays

    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();


    private int layerMask = 1 << 8;
    //layerMask is only for the faces of the cube;

    CubeState cubeState;
    CubeMap cubeMap;
    public GameObject emptyGO;


    // Start is called before the first frame update
    void Start()
    {
        SetRayTransforms();

        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();
        ReadState();
        CubeState.started = true;


    }

    // Update is called once per frame
    void Update()
    {
        //ReadState();
    }

    //Method to read every side of the cube (all of them):

    public void ReadState()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        //We will now set the state of each position in the list of sides
        //in order to know what color is in which position
        cubeState.up = ReadFace(upRays, tUp);
        cubeState.down = ReadFace(downRays, tDown);
        cubeState.front = ReadFace(frontRays, tFront);
        cubeState.back = ReadFace(backRays, tBack);
        cubeState.right = ReadFace(rightRays, tRight);
        cubeState.left = ReadFace(leftRays, tLeft);

        //update the map with the found positions:
        cubeMap.Set();

    }


    void SetRayTransforms()
    {
        //populate the ray lists with raycasts eminating from the transforms
        //and angled towards the cube

        upRays = BuildRays(tUp, new Vector3(90, 90, 0));
        downRays = BuildRays(tDown, new Vector3(270, 90, 0));
        rightRays = BuildRays(tRight, new Vector3(0, 0, 0));
        leftRays = BuildRays(tLeft, new Vector3(0, 180, 0));
        frontRays = BuildRays(tFront, new Vector3(0, 90, 0));
        backRays = BuildRays(tBack, new Vector3(0, 270, 0));


    }


    //Function to build rays for reading the cube.
    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        //Ray count is used to name the rays to be sure they are in the correct order.
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();
        //This list is used to create 9 rays in the shape of the side of the cube
        //ray 0 starts from top left and ray 8 will be at the bottom right.
        //|0|1|2|
        //|3|4|5|
        //|6|7|8|
        //the coordinates of the center, |4|, are (0,0}
        //so for |0| the coordinates would be (-1,1).

        //Create rays for each element of the side of the cube:
        for(int y = 1; y > -2; y--)
        {
            for(int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + x,
                    rayTransform.localPosition.y + y, rayTransform.localPosition.z);

                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction); //to fix the rotation
        return rays;

    }

    //Method to read an entire side of the cube (only one):
    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        
        List<GameObject> facesHit = new List<GameObject>();

        //Sending rays for each of the rayStart:

        foreach (GameObject rayStart in rayStarts)
        {

            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            //Check to see if rays intersects a layer of the cube or not:

            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                //print(hit.collider.gameObject.name);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }

        }

        return facesHit;
        //cubeMap.Set(); Set the color of the map based on the contents of the faces 
                       //hit list.
    }


}
