using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float velocidade = 1;
    [HideInInspector]public float reposicao = 18;
    [HideInInspector]public Vector3 posicaoInicial;
    // Start is called before the first frame update
    void Start()
    {
        posicaoInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float novoPosicao = Mathf.Repeat(Time.time*velocidade,reposicao);
        transform.position = posicaoInicial + Vector3.down* novoPosicao;
    }
}
