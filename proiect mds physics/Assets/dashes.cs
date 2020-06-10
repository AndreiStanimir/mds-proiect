using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashes : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (transform.name == "Dash1")
            GetComponent<Slider>().value = Dash.dash1 / Dash.dashRate;
        if (transform.name == "Dash2")
            GetComponent<Slider>().value = Dash.dash2 / Dash.dashRate;
    }
}
