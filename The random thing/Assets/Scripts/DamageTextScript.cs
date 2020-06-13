using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextScript : MonoBehaviour
{

    public Text damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
        
    }

    // Update is called once per frame
    public void Text(string value)
    {
        damage.text = value;
    }
}
