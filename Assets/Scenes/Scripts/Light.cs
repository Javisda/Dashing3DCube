using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{

    public GameObject player;
    private Vector3 playerOffset;

    // Start is called before the first frame update
    void Start()
    {
        playerOffset = new Vector3(-3, 4, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + playerOffset;
    }
}
