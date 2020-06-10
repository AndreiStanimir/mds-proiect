using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFlow_Story1 : MonoBehaviour
{

    [SerializeField] GameObject cube;
    [SerializeField] GameObject playerLantern;
    [SerializeField] GameObject citizen;
    [SerializeField] GameObject ghost;
    [SerializeField] GameObject[] postProcessingProfiles = new GameObject[3];
    Transform player;
    Light warpLight;
    AudioSource music;
    AudioSource ghostSound;

    bool lanternAquired = false;
    bool cubeGone = false;

    enum PPState { normal, crazy1, crazy2};
    PPState crazyState;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        warpLight = GameObject.Find("Directional Light").GetComponent<Light>();
        ghostSound = GameObject.Find("GhostSound").GetComponent<AudioSource>();
        music = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!lanternAquired &&cube.GetComponent<Dialog>().currentString == 4 )
        {
            playerLantern.SetActive(true);
            lanternAquired = true;
        }
        if(!cubeGone && cube.GetComponent<Dialog>().currentString == 12)
        {
            cubeGone = true;
            
            ghostSound.Play();
        }
        if(cubeGone)
        {
            cube.transform.Translate(Vector3.forward * Time.deltaTime * -10);
        }
        if(player.position.x < -38 && crazyState == PPState.normal)
        {
            crazyState = PPState.crazy1;
            postProcessingProfiles[0].SetActive(false);  //can be made a function SetPPProfile(index)
            postProcessingProfiles[1].SetActive(true);
            postProcessingProfiles[2].SetActive(false);
        }
        if (player.position.x < -58 && crazyState == PPState.crazy1)
        {
            crazyState = PPState.crazy2;
            postProcessingProfiles[0].SetActive(false); 
            postProcessingProfiles[1].SetActive(false);
            postProcessingProfiles[2].SetActive(true);
        }
        if(player.position.x < -64)
        {
            warpLight.intensity += Time.deltaTime * 10;
        }

        // mergea functie care facea sa fie de la 0 la 1, aka normalizare si d-astea

        ghostSound.volume = (1 - Mathf.Abs((-73 - player.position.x - 20) / 100) + 0.1f)/8;
        ghostSound.pitch = 1 - (1 - Mathf.Abs((-73 - player.position.x - 20) / 100) + 0.1f) / 6;
        music.pitch = 1 + (1 - Mathf.Abs((-73 - player.position.x - 20) / 100) + 0.1f) / 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            citizen.SetActive(false);
            ghost.SetActive(true);
        }
    }

}
