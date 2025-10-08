using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    private PlayerMove playerMove;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            playerMove.SetCanMove(false);
            animator.SetBool("isMoving", false);
            Debug.Log("Wall‚ÉÚG ¨ ’â~");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            playerMove.SetCanMove(true);
            Debug.Log("Wall‚©‚ç—£‚ê‚½ ¨ ˆÚ“®‰Â”\");
        }
    }
}
