using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMover : MonoBehaviour
{
    public RectTransform rect;
    public float totalSpeed;
    public float xSpeed = 1f, ySpeed = 2f, zSpeed = 0f;
    public bool applyToChildren = false;
    private Vector3 moveVector = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        var xPos = Mathf.Sin(Time.time) * xSpeed;
        var yPos = Mathf.Sin(Time.time) * ySpeed;
        var zPos = Mathf.Sin(Time.time) * zSpeed;
        moveVector.Set(xPos, yPos, zPos);
        if(applyToChildren == true)
        {
            foreach(Transform child in transform)
            {
                var childRect = child.GetComponent<RectTransform>();
                var xrand = Random.Range(-1, 1);
                var yrand = Random.Range(-1, 1);
                moveVector.Set(xPos * xrand, yPos * yrand, zPos);
                childRect.localPosition += moveVector * totalSpeed * Time.deltaTime;
            }
        }
        else
        {
            rect.localPosition += moveVector * totalSpeed * Time.deltaTime;
        }
    }
}
