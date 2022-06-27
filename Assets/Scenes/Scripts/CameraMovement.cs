using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    private Vector3 playerOffset;
    private Vector3 lateralOffset;
    private bool firstRotationMade;
    private bool secondRotationMade;

    // Start is called before the first frame update
    void Start()
    {
        playerOffset = new Vector3(9, 4, -3);
        lateralOffset = new Vector3(-20, 0, 0);
        firstRotationMade = false;
        secondRotationMade = false;
        setInitialCamera();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        

        if (player.transform.position.z < 216 || player.transform.position.z > 246)
            transform.position = player.transform.position + playerOffset;
        else { 
            transform.position = player.transform.position + lateralOffset;

            if (firstRotationMade == false) {
                firstRotationMade = true;
                transform.rotation = Quaternion.Euler(-2.92f, 89.715f, -1.73f);
            }
        }
        if ((player.transform.position.z > 246) && (secondRotationMade == false)) {
            secondRotationMade = true;
            setInitialCamera();
        }

    }

    public void setInitialCamera() {
        transform.rotation = Quaternion.Euler(22.8f, -40.67f, -5.26f);
    }

    public void resetRotations() {
        firstRotationMade = false;
        secondRotationMade = false;
    }
}
