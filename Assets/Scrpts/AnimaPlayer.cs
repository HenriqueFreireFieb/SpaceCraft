using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaPlayer : MonoBehaviour
{
    private Animator _animacao;
    // Start is called before the first frame update
    void Start()
    {
        // Acessando todas as propriedades do componente animator do objeto player
        _animacao = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Ativando a animação virar a esquerda
            _animacao.SetBool("Mover Esquerda", true);

            // Desativando a animação virar a direita 
            _animacao.SetBool("Mover Direita", false);

            
        }else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            // Desativando a animação virar a esquerda 
            _animacao.SetBool("Mover Esquerda", false);

            // Desativando a animação virar a direita 
            _animacao.SetBool("Mover Direita", false);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Ativando a animação virar a direita
            _animacao.SetBool("Mover Direita", true);
            
            // Desativando a animação virar a esquerda 
            _animacao.SetBool("Mover Esquerda", false);
        }else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            // Desativando a animação virar a direita 
            _animacao.SetBool("Mover Direita", false);
           
            // Desativando a animação virar a esquerda 
            _animacao.SetBool("Mover Esquerda", false);
        }
    }
}
