using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundMat : MonoBehaviour
{
    private Renderer render;

    public float xSpeed;
    public float ySpeed;

    private void Start()
    {
        render=GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        render.material.mainTextureOffset += new Vector2(xSpeed,ySpeed);
    }
}
