using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ControlePlayer : MonoBehaviour
{
    const float RAIO_JUMPABLE = 0.05f;
    [Range(0f,15f)]
    [SerializeField]
    float forcaPulo=1f;
    [SerializeField]
    LayerMask layerMask;
    float sentido = 0;// o nome desse sujeito é campo, que é uma variável declarada no escopo da classe, logo pode ser utilizado em qualquer um dos métodos função>start,update)
    // Start is called before the first frame update
    bool noChao=false;
    int contadorPulo = 0;
    const int TotalPulos = 1;

    // armarzenar os componentes
    Animator animator;
    Rigidbody2D rigidbody;

    Vector3 checkpoint;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        checkpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f * sentido, 0);
        Vector2 velAtual= GetComponent<Rigidbody2D>().velocity; 
        velAtual.x = 5f * sentido;
        rigidbody.velocity = velAtual;

       
    }
    private void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.5f, layerMask);
        Debug.Log(hit);
        if (hit != null)
        {
            if (hit.CompareTag("chao"))
            {
                noChao = true;
                contadorPulo = 0;
            }
            else if (hit.CompareTag("mal"))
            {
                Destroy(hit.gameObject);
            }
        }
        else
        {
            noChao = false;

        }
        animator.SetBool("GROUNDED", noChao);
    }
    public void Sentido(CallbackContext context)
    {
        //Debug.Log(context.ReadValue<float>());
        // transform.Translate(0.5f * context.ReadValue<float>(),0,0); 0,0 = y e z
        sentido = context.ReadValue<float>();
        if (sentido != 0)
        {
            gameObject.transform.localScale = new Vector3(sentido, 1, 1);
            animator.SetBool("WALKING", true);
        }
        else
        {
            animator.SetBool("WALKING", false);
        }
    }
    public void Pulo(CallbackContext context)
    {
        if (context.ReadValue<float>() == 1 && (noChao || contadorPulo < TotalPulos))
        {
            rigidbody. AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse); // no vector = (x,y)
            animator.SetTrigger("JUMP");
            animator.SetBool("GROUNDED", false);
            noChao = false;
            contadorPulo++;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, RAIO_JUMPABLE);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("mal"))
        {
            transform.position = checkpoint; //volta ao último chekpoint
        }
    }

}
