using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour

{   [Header("Movement variables")]
    public float speed = 10f;
    public float radius = 0.2f;
    public float jumpForce = 200f;
    public LayerMask groundMask;
    public int maximumLife = 3;
    public int atualLife;

    [Header("Attack variables")]
    public int damage = 10;
    public Transform attackCheck;
    public float radiusAttack;
    public LayerMask Enemy;
    float coolDownAttack;
    public float timeForDie = 5f;
    public bool IsAlive = true;
    public Text damageText;

    int damageConverter;
    bool Jumping = false;
    bool inFloor = false;
    Rigidbody2D body;
    SpriteRenderer sprite;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //damage = damage * -1;

    }

    // Update is called once per frame
    void Update()
    {

        inFloor = body.IsTouchingLayers(groundMask);
        if (Input.GetButtonDown("Jump") && inFloor == true)
            Jumping = true;

        if (body.IsTouchingLayers(Enemy))
        {
            if (timeForDie <= 0f)
            {
                atualLife -= maximumLife;
                print("Dano");
                timeForDie = 5f;
            }
        }

        damageConverter = damage * -1;
        damageText.text = damageConverter.ToString();
        
        timeForDie -= Time.deltaTime;
        Animation();
    }

    private void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal");
        if (!Input.GetButton("Fire1"))
        {
            body.velocity = new Vector2(movement * speed, body.velocity.y);
        }

        if (coolDownAttack <= 0)
        {
            if (Input.GetButton("Fire1") && body.velocity.y == 0)
            {
                body.velocity = new Vector2(0, body.velocity.y);
                animator.SetTrigger("atack");
                coolDownAttack = 0.2f;
                //playerAttack();
            }
        }
        else
        {
            coolDownAttack -= Time.deltaTime;
        }

        if ((movement > 0 && sprite.flipX == true) || (movement < 0 && sprite.flipX == false)){
            sprite.flipX = !sprite.flipX;
            attackCheck.localPosition = new Vector2(-attackCheck.localPosition.x, attackCheck.localPosition.y);

        }

        if (Jumping)
        {
            body.AddForce(new Vector2(0f, jumpForce));
            Jumping = false;
        }

        if(body.velocity.y > 0f && !Input.GetButton("Jump"))
        {
            body.velocity += Vector2.up * -0.8f;
        }


    }

    void Animation()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(body.velocity.x));
        animator.SetFloat("yVelocity", Mathf.Abs(body.velocity.y));

    }

    public void playerAttack()
    {
        Collider2D[] enemiesAttack = Physics2D.OverlapCircleAll(attackCheck.position, radiusAttack, Enemy);
        for(int i = 0; i< enemiesAttack.Length; i++)
        {

            print(enemiesAttack[i].name);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, radiusAttack);
    }

}
