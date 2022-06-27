using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    private Renderer playerRend;
    public CameraMovement myCamera;

    AudioSource crashSound;
    AudioSource levelMusic;
    public GameObject explosionParticle;

    public float speed = 20;
    public float jumpForce;
    public float gravityModifier;
    public float velRotation = 0.2f;
    public bool canJump = true;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRend = GetComponent<Renderer>();
        Physics.gravity *= gravityModifier;

        levelMusic = GetComponent<AudioSource>();
        crashSound = GameObject.FindGameObjectWithTag("CrashSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = playerRb.velocity;

        if (!isDead)
        {
            vel.z = 10;
        }
        else {
            vel.z = 1;
        }
        
        playerRb.velocity = vel;

        if (!isDead) { 
            // Rotate in the x axis while jumping
            if (Physics.Raycast(transform.position, Vector3.down, GetComponent<BoxCollider>().size.y / 2 + 0.4f))
            {
                Quaternion rot = transform.rotation;
                rot.x = Mathf.Round(rot.z / 90) * 90;
                transform.rotation = rot;
                
                if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))&& (canJump == true))
                {
                    playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    canJump = false;
                }
            }
            else {
                transform.Rotate(Vector3.right * velRotation * Time.deltaTime);
            }
        


            // Keyboard movement
            if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 1)
            {
                vel.x = 10;
                playerRb.velocity = vel;
            }
            else if (Input.GetKeyDown(KeyCode.A) && transform.position.x > 0)
            {
                vel.x = -10;
                playerRb.velocity = vel;
            }
            


            // Constraints
            if (transform.position.x > 1)
            {
                transform.position = new Vector3(1, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < 0)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
            else if (transform.position.y < -10)
            {
                DieByFalling();
            }



            // Player colors
            if (transform.position.x == 0) {
                playerRend.material.color = new Color(204 / 255f, 153 / 255f, 255 / 255f);
            } else if (transform.position.x == 1)
            {
                playerRend.material.color = new Color(255 / 255f, 153 / 255f, 255 / 255f);
            }
        
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
    }

    public void die() {
        isDead = true;
        Debug.Log("You died!");
    }

    public void revive()
    {
        isDead = false;
        myCamera.setInitialCamera();
        myCamera.resetRotations();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    void Explode()
    {
        GameObject explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
    }

    public void DieByFalling() {
        die();
        levelMusic.Stop();
        crashSound.Play();
        Explode();
        StartCoroutine(WaitSeconds());
    }

    public IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2f);

        revive();

        if (transform.position.x < 0.5)
            transform.position = new Vector3(0, 1, -2);
        else
        {
            transform.position = new Vector3(1, 1, -2);
        }

        // Replay Music 
        levelMusic.Play();
        crashSound.Stop();
    }
}
