using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBigCube : MonoBehaviour
{
    //Vectors that help moving the cube
    //The player will be able to right click the cube
    //and move it.
    Vector2 firstPressPosition; //Pos where swipe of the cube started
    Vector2 secondPressPosition; //Pos where swipe of the cube ended
    Vector2 currentSwipe; //For the direction of the swipe

    //Variables for visual feedback (to be able to move the cube by keeping the right
    //mouse button clicked).
    Vector3 previousMousePosition;
    Vector3 mouseDelta; //difference between the current and previous mouse position

    public GameObject target; //the object that we are going to rotate
    float speed = 300f; //variable used for the transition of the swipe move

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();
        
    }

    void Swipe()
    {
        //If the right button of the mouse is clicked:
        if (Input.GetMouseButtonDown(1))
        {
            //get the 2D pos of the 1sr click:
            firstPressPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //print(firstPressPosition); //Every time we click, the coordinates appear in console

        }
        //we build the swipe by comparing the point where the right button was clicked
        //to the point where it was released.

        if (Input.GetMouseButtonUp(1))
        {
            //get the 2D pos of the 2nd click:
            secondPressPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create a vector from the 1st and 2nd click positions:
            currentSwipe = new Vector2(secondPressPosition.x - firstPressPosition.x, secondPressPosition.y - firstPressPosition.y);

            //normalize the vector:
            currentSwipe.Normalize();

            //If the swipe is a left swipe, the target should rotate 90deg around y axe
            //If the swipe is a right swipe, the target should rotate -90deg around y axe
            if (LeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe(currentSwipe))
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if(UpLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(90, 0, 0, Space.World);
            }
            else if(UpRightSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (DownLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 0, 90, Space.World);
            }
            else if (DownRightSwipe(currentSwipe))
            {
                target.transform.Rotate(-90, 0, 0, Space.World);
            }

        }


    }

    //method for visual feedback:

    void Drag()
    {
        if (Input.GetMouseButton(1))
        {
            //while the right mouse button is held down the cube can be moved around its central axis
            //in order to provide visual feedback:
            mouseDelta = Input.mousePosition - previousMousePosition;
            mouseDelta = mouseDelta * 0.3f; //we multiplied with 0.1 to get a better control of the cube
                                            //(reduction of rotation speed)
            //the cube rotation will be updated based on the ammount the mouse is move:
            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * transform.rotation;
        }
        else
        {
            //automatically move to the target position

            if (transform.rotation != target.transform.rotation)
            {
                var step = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            }


        }
        previousMousePosition = Input.mousePosition;
    }

    //We determine what type of swipe the player did:   
    //In that way we can rotate the cube
    bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }
    //returns true if the swipe is moving in a negative x (left direction)
    //we also left room for a short movement on y axe.
    //we do the same for right swipe but this time the swipe should be moving in a positive x

    bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    //functions to rotate the cube around Ox and Oz axis:
    bool UpLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x < 0f;
    }

    bool UpRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x > 0f;
    }

    bool DownLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x < 0f;
    }

    bool DownRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x > 0f;
    }

}
