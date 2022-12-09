using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCode : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float moveConstant;
    private float xSpeed = 0;
    private float ySpeed = 0;
    private float stamina = 150;
    //bool cooldown = false;
    bool dodging = false;
    bool attacking = false;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //we need movement, a dodge [can just be a roll animation and during the roll animation you cant take damage], and a melee attack

    }

    void FixedUpdate() {
        //Stamina Code (work in progress)
        if (stamina < 150 && !attacking) {
            stamina++;
        }
        

        //Movement Code
        if (!dodging && !attacking) {
            xSpeed = Input.GetAxisRaw("Horizontal") * 5;
            Vector2 movement = new Vector2(xSpeed, _rigidbody.velocity.y);
            _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, movement, moveConstant);
        }

        //Attack Code
        if (Input.GetButtonDown("Fire1") && !attacking && !dodging)
        {  
            attacking = true;
            Debug.Log("Attacking");
            StartCoroutine(SwingDelay());
        }

        //Roll Code
        if (Input.GetButtonDown("Jump") && !attacking && !dodging)
        {  
            dodging = true;
            Debug.Log("Rolling");
            StartCoroutine(RollDelay());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && !dodging){
            //take damage
            Debug.Log("Damage!");
        }
    }

    IEnumerator SwingDelay() {
        yield return new WaitForSeconds(0.6f);
        //attack code
        Debug.Log("Attack!");
        yield return new WaitForSeconds(0.6f);
        attacking = false;
    }

    IEnumerator RollDelay() {
        yield return new WaitForSeconds(0.3f);
        //roll code
        //add constant velocity for period of time
        Debug.Log("Dodge!");
        dodging = false;
    }

    void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}

