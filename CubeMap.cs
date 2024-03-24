using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{

    CubeState cubeState;

    public Transform front;
    public Transform back;
    public Transform right;
    public Transform left;
    public Transform up;
    public Transform down;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set() //method to set the colors of the cube map
    {
        cubeState = FindObjectOfType<CubeState>();

        UpdateMap(cubeState.front, front);
        UpdateMap(cubeState.back, back);
        UpdateMap(cubeState.up, up);
        UpdateMap(cubeState.down, down);
        UpdateMap(cubeState.right, right);
        UpdateMap(cubeState.left, left);

    }

    void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            if (face[i].name[0] == 'F')       //name[0] = F si vine de la Front
            {
                map.GetComponent<Image>().color = new Color(0.9339623f, 0.4952427f, 0.03964933f, 1);
            }
            
            if (face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = new Color(1, 0.0707f, 0.0707f, 1);
            }

            if (face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = new Color(0.875447f, 0.9150f, 0.0906f, 1);
            }

            if (face[i].name[0] == 'D')
            {
                map.GetComponent<Image>().color = new Color(1,1,1,1);
            }

            if (face[i].name[0] == 'R')
            {
                map.GetComponent<Image>().color = new Color(0.0718f, 0.2028f, 0.8962f, 1);
            }

            if (face[i].name[0] == 'L')
            {
                map.GetComponent<Image>().color = new Color(0.0942f, 0.6886f, 0.2174f, 1);
            }
            i++;
        }
    }
}
