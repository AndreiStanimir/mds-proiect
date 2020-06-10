using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public static Vector3 pistolPos, pistolRot;
    public static Vector3 akPos, akRot;
    public GameObject pistol;
    public GameObject ak;

    void Start()
    {
        pistolPos = new Vector3(0.55f, -0.42f, 1.36f);
        pistolRot = new Vector3(-4.445f, -4.3f, 0.25f); // ??

        akPos = new Vector3(1.08f, -0.08801085f, 1.86f);
        akRot = new Vector3(-2f, -24.7f, 2.32f);
        
    }

    // Update is called once per frame
    void Update()
    {
        weaponPickup(pistol, pistolPos, pistolRot);
        weaponPickup(ak, akPos, akRot);
    }

    void weaponPickup(GameObject x, Vector3 pos, Vector3 rot)
    {
        //  print(transform.GetChild(0).childCount);
        if (Vector3.Distance (transform.position, x.transform.position) < 10 && Input.GetKeyDown(KeyCode.E) && transform.GetChild(0).childCount == 1) 
        {
            
            Pistol.pistolPickup.Play(0);
            Destroy(x.GetComponent<Rigidbody>());
            x.transform.parent = transform.GetChild(0);
            x.transform.SetSiblingIndex(0);
            x.transform.localPosition = pos;
            x.transform.localRotation = Quaternion.Euler(rot); 
        }
    }

}
