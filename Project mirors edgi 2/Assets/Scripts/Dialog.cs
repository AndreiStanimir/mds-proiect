using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    
    [SerializeField] string title;
    [SerializeField] string[] sentences;
    [SerializeField] float speed;
    [SerializeField] float letterDebit = 1;
    [SerializeField] Font font;
    [SerializeField] AudioClip sound;

    GameObject player;

    GameObject dialogPanel;
    bool ceva2 = true;
    bool ceva = true;
    private AudioSource audioSource;
    public int currentString = 0;
    private int currentLetter = 0;
    private Text text;

    enum states { writing, ready, off, };
    states state;

    private void Start()
    {
        player = GameObject.Find("Player");
        state = states.off;
        dialogPanel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        text = dialogPanel.transform.GetChild(0).GetComponent<Text>();
        for (int x = 0; x < sentences.Length; x++)
            sentences[x] = sentences[x].Replace("\\n", "\n");
    }

    private void ResumeDialog()
    {
        currentLetter = 0;
        dialogPanel.SetActive(true);
        dialogPanel.transform.GetChild(1).GetComponent<Text>().text = title;
        text.text = "";
        InvokeRepeating("PrintLetter", 0.01f, speed);
        state = states.writing;
        text.font = font;
        audioSource = dialogPanel.transform.GetComponent<AudioSource>();


    }

    public void Interact()
    {
        if (!dialogPanel.activeSelf)
        {
            if (currentString >= sentences.Length - 1)
            {
                text.text = "";
                currentString = 0;
            }

            if (state == states.off)
                ResumeDialog();
        }
        ceva2 = true;


    }

    public void Stop()
    {
        CancelInvoke("PrintLetter");
        state = states.off;
        dialogPanel.SetActive(false);
        text.text = "";
        currentLetter = 0;
        currentString = 0;
        ceva = true;
    }

    public void Pause()
    {
        CancelInvoke("PrintLetter");

        state = states.off;
        dialogPanel.SetActive(false);
        text.text = "";
        currentLetter = 0;
        ceva = true;
    }

    private void Update()
    {
        if (state != states.off)
        {
            if (state == states.ready && Input.GetKeyDown(KeyCode.F))
            {
                ceva = false;
                currentString++;
                if (currentString == sentences.Length)
                    Stop();
                else
                {
                    text.text = "";
                    InvokeRepeating("PrintLetter", 0.01f, speed);
                    state = states.writing;
                }
            }

            else if (state == states.writing && Input.GetKeyDown(KeyCode.F) && !ceva)
            {
                CompleteSentence();
                currentLetter = 0;
                CancelInvoke("PrintLetter");

            }



        }
        if(ceva2)
            if ((Vector3.Distance(player.transform.position, transform.position) > 4))
            {
                Pause();
                ceva2 = false;
            }
    }

    



    void PrintLetter()
    {

        if (currentString < sentences.Length &&  currentLetter < sentences[currentString].Length)
        {
            for (int x = 0; x < letterDebit; x++)
            {
                if (currentLetter < sentences[currentString].Length)
                {
                   
                    text.text += sentences[currentString][currentLetter];
                    currentLetter++;

                }
                else break;
            }



            


            audioSource.pitch = UnityEngine.Random.RandomRange(0.9f, 1.1f);
            audioSource.PlayOneShot(sound);
        }
        if (currentLetter == sentences[currentString].Length)
        {
            state = states.ready;
            CancelInvoke("PrintLetter");
            currentLetter = 0;
        }

        

    }

    void CompleteSentence()
    {
        dialogPanel.transform.GetChild(0).GetComponent<Text>().text = sentences[currentString];
        state = states.ready;
    }




}
