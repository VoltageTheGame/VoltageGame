using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;

    public GameObject Player;

    Vector3 nextPos;
    void Start()
    {
        nextPos = startPos.position;
    }

    void Update()
    {
        if(transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if(transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position,nextPos,speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position,pos2.position);
    }
/*
    private void OnCollisionEnter2D(Collider2D other){
        if(other.gameObject == Player){
            Player.transform.parent = transform;
            Debug.Log("1");
        }
    }

    private void OnCollisionExit2D(Collider2D other)
    {
        if(other.gameObject == Player)
        {
            Player.transform.parent = null;
            Debug.Log("2");
        }
    }
*/

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player.transform.parent = transform;
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        Player.transform.parent = null;
    }

}
