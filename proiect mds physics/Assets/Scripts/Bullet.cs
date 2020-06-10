using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject BulletHole;

    private GameObject k;

    void Start()
    {
        // Vector3 contact = transform.position;
        // Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact);
        // k = Instantiate(explosionEffect, contact, rotation);
        // Destroy(gameObject,0.08f);
        // //Destroy(k, 0.19f);
        // GetComponent<MeshRenderer>().enabled = false;
        // GetComponent<SphereCollider>().enabled = false;
        // GetComponent<Rigidbody>().velocity = Vector3.zero;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Player")
        {
            ContactPoint contact = collision.contacts[0];

            //  Vector3 position = contact.point;
            print(collision.gameObject.name);
            if (collision.transform.tag == "Wall" || collision.transform.tag == "Ground")
                Instantiate(BulletHole, contact.point, Quaternion.identity);
            EnemyAi ghost = collision.gameObject.GetComponentInParent<EnemyAi>();
            if(ghost!=null)
                if (ghost.tag=="Ghost")
                {
                    print("hit");
                    ghost.takeDamage();
                }
            k = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject,0.08f);
            //Destroy(k, 0.19f);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void Update()
    {
        Destroy(gameObject, 2f);
    }
}
