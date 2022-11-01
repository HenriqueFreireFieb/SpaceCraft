using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDoJogo : MonoBehaviour
{
    // Variavel responsavel por saber se o jogo foi ou não finalizado.
    public bool fimDeJogo = true;

    // Variavel responsavel por instanciar o Player.
    public GameObject player;

    // Variavel responsavel por acessar ao script GerenciadorIU
    private GerenciadorIU _gerenciadorIU;

    void Start()
    {
        // Acessando o componente de script GerenciadorIU no objeto Canvas
        _gerenciadorIU = GameObject.Find("Canvas").GetComponent<GerenciadorIU>();
    }

    void Update()
    {
        if (fimDeJogo == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // instanciando a nave do Player
                Instantiate(player, Vector3.zero, Quaternion.identity);

                fimDeJogo = false;

                _gerenciadorIU.EsconderTelaInicial();
            }
        }
    }
}
