using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationArrowController : MonoBehaviour
{

    [SerializeField]
    private Transform posStart;
    [SerializeField]
    private Image setaImg;

    public float zRotation;
    public bool liberaRot;
    public bool liberaChuteArremeso ;

    /// <summary>
    /// cicle lyfe
    /// </summary>
    void Start()
    {
        PosicionaSeta();
        PosicionaEsfera();
    }

    void Update()
    {
        RotacaoSeta();
    }

    private void FixedUpdate()
    {
        InputRotacao();
        LimitaRotacao();
    }

    private void OnMouseDown()
    {
        this.liberaRot = !this.liberaRot;
    }

    private void OnMouseUp()
    {
        this.liberaRot = !this.liberaRot;
        this.liberaChuteArremeso = !this.liberaChuteArremeso;
    }

    /// <summary>
    /// 
    /// </summary>
    private void PosicionaSeta()
    {
        //testar com outro componente UI
        //precisa colocar o "Pivolt" "x" no (center left) ou setar o X como 0
        setaImg.rectTransform.position = posStart.position;
    }

    private void PosicionaEsfera()
    {
        this.gameObject.transform.position = posStart.position;
    }

    private void RotacaoSeta()
    {
        setaImg.rectTransform.eulerAngles = new Vector3(0, 0, this.zRotation);
    }

    private void InputRotacao()
    {
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    zRotation += 2.5f;
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    zRotation -= 2.5f;
        //}

        if (liberaRot) {

            //float moveX = Input.GetAxis("Mouse X");
            float moveY = Input.GetAxis("Mouse Y");
            moveY = moveY*-1;
            if (zRotation < 90 && moveY > 0)
            {
                zRotation += 2.5f;
            }
            if(zRotation > 0 && moveY < 0)
            {
                zRotation -= 2.5f;
            }
        }
    }

    private void LimitaRotacao()
    {
        if (zRotation >= 90)
        {
            zRotation = 90;
        }
        if(zRotation <= 0)
        {
            zRotation = 0;
        }
    }
}
