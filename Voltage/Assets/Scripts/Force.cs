using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Force : MonoBehaviour
{

    private Rigidbody2D esfera;
    private float force = 0f;
    [SerializeField]
    private PlayerController rotate;
    public Image flexaTwo;

    // Start is called before the first frame update
    void Start()
    {
        esfera = GetComponent<Rigidbody2D>();
        //rotate = GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlForce();
        AplicaForce();
    }

    private Tuple<float, float> CalculeTrigonometria()
    {
        float x,y;

        //Deg2Rad = constante de conversao de graus para radianos
        x = force * (Mathf.Cos(rotate.zRotation * Mathf.Deg2Rad));
        y = force * (Mathf.Sin(rotate.zRotation * Mathf.Deg2Rad));

        return new Tuple<float, float>(x, y);
    }

    private void AplicaForce()
    {
        Tuple<float, float> tuple;
        tuple = this.CalculeTrigonometria();

        if (rotate.liberaChuteArremeso){
            esfera.AddForce(new Vector2(tuple.Item1, tuple.Item2));
            rotate.liberaChuteArremeso = !rotate.liberaChuteArremeso;
        }
    }

    private void ControlForce()
    {
        if (rotate.liberaRot)
        {
            float moveX = Input.GetAxis("Mouse X");

            if(moveX < 0)
            {
                //fillAmount = preenchimento da flexa
                flexaTwo.fillAmount += 1 * Time.deltaTime;
                force = flexaTwo.fillAmount * 1000;
            }else if (moveX > 0)
            {
                flexaTwo.fillAmount -= 1 * Time.deltaTime;
                force = flexaTwo.fillAmount * 1000;
            }
        }
    }
}
