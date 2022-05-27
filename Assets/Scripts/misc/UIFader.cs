using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour
{
    public CanvasGroup can;
    public float sinLength = 1f, sinSpeed = 1f, alphaMin = 0.5f, alphaMax = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        can = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        can.alpha += Mathf.Sin(Time.time * sinLength) * sinSpeed;
        can.alpha = Mathf.Clamp(can.alpha, alphaMin, alphaMax);
    }
}
