using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPresist : MonoBehaviour
{
    private void Awake()
    {
        int numScenePresist = FindObjectsOfType<ScreenPresist>().Length;

        if (numScenePresist > 1)
            Destroy(gameObject);

        else
            DontDestroyOnLoad(gameObject);
    }

    public void ResetScenePresist()
    {
        Destroy(gameObject);
    }
}
