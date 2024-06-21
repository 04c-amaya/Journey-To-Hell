using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScenery : MonoBehaviour
{
    float lastX;
    float currentX;
    float delataX;
    public List<Background> backgroundObjects;
    void Start()
    {
        lastX = transform.position.x;
    }


    void Update()
    {

        currentX = transform.position.x;
        delataX = currentX - lastX;
        for (int i = 0; i < backgroundObjects.Count; i++)
        {
            backgroundObjects[i].background.transform.Translate(delataX * backgroundObjects[i].multiplier, 0, 0);
        }
        lastX = currentX;
    }
}
[System.Serializable]
public class Background
{
    public GameObject background;
    public float multiplier;
}

