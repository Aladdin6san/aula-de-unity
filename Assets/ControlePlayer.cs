using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ControlePlayer : MonoBehaviour
{
    [Range(0f,15f)]
    [SerializeField]
    float forcaPulo=1f;
    LayerMask layerMask;
    float sentido = 0;// o nome desse sujeito é campo, que é uma variável declarada no escopo da classe, logo pode ser utilizado em qualquer um dos métodos função>start,update)
    // Start is called before the first frame update
    bool noChao=false;
    void Start()
    {
        for (int i = 0; i < 10; i++) ;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f * sentido, 0);
        Vector2 velAtual= GetComponent<Rigidbody2D>().velocity; 
        velAtual.x = 5f * sentido;
        GetComponent<Rigidbody2D>().velocity = velAtual;
        GetComponent<Animator>().SetBool("GROUNDED", noChao);

       
    }
    private void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.5f, layerMask);
        Debug.Log(hit);
        if (hit != null)
        {
            noChao = true;
        }
    }
    public void Sentido(CallbackContext context)
    {
        //Debug.Log(context.ReadValue<float>());
        // transform.Translate(0.5f * context.ReadValue<float>(),0,0); 0,0 = y e z
        sentido = context.ReadValue<float>();
        if (sentido != 0)
        {
            gameObject.transform.localScale = new Vector3(sentido, 1, 1);
            GetComponent<Animator>().SetBool("WALKING", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("WALKING", false);
        }
    }
    public void Pulo(CallbackContext context)
    {
        if(context.ReadValue<float>()==1)
        {
            GetComponent<Rigidbody2D>(). AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse); // no vector = (x,y)
            GetComponent<Animator>().SetTrigger("JUMP");
            GetComponent<Animator>().SetBool("GROUNDED", false);
            noChao = false;
        }
    }
    

}
