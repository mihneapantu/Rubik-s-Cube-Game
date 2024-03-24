using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{

    private List<GameObject> activeSide; //variable to know which side is rotating
    private Vector3 localForward;
    private Vector3 mouseRef; //the amount that a side rotates
    private bool dragging = false; //check if the mouse is dragging a side around

    private ReadCube readCube;
    private CubeState cubeState;

    private float sensitivity = 0.4f;   //vairable to handle how much the side should
                                        //rotate based how much the mouse was moved

    private Vector3 rotation; //variable to look after the rotation itself

    //Variables for automatically complete side rotation:
    private bool autoRotating = false;
    private Quaternion targetQuaternion; //variable to target the angle we want to 
                                         //automatically move to

    private float speed = 300f; //The speed of rotation


    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // LateUpdate is called once per frame at the end
    void LateUpdate()
    {
        //cool the spinside method everyframe:
        if(dragging && !autoRotating)
        {
            SpinSide(activeSide);
            if(Input.GetMouseButtonUp(0))
            {
                dragging = false;
                RotateToRightAngle();
            }
        }
        if(autoRotating)
        {
            AutoRotate();
        }
        
    }

    //set the variables for the automatic rotation

    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        cubeState.PickUp(side);
        //figure the axis to rotate around
        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        activeSide = side;
        autoRotating = true;

    }

    //Method to set the variables for rotation and it will be
    //called at the start of the rotating
    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Input.mousePosition; //track the start pos of the mouse to know
                                        //how much to rotate the side by, as the mouse 
                                        //moves away from the start pos
        dragging = true;
        //Create a vector to rotate around based on the local position
        //of the piece we are rotating and the center of the cube 
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;

    }

    //method to calculate the rotation that is called on every frame we are dragging
    //the side:
    
    private void SpinSide(List<GameObject> side)
    {
        //reset the rotation
        rotation = Vector3.zero;

        //current mouse position minus last mouse position to know how
        //much to retain the side:
        Vector3 mouseOffset = (Input.mousePosition - mouseRef);

        if(side == cubeState.front) //if it is front we will rotate around X axis:
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if (side == cubeState.back)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if (side == cubeState.up)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if (side == cubeState.down)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if (side == cubeState.left)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if (side == cubeState.right)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }


        //rotate:
        transform.Rotate(rotation, Space.Self);

        //store mouse position for the next time we call this method:
        mouseRef = Input.mousePosition;
    }

    //method for automatic rotation:
    public void RotateToRightAngle()
    {
        //Set the variables:
        Vector3 vec = transform.localEulerAngles;
        //round vec to the nearest 90 degrees:
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;
        autoRotating = true;
    }

    //method that will be called every frame when autoRotating is true
    private void AutoRotate()
    {
        dragging = false;
        var step = speed * Time.deltaTime;
        //adjust the local rotation of the pivot by this step amount over time:
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        //if we are close to the target we want to end the rotation and line everything up,
        //put down the pieces we picked up, read the new state of the cube and set autoRotating
        //to false:
        if(Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            //unparent the little cubes from the center little piece:
            cubeState.PutDown(activeSide, transform.parent);

            readCube.ReadState();
            CubeState.autoRotating = false;

            autoRotating = false;
            dragging = false;
        }
    }

}
