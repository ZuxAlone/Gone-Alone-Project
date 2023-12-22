using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndingManager : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    [SerializeField] GameObject menuButtonObj;
    [SerializeField] float timeOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowMenuButton", (float)director.playableAsset.duration + timeOffset);
    }

    void ShowMenuButton()
    {
        menuButtonObj.SetActive(true);
    }
}
