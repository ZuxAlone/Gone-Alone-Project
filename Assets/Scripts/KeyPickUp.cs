using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    [SerializeField] private int numKeyPart;
    [SerializeField] private float amplitude;
    [SerializeField] private float frecuency;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(amplitude * Mathf.Cos((2 * Mathf.PI / frecuency) * Time.time) * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other is BoxCollider2D)
        {
            GameManager.Instance.UpdateKeyDisplay(numKeyPart);
            FindObjectOfType<AudioManager>().Play("Pickup");
            Destroy(gameObject);
        }
    }
}
