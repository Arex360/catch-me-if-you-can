using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSystem : MonoBehaviour
{
    public float delay;

    void Start()
    {
     Invoke(nameof(resetAbilities),delay);
    }
    public void resetAbilities(){
        abilitiesSystem.instance.DisableAllAbilities();
        CamManager.instance.EnableMainCam();
    }
}
