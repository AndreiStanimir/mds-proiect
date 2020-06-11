using UnityEngine;

public class PlayerNewMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed = 50f;
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] public static bool isGrounded;
    int jumped = 0;
    Rigidbody Player;
    //readonly float distToGround;
    Vector3 crouchPos, notCrouchPos;
    bool canJump = false;
    bool canJumpTemp;
    public static bool canGetVelocity = true;
    float taim;
    WallRunV2 wrv2;
    AudioSource footsteps;
    AudioSource landing;
    AudioSource wind;
    AudioSource jumpSound;
    public static AudioSource hookSound;
    public static AudioSource dashSound;
    CapsuleCollider body;

    void Start()
    {
        body = GetComponent<CapsuleCollider>();
        wrv2 = GetComponent<WallRunV2>();
        Player = GetComponent<Rigidbody>();
        notCrouchPos = transform.GetChild(0).transform.localPosition;
        crouchPos = transform.GetChild(0).transform.localPosition - new Vector3(0, 0.4f, 0);
        AudioSource[] audioSources = GetComponents<AudioSource>();
        footsteps = audioSources[0];
        landing = audioSources[1];
        wind = audioSources[2];
        jumpSound = audioSources[3];
        hookSound = audioSources[4];
        dashSound = audioSources[5];
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log(Player.velocity);
        if (canGetVelocity)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Player.velocity += transform.forward * Time.fixedDeltaTime * movementSpeed;
                if (!footsteps.isPlaying && isGrounded)
                    SoundManager.PlayCustom(footsteps, 0.05f);
            }

            if (Input.GetKey(KeyCode.S))
            {
                Player.velocity -= transform.forward * Time.fixedDeltaTime * movementSpeed;
                if (!footsteps.isPlaying && isGrounded)
                    SoundManager.PlayCustom(footsteps, 0.05f);
            }

            if (Input.GetKey(KeyCode.D))
            {
                Player.velocity += transform.right * Time.fixedDeltaTime * movementSpeed;
                if (!footsteps.isPlaying && isGrounded)
                    SoundManager.PlayCustom(footsteps, 0.05f);
            }

            if (Input.GetKey(KeyCode.A))
            {
                Player.velocity -= transform.right * Time.fixedDeltaTime * movementSpeed;
                if (!footsteps.isPlaying && isGrounded)
                    SoundManager.PlayCustom(footsteps, 0.05f);

            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
            {
                Vector3 X = Player.velocity;
                Player.velocity = new Vector3(X.x, 0, X.z);
                Player.velocity += transform.up * jumpHeight * 2.2f;
                jumped = 1;
                SoundManager.PlayCustom(jumpSound, 0.05f, -0.64f);

            }
            else if (jumped == 1 && Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 X = Player.velocity;
                Player.velocity = new Vector3(X.x, 0, X.z);
                Player.velocity += transform.up * jumpHeight * 2.2f;
                jumped = 2;
                SoundManager.PlayCustom(jumpSound, 0.05f, -0.54f);  //sunet mai inalt la doublejump, poti sa scoti daca vrei punand -0.64 in loc de 54
            }
            else if (!isGrounded && jumped == 0)
            {
                Vector3 X = Player.velocity;
                Player.velocity = new Vector3(X.x, 0, X.z);
                Player.velocity += transform.up * jumpHeight * 2.2f;
                jumped = 2;
                SoundManager.PlayCustom(jumpSound, 0.05f, -0.54f);
            }
            else if (!isGrounded && canJump)
            {
                Vector3 X = Player.velocity;
                Player.velocity = new Vector3(X.x, 0, X.z);
                Player.velocity += transform.up * jumpHeight * 2.2f;
                canJumpTemp = false; canJump = false;
                jumped = 2;
                SoundManager.PlayCustom(jumpSound, 0.05f - 0.54f);

            }
        }

        if (GrapplingHook.isGrappling && Time.time - taim > 2)
        {
            canJumpTemp = true;
            taim = Time.time;
        }
        if (!GrapplingHook.isGrappling && canJumpTemp == true)
            canJump = true;



        if (!isGrounded)
            Player.drag = 2;
        else
            if (Input.GetKey(KeyCode.C))
        { // crouch
            Player.drag = 4;
            transform.GetChild(0).transform.localPosition = crouchPos; // positions of weapons
            body.radius = 0.7f;
            body.height = 1.5f;
        }
        else
        {
            Player.drag = 1.5f;
            transform.GetChild(0).transform.localPosition = notCrouchPos;
            body.radius = 1;
            body.height = 2.25f;
        }


        if (!isGrounded)
            wind.volume = 0.05f;
        else
            wind.volume = 0.02f;

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            canGetVelocity = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = false;

        if (collision.collider.CompareTag("Wall"))
            canGetVelocity = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") && !isGrounded) // wall running mode
            canGetVelocity = false;

        if (collision.collider.CompareTag("Ground"))
        {

            landing.Play(0);
        }
    }

}
