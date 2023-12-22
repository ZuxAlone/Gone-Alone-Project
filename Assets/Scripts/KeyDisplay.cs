using System;
using UnityEngine;
using UnityEngine.UI;

public class KeyDisplay : MonoBehaviour
{

    [SerializeField] private KeyPart[] keyParts;
    [SerializeField] private Image[] images;

    public void InitializeKeyDisplay() 
    {
        for (int i = 0; i < images.Length; i++)
        {
            KeyPart part = Array.Find(keyParts, k => (k.numKeyPart == i && !k.isPicked));
            images[i].sprite = part.keySprite;
        }
    }

    public void UpdateKeyDisplay(int numPart) 
    {
        KeyPart part = Array.Find(keyParts, k => (k.numKeyPart == numPart && k.isPicked));
        images[numPart].sprite = part.keySprite;
    }
}
