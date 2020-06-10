using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScript : MonoBehaviour
{
    Text castText;
    bool isEnding = false;

    // Start is called before the first frame update
    void Start()
    {
        castText = GameObject.Find("FinalText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnding)
            castText.rectTransform.transform.Translate(Vector3.up);
    }

    

    public void DisableThings()
    {
        GetComponent<PlayerMovement>().enabled = false;
        transform.GetChild(0).GetComponent<CameraLook>().enabled = false;
        isEnding = true;
    }
}
