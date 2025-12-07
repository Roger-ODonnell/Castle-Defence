using System;
using UnityEngine;

public class Cameralook : MonoBehaviour
{
    [SerializeField] Transform camMount;
    public float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;
    [SerializeField] Transform playerTransform;

    public bool paused = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        Camera.main.transform.SetParent(camMount);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (paused) { paused = false; }
            else
                paused = true;
        }


        if (!paused)
        {
            Look();
        }
    }

    public void Look()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
        playerTransform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    internal void togglePause()
    {
        if (paused)//true
        {
            paused = false;//false
            Cursor.visible = false;              // Makes the cursor visible
            Cursor.lockState = CursorLockMode.Locked;  // Unlocks the cursor from the center of the screen
        }
        else
        {
            paused = true;//true
            Cursor.visible = true;              // Makes the cursor visible
            Cursor.lockState = CursorLockMode.None;  // Unlocks the cursor from the center of the screen
        }
    }
}
