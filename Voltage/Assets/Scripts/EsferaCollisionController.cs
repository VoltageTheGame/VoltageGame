using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsferaCollisionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            Destroy(collision.gameObject);
        }
    }

    //Usar para ativar o objeto, dps da colisão
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Trocar o sprite do objeto
        //Talves tenha que ser o Enter nao o Exit
    }
}
