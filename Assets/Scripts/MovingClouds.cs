using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClouds : MonoBehaviour
{
    [SerializeField] private Material normalMaterial;
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        normalMaterial = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        normalMaterial.mainTextureOffset += Vector2.right * moveSpeed * Time.deltaTime;
    }
}
