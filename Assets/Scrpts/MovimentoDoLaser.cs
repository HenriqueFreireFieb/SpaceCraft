using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoDoLaser : MonoBehaviour
{
    // Variavel responsavel pela velocidade do laser
    [SerializeField]
    private float _velocLaser = 12.0f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movimentando o laser
        transform.Translate(Vector3.up* _velocLaser * Time.deltaTime);

        // Se a posição y do laser for maior que 5.5 destrua-o
        if(transform.position.y > 5.5){
            Destroy(this.gameObject);
        }

    }
}
