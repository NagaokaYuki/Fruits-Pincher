using UnityEngine;
using System.Collections;

public class fpsControl : MonoBehaviour
{
    public int targetFrameRate;
    
    void Awake ()
    {
		QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
    
}