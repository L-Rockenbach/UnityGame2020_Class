using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    public GameObject damageText;

    public void PlayerHit(string value)
    {
        var dano = Instantiate(damageText, transform.position, Quaternion.identity);
        dano.SendMessage("Text", value);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
