using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float playerSpeed = 7f;
    public ParticleSystem particle;

    private Vector3 dir;
    private bool dirTurn;
    public bool gameOver, touchDisable;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && FindObjectOfType<GroundManager>().finishMoveGround && !touchDisable)
        {
            dirTurn = !dirTurn;

            if (dirTurn)
                dir = Vector3.forward;
            else
                dir = Vector3.back;
        }

        if (!FindObjectOfType<CameraController>().startToRotateCamera)
            transform.position = transform.position + dir * playerSpeed * Time.deltaTime;

        RaycastHit hit;

        Ray rayDown = new Ray(transform.position, Vector3.down);

        if (!Physics.Raycast(rayDown, out hit, .5f))
        {
            gameOver = true;
            touchDisable = true;
            playerSpeed = 20;
            dir = Vector3.down;
        }

    }

    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Obstacle")
        {
            gameOver = true;
            touchDisable = true;
            dir = Vector3.left;
        }

        if (target.tag == "Gold")
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) + 1);
            ParticleSystem goldParticle;
            goldParticle = Instantiate(particle, target.transform.position, Quaternion.identity);
            goldParticle.Simulate(.5f, true, false);
            goldParticle.Play();
            Destroy(goldParticle, 0.5f);
            Destroy(target.gameObject);
        }
    }
}