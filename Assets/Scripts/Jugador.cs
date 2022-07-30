using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    public float fuerzaSalto;
    public Rigidbody2D rigidbody2D;
    private Animator animator;
    public GameManager gameManager;
    public int contador_vidas = 3;
    public GameObject[] hearts;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        //rigidbody2D = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.gameOn) 
        { 
            if((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.UpArrow)))
            {
                UpdateState("Saltar");
                rigidbody2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                rigidbody2D.AddForce(Vector2.down * fuerzaSalto, ForceMode2D.Impulse);
            }
            
        }

        //Borro el corazón de la vida en el juego
        if(contador_vidas < 3)
        {
            Destroy(hearts[contador_vidas]);
        }
        

    }

    public void UpdateState(string state = null)
    {
        if (state != null)
        {
            animator.Play(state);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameManager.gameOn)
        {        
            if(collision.gameObject.tag == "Suelo")
            {               
                UpdateState("Correr");
            }

            if(collision.gameObject.tag == "Obstaculo")
            {

                contador_vidas--;

                if(contador_vidas <= 0)
                {
                    UpdateState("Morir");
                    rigidbody2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                    gameManager.gameOn = false;
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                
            }
        }
    }
}
