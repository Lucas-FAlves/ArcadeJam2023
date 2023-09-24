using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemePlayer : MonoBehaviour
{   
    void Start()
    {
        AudioManager.instance.PlaySound("Fight_Song1");   
    }
}
