using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRb,esferaRb;
    private Animator playerAnimator;

    public float speed;
    public float jumpForce;
    //esta olhando para esquerda
    public bool isLookLeft;

    public Transform groundCheck;
    private bool isGrounded;//é chao
    private bool talk;
    private int colects;
    private bool death;

    public GameObject lastCheckPoint, gameOverText, restarButton, restartCheck, esfera, startText;

    //esfera
    [SerializeField]
    private Image setaImg;
    [SerializeField]
    private Image setaImg2;
    [SerializeField]
    private Transform posStart;

    public float zRotation;
    public bool liberaRot;
    public bool liberaChuteArremeso ;
    private float force = 0f;
    public Image flexaTwo;

    // Start is called before the first frame update
    void Start()
    {
        talk = false;
        colects = 0;
        //captura os componetes do objeto da cena
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        esferaRb = esfera.GetComponent<Rigidbody2D>();

        gameOverText.SetActive(false);
        setaImg.enabled = false;
        setaImg2.enabled = false;
        restarButton.SetActive(false);
        esfera.SetActive(false);
        //restartCheck.SetActive(false);
        //setaImg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   

        RotacaoSeta();
        ControlForce();
        AplicaForce();
        //GetAxisRaw = Não tem sensibilidade no analogico, se o jogador pressionar mais forte ou mais fraco mais andar a mesma qtd
        float h = Input.GetAxisRaw("Horizontal");

        //se ele atacar no chao ele para na hora, no ar nao

        if(h > 0 && isLookLeft)
        {
            Flip();
        }else if (h < 0 && !isLookLeft)
        {
            Flip();
        }

        /*
        if(h != 0){
            GetComponent<Animator>().SetBool("walking",true);
        }
        else{
            GetComponent<Animator>().SetBool("walking",false);
        }
        */

        if(playerAnimator.GetInteger("walking") != 0){
            startText.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.R)){//reseta jogo
            transform.position = lastCheckPoint.transform.position;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Invoke("Restart",2f);
        }

        if(Input.GetKeyDown(KeyCode.F)){//volta esfera
            esfera.SetActive(false);
            playerAnimator.SetFloat("semEsfera", 1f);
        }

        float speedY = playerRb.velocity.y;

        if (Input.GetButtonDown("Jump") && talk)
        {
            Debug.Log("dialogo");
        }
        else if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.AddForce(new Vector2(0, jumpForce));
        }

        //nao sei o que faz
        playerRb.velocity = new Vector2(h * speed, speedY);

        if(death){
            playerRb.velocity = new Vector2(0, 0);
        }
        

        //Seta os valores que estao no animator do personagem
        //playerAnimator.SetInteger("horizontal", (int)h);
        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("speedY", speedY);
        playerAnimator.SetInteger("walking", (int)h);
        
    }

    //Atualizacao fixa em 0.02 segundos
    private void FixedUpdate()
    {
        //cria um colisor de cicurlo, em baixo do colisor do protagonista, quando ele estiver no cao
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
        InputRotacao();
        LimitaRotacao();
    }

    void lose(){
        //Destroy(this.playerRb.gameObject);
        gameOverText.SetActive(true);
        restarButton.SetActive(true);
        gameObject.SetActive(false);
    }

    //Vira o personagem para esquerda ou direita
    void Flip()
    {
        if(!playerAnimator.GetBool("preparo")){
            isLookLeft = !isLookLeft;
            float x = transform.localScale.x * -1;//inverte o sinal do X do transforme
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision2D){
        if(collision2D.gameObject.CompareTag("moeda")){
            Destroy(collision2D.gameObject);
            colects++;
        }

        if(collision2D.gameObject.CompareTag("vazio")){
            this.death = true;
            playerAnimator.SetFloat("speedY", 0f);
            playerAnimator.SetBool("isGrounded", true);
            playerAnimator.SetTrigger("lose");
            playerRb.velocity = new Vector2(0, 0);
            

            //playerRb.angularVelocity = 0f;
            //transform.position = lastCheckPoint.transform.position;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(collision2D.gameObject.CompareTag("inimigo")){
            transform.position = lastCheckPoint.transform.position;
        }

        if(collision2D.gameObject.CompareTag("npc")){
            talk = true;
        }

        if(collision2D.gameObject.CompareTag("checkPoint")){
            lastCheckPoint = collision2D.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision2D){
        if(collision2D.gameObject.CompareTag("npc")){
            talk = false;
        }
    }

    private void OnMouseDown()
    {
        //setaImg.SetActive(true);
        //posicionar a seta onde o personagem esta
        //setaImg.rectTransform.position = transform.position;
        playerAnimator.SetBool("preparo", true);
        this.speed = 0;
        if(playerAnimator.GetFloat("semEsfera") == 1f){
            zRotation = 0;
            flexaTwo.fillAmount = 0.3f;
            setaImg.enabled = true;
            setaImg2.enabled = true;
            //setaImg.rectTransform.position = posStart.position;
            this.liberaRot = !this.liberaRot;
        }
    }

    private void OnMouseUp()
    {
        playerAnimator.SetBool("preparo", false);
        this.speed = 6;
        if(playerAnimator.GetFloat("semEsfera") == 1f){
            playerAnimator.SetBool("chute", true);
        }
        
    }

    void chutaEsfera(){
        playerAnimator.SetBool("chute", false);
        playerAnimator.SetFloat("semEsfera", -1f);
        Vector3 vetor = new Vector3(0, 0, 0);
        vetor = this.transform.position;
        //faz a bolinha ser lancada um pouco mas a frente para evitar colisao com o personagem
        vetor[0] = vetor[0]+1.5f;
        esfera.transform.position = vetor;
        esfera.SetActive(true);
        //setaImg.SetActive(false);
        this.liberaRot = !this.liberaRot;
        this.liberaChuteArremeso = !this.liberaChuteArremeso;
        
        
        setaImg.enabled = false;
        setaImg2.enabled = false;
    }

    private void RotacaoSeta()
    {
        setaImg.rectTransform.eulerAngles = new Vector3(0, 0, this.zRotation);
    }

    private void InputRotacao()
    {
        if (liberaRot) {

            if (Input.GetKey(KeyCode.W))
            {
                zRotation += 2.5f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                zRotation -= 2.5f;
            }

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

    private Tuple<float, float> CalculeTrigonometria()
    {
        float x,y;

        //Deg2Rad = constante de conversao de graus para radianos
        x = force * (Mathf.Cos(this.zRotation * Mathf.Deg2Rad));
        y = force * (Mathf.Sin(this.zRotation * Mathf.Deg2Rad));

        return new Tuple<float, float>(x, y);
    }

    private void AplicaForce()
    {
        Tuple<float, float> tuple;
        tuple = this.CalculeTrigonometria();

        if (this.liberaChuteArremeso){
            esferaRb.AddForce(new Vector2(tuple.Item1, tuple.Item2));
            this.liberaChuteArremeso = !this.liberaChuteArremeso;
        }
    }

    private void ControlForce()
    {
        if (this.liberaRot)
        {
            if (Input.GetKey(KeyCode.A))
            {
                flexaTwo.fillAmount -= 1 * Time.deltaTime;
                force = flexaTwo.fillAmount * 1000;
            }
            if (Input.GetKey(KeyCode.D))
            {
                flexaTwo.fillAmount += 1 * Time.deltaTime;
                force = flexaTwo.fillAmount * 1000;
            }

            if(isLookLeft){

            }

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
