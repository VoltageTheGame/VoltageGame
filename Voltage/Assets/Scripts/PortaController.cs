using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaController : MonoBehaviour
{
    public GameObject parede;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision2D){
        if(collision2D.gameObject.CompareTag("esfera")){
            parede.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision2D){
        if(collision2D.gameObject.CompareTag("esfera")){
            parede.SetActive(true);
        }
    }
}
