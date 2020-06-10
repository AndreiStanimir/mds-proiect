using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFlowFinal : MonoBehaviour
{
    Light directionalLight;

    // Start is called before the first frame update
    void Start()
    {
        directionalLight = GameObject.Find("Directional Light").GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(directionalLight.intensity <= 1.3f)
         directionalLight.intensity += Time.deltaTime * 0.1f;
    }
}
