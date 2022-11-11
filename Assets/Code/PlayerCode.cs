using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCode : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float moveConstant;
    private float xSpeed = 0;
    private float ySpeed = 0;
    bool dodging = false;
    bool attacking = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //we need movement, a dodge [can just be a roll animation and during the roll animation you cant take damage], and a melee attack
    }

    void FixedUpdate() {
        //Movement Code
        xSpeed = Input.GetAxisRaw("Horizontal") * speed;
        Vector2 movement = new Vector2(xSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, movement, moveConstant);
    }
}
