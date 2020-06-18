using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour

{
   // private float gravidade;
   // private float velocidadePulo;

    public float alturaPulo = 4f;
    public float tempoParaApicePulo = 0.4f;


    [Header("Movement variables")]
    public float speed = 10f;
    public float radius = 0.2f;
    public float jumpForce = 200f;
    public LayerMask groundMask;
    public int maximumLife = 3;
    public Text coracaozinho;

    

    [Header("Attack variables")]
    public int damage = 10;
    public Transform attackCheck;
    public float radiusAttack;
    public LayerMask Enemy;
    public LayerMask FallLimit;
    float coolDownAttack;
    public float timeForDie = 1f;
 

    string atualLife;
    string danoFlutuante;
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

        /*gravidade = -(alturaPulo * 2) / Mathf.Pow(tempoParaApicePulo, 2);
        velocidadePulo = Mathf.Abs(gravidade) * tempoParaApicePulo;*/

    }

    // Update is called once per frame
    void Update()
    {
        /*if (!body.IsTouchingLayers())
        {
            body.velocity = new Vector2(body.velocity.x, gravidade);
        }*/

        inFloor = body.IsTouchingLayers(groundMask);
        if (Input.GetButtonDown("Jump") && inFloor == true)
            Jumping = true;

        damageConverter = damage * -1;
        danoFlutuante = damageConverter.ToString();
        
        timeForDie -= Time.deltaTime;
        Animation();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.name);
        if (collision.collider.tag == "Enemy")
        {
            if (timeForDie <= 0f)
            {
                maximumLife -= 1;
                atualLife = maximumLife.ToString();
                coracaozinho.text = atualLife;
                print("Dano");
                timeForDie = 1f;
            }
        }
        if (collision.collider.tag == "WasWeKnowIt" || maximumLife == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            print("Você morreu");
            //Destroy(gameObject, 0.2f);
        }
       /* if(collision.collider.tag == "Ground")
        {
            inFloor = true;
            if (Input.GetButtonDown("Jump") && inFloor == true)
                Jumping = true;
            print(Jumping);
        }*/
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
            enemiesAttack[i].SendMessage("PlayerHit", danoFlutuante);
            enemiesAttack[i].SendMessage("PlayerDamage", damage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, radiusAttack);
    }
    

}
