using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    
    [SerializeField] private float velocity;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * velocity * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * velocity * Time.deltaTime;

        if(moveX != 0 || moveZ != 0)
        {
            animator.SetBool("isWalking", true);
        }

        if (moveX == 0 && moveZ == 0)
        {
            animator.SetBool("isWalking", false);
        }
        transform.Translate(moveX, 0, moveZ);
    }
}
