using UnityEngine;
using System.Collections;

public class BGscroll : MonoBehaviour
{
    public float ScrollSpeed = 0.5f;
    float Target_Offset;

    void Update()
    {
        Target_Offset += Time.deltaTime * ScrollSpeed;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Target_Offset);
    }
}