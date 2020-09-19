using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EsferaController : MonoBehaviour
{
    private Rigidbody2D esferaRb;
    private Animator esferaAnimator, playerAnimator;
    [SerializeField]
    private GameObject player;
    private int quica;
    public bool volta;
    // Start is called before the first frame update
    void Start()
    {
        volta = false;
        quica = 3;
        playerAnimator = player.GetComponent<Animator>();
        esferaRb = GetComponent<Rigidbody2D>();
        esferaAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(this.volta){
            Vector3 posicao = playerAnimator.gameObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position,posicao,3 * Time.deltaTime);
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D colisor){
        if(colisor.gameObject.CompareTag("parede")){
            //Vector3 posicao = playerAnimator.gameObject.transform.position;
            //Debug.Log(posicao);
            //Debug.Log(transform.position);
            //transform.position = Vector3.MoveTowards(transform.position,posicao,2 * Time.deltaTime);
            this.volta = true;
            //playerAnimator.SetFloat("semEsfera", 1f);
            //esferaRb.gameObject.SetActive(false);
            esferaRb.gameObject.SetActive(false);
            playerAnimator.SetFloat("semEsfera", 1f);
        }

        if(colisor.gameObject.CompareTag("Player")){
            esferaRb.gameObject.SetActive(false);
            playerAnimator.SetFloat("semEsfera", 1f);
            this.volta = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision2D){
        if(collision2D.gameObject.CompareTag("gruda")){
            esferaRb.velocity = new Vector2(0, 0);
            esferaRb.mass = 0;
            esferaRb.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision2D){
        //esferaRb.velocity = new Vector2(0, 0);
        esferaRb.mass = 1;
        esferaRb.gravityScale = 1;
    }
}
