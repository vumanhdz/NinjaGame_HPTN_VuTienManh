using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScriptz : MonoBehaviour
{
    public Transform mainCam;
    public Transform midBg;
    public Transform botBg;
    public float len;
    // Update is called once per frame
    void Update()
    {
        if (mainCam.position.x > midBg.position.x)
        {
            UpdateBackgroundPosition(Vector3.right);
        }
        else if(mainCam.position.x<midBg.position.x)
        {
            UpdateBackgroundPosition(Vector3.left);
        }
    }

    void UpdateBackgroundPosition(Vector3 direction)
    {
        botBg.position = midBg.position + direction * len;
        Transform temp = midBg;
        midBg = botBg; 
        botBg = temp;
    }
}
