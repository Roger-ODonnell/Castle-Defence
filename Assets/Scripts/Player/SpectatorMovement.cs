using UnityEngine;

public class SpectatorMovement : MonoBehaviour
{
    Transform cameraPos;

    float movex;
    float movez;
    float movey;
    float speed = 10f;
    float yspeed = 5f;

    [Header("Components")]
    Camera m_camera;

    void Start()
    {
        m_camera = Camera.main;  // Don't keep calling Camera.main
    }

    void Update()
    {
        movex = Input.GetAxis("Horizontal") * speed;
        movez = Input.GetAxis("Vertical") * speed;



        if (Input.GetKey(KeyCode.Space))
        {
            movey = 1;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            movey = -1;
        }
        else { movey = 0; }

        movey = movey * yspeed;


        Vector3 move = new Vector3(movex *= Time.deltaTime, movey *= Time.deltaTime, movez *= Time.deltaTime);
        transform.Translate(move);
    }
}
