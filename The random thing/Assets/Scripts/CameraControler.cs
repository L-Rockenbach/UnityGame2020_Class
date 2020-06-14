using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControler : MonoBehaviour
{
    public Transform seguirPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(seguirPlayer.position.x, seguirPlayer.position.y - seguirPlayer.position.y, -40);
        
    }
}
