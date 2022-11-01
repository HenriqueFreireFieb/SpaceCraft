using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorIU : MonoBehaviour
{
    // Array responsavel por gerencias as imagems de vida
    public Sprite[] vidas;

    public Image mostrarImagemDasVidas;

    // Variavel responsavel por contabilizar a pontuação do jogador
    public int placar;

    // Variavel responsavel por mostrar no HUD as pontuações do jogador
    public Text textoPlacar;

    public GameObject tituloDaTela;

    public GameObject meuNome;

    public void AtualizarVidas(int vidasAtuais)
    {
        mostrarImagemDasVidas.sprite = vidas[vidasAtuais];
    }

    public void AtualizarPlacar()
    {
        // Adicionando 100 pontos ao placar sempre que um inimigo for morto
        placar += 00100;

        // Atualizando vizualmente o placar
        textoPlacar.text = "PLACAR: " + placar;
    }

    public void MostrarTelaInicial()
    {
        // Exibindo a tela do inicial do jogo
        tituloDaTela.SetActive(true);
        meuNome.SetActive(false);
    }

    public void EsconderTelaInicial()
    {
        // Escondendo a tela do inicial do jogo
        tituloDaTela.SetActive(false);
        meuNome.SetActive(true);
        textoPlacar.text = "PLACAR: 00000"; // zerando o texto do placar
        placar = 0;
    }

}
