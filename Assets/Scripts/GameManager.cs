using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject menuGameOver;
    public GameObject col;
    public Renderer fondo;
    public GameObject piedra1;
    public GameObject piedra2;
    public List<GameObject> cols;
    public List<GameObject> obstaculos;
    public float velocidad = 4;
    public bool gameOn;
    public bool start;
    private Animator animator;
   
    // Start is called before the first frame update
    void Start()
    {
        if (!start)
        {
            gameOn = false;
        }
        else
        {
            gameOn = true;
        }
            

        //Crear mapa
        for (int i = 0; i < 21; i++)
        {
            cols.Add(Instantiate(col, new Vector2(-10 + i, -3), Quaternion.identity)); 
        }


        //Crear piedras
        obstaculos.Add(Instantiate(piedra1, new Vector2(14, -2), Quaternion.identity));
        obstaculos.Add(Instantiate(piedra2, new Vector2(18, -2), Quaternion.identity));        
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            menuPrincipal.SetActive(false);
            start = true;
            gameOn = true;
        }

        if (start)
        {
        
            if (gameOn)
            {
                fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.11f, 0) * Time.deltaTime;

                        
                //Mover Mapa
                for(int i=0; i<cols.Count; i++)
                {

                    if (cols[i].transform.position.x <= -10)
                    {
                        cols[i].transform.position = new Vector3(10, -3,0);
                    }


                    cols[i].transform.position = cols[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
                }

                //Mover Obstaculos
                for(int i=0; i<obstaculos.Count; i++)
                {
                    if (obstaculos[i].transform.position.x <= -10)
                    {
                        float randomObstaculo = Random.Range(11, 18);
                        obstaculos[i].transform.position = new Vector3(randomObstaculo, -2, 0);
                    }

                    obstaculos[i].transform.position = obstaculos[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
                }
            }
            else
            {
                //Muestro mensaje cuando el jugador muere
                menuGameOver.SetActive(true);

                //Reiniciar juego cuando el jugador muere
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }
        }
    }
}
