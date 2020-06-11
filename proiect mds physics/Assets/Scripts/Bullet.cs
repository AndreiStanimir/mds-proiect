using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject BulletHole;
    private GameObject k;
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Player"))
        {
            ContactPoint contact = collision.contacts[0];
            print(collision.gameObject.name);
            if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Ground"))
            {
                Instantiate(BulletHole, contact.point, Quaternion.identity);
            }
            EnemyAi ghost = collision.gameObject.GetComponentInParent<EnemyAi>();
            if (ghost != null)
                if (ghost.CompareTag("Ghost"))
                {
                    print("hit");
                    ghost.TakeDamage();
                }
            k = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.08f);
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
