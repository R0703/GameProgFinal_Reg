using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 3.0f;
    float xMovement;
    float yMovement;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private Camera mainCam;

    public AudioClip attackSFX; 
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    private void spriteFlip(float xMovement)
    {
        if (xMovement < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    private void FixedUpdate()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");

        animator.SetFloat("moving", Mathf.Abs(xMovement * speed) + Mathf.Abs(yMovement * speed));
        transform.Translate(new Vector3(xMovement * speed, yMovement * speed, 0f) * Time.deltaTime);
        spriteFlip(xMovement);
    }

    private void Update()
    {
        mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, mainCam.transform.position.z);

        animator.SetFloat("Horizontal", Mathf.Abs(xMovement));
        animator.SetFloat("Vertical", yMovement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("isAttack");
        PlayAttackSFX();
    }
    private void PlayAttackSFX()
    {
        if (attackSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSFX);
        }
    }
}
