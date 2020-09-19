using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Transform background;
    public float speed;

    private Transform camera;
    private Vector3 previewCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        previewCameraPosition = camera.position;
    }

    // Update is called once per frame
    void Update()
    {
        //da onde tava e onde esta agora
        float parallaxX = previewCameraPosition.x - camera.position.x;
        float bgTargetX = background.position.x + parallaxX;//move o chao

        Vector3 bgPosition = new Vector3(bgTargetX, background.position.y, background.position.z);
        background.position = Vector3.Lerp(background.position, bgPosition, speed * Time.deltaTime); //Lep = aonde eu to, pra onde eu vou

        //nova posição da camera
        previewCameraPosition = camera.position;
    }
}
