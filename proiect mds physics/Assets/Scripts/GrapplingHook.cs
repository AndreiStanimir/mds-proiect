using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] Material mat;
    Rigidbody rb;
    RaycastHit grapplePoint;
    RaycastHit grapplePoint2;
    public static bool isGrappling = false;
    float distance; //keeps track of the lenght of your "rope"
    public float grappleSpeed = 5f; //lets you control how fast you want your grappling hook to be
    public float grappleDist = 100f;
    public GameObject hookPoint;

    void Start()
    {
        // Get Camera and Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ray from camera into the scene
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        hookPoint.transform.localScale = Vector3.one * 0.25f + Vector3.one * Vector3.Distance(transform.position, hookPoint.transform.position) / 40;

        // the hook point
        if (Physics.Raycast(ray, out grapplePoint2) && Vector3.Distance(transform.position, grapplePoint2.point) < grappleDist && !isGrappling
            && grapplePoint2.collider.CompareTag("grapple"))
        {
            hookPoint.SetActive(true);
            if (!isGrappling)
                hookPoint.transform.position = grapplePoint2.point;
        }
        else if (!isGrappling)
            hookPoint.SetActive(false);

        // Check if a button is pressed and if the Raycast hits something and if it's a good distance
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out grapplePoint) && Vector3.Distance(transform.position, grapplePoint.point) < grappleDist
            && grapplePoint2.collider.CompareTag("grapple"))
        {
            PlayerNewMovement.hookSound.Play(0);
            Vector3 grappleDirection = grapplePoint.point - transform.position;
            isGrappling = true;
        }


        //turn grappling mode off when the button is released
        if (Input.GetMouseButtonUp(1))
        {
            isGrappling = false;
        }

        //when in grappling mode (Thats when the magic happens :D)
        if (isGrappling)
        {

            //Get Vector between player and grappling point
            Vector3 grappleDirection = grapplePoint.point - transform.position;
            DrawLine(transform.position, grapplePoint.point);


            if (distance < grappleDirection.magnitude)
            // With this you can determine if you are overshooting your target.
            // You are basically checking, if you are further away from your target than during the last frame
            {
                float velocity = rb.velocity.magnitude;//How fast you are currently

                Vector3 newDirection = Vector3.ProjectOnPlane(rb.velocity, grappleDirection);//So this is a bit more complicated
                //basically I am using the grappleDirection Vector as a normal vector of a plane.
                //I am really bad at explaining it. Partly due to my bad english but it is best if you just look up what Vector3.ProjectOnPlane does.

                rb.velocity = newDirection.normalized * velocity;//Now I just have to redirect the velocity

            }
            else//So this part is executed when you are grappling towards the grappling point
                rb.AddForce(grappleDirection.normalized * grappleSpeed);//I use addforce just to keep the velocity rather constant. You can fiddle around with the forcemodes a bit to get better results

            //Calculate distance between player and grappling point
            distance = grappleDirection.magnitude;

        }
    }

    void DrawLine(Vector3 start, Vector3 end, float duration = 0.02f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = mat;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

}