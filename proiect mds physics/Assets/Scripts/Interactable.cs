using UnityEngine;

public class Interactable : MonoBehaviour
{
    GameObject interactAvaibleMessage;

    BoxCollider bc;
    void Start()
    {
        bc = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        bc.size += Vector3.one * 2;
        bc.isTrigger = true;
        interactAvaibleMessage = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                BroadcastMessage("Interact");
            }

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            interactAvaibleMessage.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            interactAvaibleMessage.SetActive(false);

    }

}
