using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{
    [SerializeField] float sensitivity = 100f;
    float xRotation = 0f;
    Transform playerBody;
    public float cameraShake;
    public float wallRunTilt = 0;
    public GameObject weapon;
    float deltaRotationX = 0f;
    float deltaRotationY = 0f;
    float mouseX, mouseY;
    float schema = 0;

    void Start()
    {
        playerBody = transform.parent.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation - cameraShake, 0f, wallRunTilt);

        RotateWithMouseWeapon();
        WeaponMove();
        
    }

    void RotateWithMouseWeapon()
    {
        if (transform.childCount != 0)
        {
            weapon = transform.GetChild(0).gameObject;
        
            if (weapon.name == "AKau")
            {
                weapon.transform.localRotation = Quaternion.Euler(WeaponPickup.akRot.x + mouseY / 3, WeaponPickup.akRot.y, WeaponPickup.akRot.z + mouseX / 4);
            }
            if (weapon.name == "PISTOL")
            {
                weapon.transform.localRotation = Quaternion.Euler(WeaponPickup.pistolRot.x + mouseY / 1.5f, WeaponPickup.pistolRot.y, WeaponPickup.pistolRot.z + mouseX / 1.5f);
            }
        }
    }

    void WeaponMove()
    {
        if (transform.childCount != 0 && PlayerNewMovement.isGrounded)
        {
            weapon = transform.GetChild(0).gameObject;

            if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space)) && schema <= 0)
            {
                schema = 0.02f;
            }

            if (schema > 0)
                schema -= 0.001f;

            // print(schema);
            if (weapon.name == "AKau")
            {
                weapon.transform.localPosition = new Vector3(WeaponPickup.akPos.x, WeaponPickup.akPos.y - schema, WeaponPickup.akPos.z);
            }
            if (weapon.name == "PISTOL")
            {
                weapon.transform.localPosition = new Vector3(WeaponPickup.pistolPos.x, WeaponPickup.pistolPos.y - schema, WeaponPickup.pistolPos.z);
            }
        }
    }
}
