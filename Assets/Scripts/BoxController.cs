using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject cube;
    private Vector3 cubeStartingSize;

    private void Start()
    {
        cubeStartingSize = cube.transform.localScale;
    }
    public void ExpandCube(float expansionAmount)
    {
        //cube.transform.localScale.x *= expansionAmount;
    }
    public void ResetCube()
    {
        cube.transform.localScale = cubeStartingSize;
    }
}
