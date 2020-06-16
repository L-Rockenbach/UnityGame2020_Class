using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer sprite;

    public GameObject damageText;
    public float speed;
    float flipTime = 5f;
    public float life = 10;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void PlayerHit(string value)
    {
        var dano = Instantiate(damageText, transform.position, Quaternion.identity);
        dano.SendMessage("Text", value);
    }
    // Update is called once per frame
    void Update()
    {
        flipTime -= Time.deltaTime;
        body.velocity = new Vector2(speed, body.position.y);
        if (body.position.x > 373.8f && body.position.x > 459.3f)
        {
            if (flipTime <= 0f)
            {
                Flip();
                flipTime = 5f;
            }
            else
            {
                flipTime -= Time.deltaTime;
            }
        }
        else if(body.position.x < 373.8f && body.position.x < 459.3f)
        {
            if (flipTime <= 0f)
                Flip();
                flipTime = 5f;
        }
        else
        {
            flipTime -= Time.deltaTime;
        }
    }
    void Flip()
    {
        speed *= -1;
        sprite.flipX = !sprite.flipX;
    }
    void PlayerDamage()
    {
        Destroy(gameObject, 0.2f);
        //if(Player.damage - life <= 0f)
        //{
        //    Destroy(gameObject);
        //}
    }

}
