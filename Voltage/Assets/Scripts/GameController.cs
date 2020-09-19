using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private Camera camera;

    public float speedCam;
    public Transform playerTransform;
    public Transform limitCamTop, limitCamDown, limitCamLeft, limitCamRight;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //camera.transform.position = new Vector3(playerTransform.transform.position.x,playerTransform.transform.position.y,camera.transform.position.z);
    }

    //Ele é executado tudo do mesmo frame, porem depois do metodo (Update) executar ele executa
    void LateUpdate(){

        this.CameraControl();

    }

    private void CameraControl(){

        float posCamX = playerTransform.position.x;
        float posCamY = playerTransform.position.y;

        if(camera.transform.position.x < limitCamLeft.position.x && 
           playerTransform.position.x < limitCamLeft.position.x){
               posCamX = limitCamLeft.position.x;
        }else if(camera.transform.position.x > limitCamRight.position.x && 
           playerTransform.position.x > limitCamRight.position.x){
               posCamX = limitCamRight.position.x;
        }

        if(camera.transform.position.y < limitCamDown.position.y && 
           playerTransform.position.y < limitCamDown.position.y){
           posCamY = limitCamDown.position.y; 
        }else if(camera.transform.position.y > limitCamTop.position.y && 
           playerTransform.position.y > limitCamTop.position.y){
           posCamY = limitCamTop.position.y; 
        }

        Vector3 posCam = new Vector3(posCamX, posCamY, camera.transform.position.z);

        //Lep = aonde eu to, pra onde eu vou
        camera.transform.position = Vector3.Lerp(camera.transform.position, posCam, speedCam * Time.deltaTime);
    }
}
