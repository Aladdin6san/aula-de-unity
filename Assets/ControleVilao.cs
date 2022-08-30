using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleVilao : MonoBehaviour
{

    Rigidbody2D rigidbody;
    Animator animator;

    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    float alturaRaio, distanciaRaio;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
  private void FixedUpdate()
    {
        Vector3 ajustaAltura = new Vector3(distanciaRaio/2,alturaRaio,0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + ajustaAltura, Vector2.left, distanciaRaio, layerMask);
        if (hit.collider != null)
        {
            if (transform.position.x > hit.point.x)

                transform.localScale = new Vector3(-1, 1, 1);

            else

                transform.localScale = new Vector3(1, 1, 1);

            rigidbody.velocity = new Vector3(transform.localScale.x, 0);
            animator.SetBool("WALKING", true);
        }
        else
        {
            animator.SetBool("WALKING", false);
        }
            
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 posInicial = transform.position + new Vector3(distanciaRaio / 2, alturaRaio, 0);
        Vector3 posFinal = transform.position + new Vector3(-distanciaRaio / 2,alturaRaio,0);
        Gizmos.DrawLine(posInicial, posFinal);
    }
}
