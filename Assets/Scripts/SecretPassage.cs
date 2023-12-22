using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretPassage : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private void Start() {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && other is BoxCollider2D)
        {
            StartCoroutine("RevealArea");
        }
    }

    IEnumerator RevealArea() 
    {
        while (tilemap.color.a > 32f/256f)
        {
            if (tilemap.color.a > 32f / 256f)
                tilemap.color -= new Color(0, 0, 0, 0.01f);
            else
                tilemap.color = new Color(1, 1, 1, 32f / 256f);
            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && other is BoxCollider2D)
        {
            StartCoroutine("HideArea");
        }
    }

    IEnumerator HideArea() 
    {
        while (tilemap.color.a < 1)
        {
            if (tilemap.color.a < 1)
                tilemap.color += new Color(0, 0, 0, 0.01f);
            else 
                tilemap.color = new Color(1, 1, 1, 1);
            yield return null;
        }
    }
}
