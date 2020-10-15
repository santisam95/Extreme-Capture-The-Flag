using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody character;
    private Transform groundChecker;
    private bool isGrounded;
    private Vector3 movement = Vector3.zero;
    private bool alive = true;

    public float jumpForce = 350f;
    public float speed = 2.0f;
    public LayerMask Ground;
    public Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Rigidbody>();
        character.freezeRotation = true;
        groundChecker = transform.GetChild(0);
        spawnPosition = this.transform.position;
    }

    private void Update()
    {
        if (alive)
        {
            isGrounded = Physics.CheckSphere(groundChecker.position, 0.2f, Ground, QueryTriggerInteraction.Ignore);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                character.AddForce(Vector3.up * jumpForce);
            }

            movement = Vector3.zero;
            movement.x = Input.GetAxis("Horizontal") * speed;
            movement.z = Input.GetAxis("Vertical") * speed;

            if (movement != Vector3.zero)
            {
                transform.forward = movement;
            }
        }
    }

    void FixedUpdate()
    {
        character.MovePosition(transform.position + (movement * Time.deltaTime * speed));
    }

    public void Die()
    {
        if (alive)
        {
            StartCoroutine(OnDeath());
        }
    }

    private IEnumerator OnDeath()
    {
        alive = false;
        //Death animation here
        this.transform.position = new Vector3(5000, 5000, 5000);
        yield return new WaitForSeconds(5);
        //Respawn
        this.transform.position = spawnPosition;
        alive = true;
    }
}
