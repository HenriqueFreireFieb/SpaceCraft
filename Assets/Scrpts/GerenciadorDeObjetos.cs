using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeObjetos : MonoBehaviour
{
    [SerializeField]
    private GameObject _inimigoPrefab;

    [SerializeField]
    private GameObject [] _powerUps;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotinaGeracaoInimigo()); // Iniciando a geração dos Inimigos
        StartCoroutine(RotinaGeraçãoPu()); // Iniciando a geração dos Power Ups
    }

    // Rotina de geração aleatória de inimigos
    IEnumerator RotinaGeracaoInimigo () 
    {
        while(true)
        {
            Instantiate(_inimigoPrefab, new Vector3(Random.Range(-7.7f, 7.7f), 6.0f, 0f), Quaternion.identity); // Gerar um inimigo em uma posição de x aleatorio e y 6

            yield return new WaitForSeconds(Random.Range(2f, 7f));
        }
    }

    // Rotina de geração aleatória de Power Ups
    IEnumerator RotinaGeraçãoPu() 
    {
        while(true)
        {
            int powerUpAleatório = Random.Range(0, 3); // Gerando um número aleatório entre 0 e 2
            Instantiate(_powerUps[powerUpAleatório], new Vector3(Random.Range(-7.7f, 7.7f), 6.0f, 0f), Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(10f, 16f));
        }
    }

}
