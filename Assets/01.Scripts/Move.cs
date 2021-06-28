using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Move : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpForce = 1.0f;
    private Rigidbody rigid;
    private bool isJumping;

    public delegate void CoinEvent(int coinAmount);
    public CoinEvent onCoinGained;

    private void Start() {
        rigid = GetComponent<Rigidbody>();   
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;

        transform.Translate(dir * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJumping)
            {
                rigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                isJumping = true;
            }
        }
    }

    

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Ground")
        {
            if(isJumping)
            {
                isJumping = false;
            }
        }

        if(other.gameObject.tag =="Coin")
        {
            Destroy(other.gameObject);
            onCoinGained?.Invoke(1);
        }    
    }
}
