using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamActivator : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.instance.isCinematicScene = false;
    }
}
