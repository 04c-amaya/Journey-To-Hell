using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public List<CameraLevel> level;

    [SerializeField] Camera cam;
     int i = 1;

    void Update()
    {
        ChangeCamera();
    }

    void ChangeCamera()
    {
            if (transform.position.y < level[0].maxHeight)
            {
                MoveCamera(level[0].cMvt.xMin, level[0].cMvt.xMax, level[0].cMvt.yMin, level[0].cMvt.yMax);
           //Debug.Log("0");
            }
            else 
            {
            if (i < level.Count)
            {
                if (transform.position.y > level[i-1].maxHeight && transform.position.y < level[i].maxHeight)
                {
                    MoveCamera(level[i].cMvt.xMin, level[i].cMvt.xMax, level[0].cMvt.yMin, level[i].cMvt.yMax);
                    //Debug.Log(i);
                }
                else
                {
                    if(transform.position.y > level[i].maxHeight)
                    {
                        i++;
                       // Debug.Log("+");
                    }
                    if (i > 0)
                    {
                        if (transform.position.y < level[i - 1].maxHeight)
                        {
                            i--;
                           // Debug.Log("-");
                        }
                    }
                 
                }
            }
        }
    }
    void MoveCamera(float _xMin, float _xMax, float _yMin, float _yMax)
    {
        float x = Mathf.Clamp(transform.position.x, _xMin, _xMax);
        float y = Mathf.Clamp(transform.position.y, _yMin, _yMax);
        cam.transform.position = new Vector3(x, y, gameObject.transform.position.z-10);
    }
}
[System.Serializable]
public class CameraLevel
{
    public CameraMovement cMvt;
    public int maxHeight;

}
[System.Serializable]
public class CameraMovement
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
}
