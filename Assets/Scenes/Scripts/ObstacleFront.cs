using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleFront : MonoBehaviour
{
    public PlayerMovement myPlayer;
    public GameObject explosionParticle;

    AudioSource levelMusic;
    AudioSource crashSound;

    // Start is called before the first frame update
    void Start()
    {
        levelMusic = myPlayer.GetComponent<AudioSource>();
        crashSound = GameObject.FindGameObjectWithTag("CrashSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
            

    }

    private void OnCollisionEnter(Collision collision)
    {
        levelMusic.Stop();
        crashSound.Play();
        myPlayer.die();

        if (collision.collider.CompareTag("Player")){ 
            Explode();
            StartCoroutine(WaitSeconds());
        };

    }

    void Explode() {
        GameObject explosion = Instantiate(explosionParticle, myPlayer.transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
    }

    public IEnumerator WaitSeconds() {
        yield return new WaitForSeconds(2f);

        myPlayer.revive();

        if (myPlayer.transform.position.x < 0.5)
            myPlayer.transform.position = new Vector3(0, 1, -2);
        else
        {
            myPlayer.transform.position = new Vector3(1, 1, -2);
        }

        // Replay Music 
        levelMusic.Play();
        crashSound.Stop();
    }
}
