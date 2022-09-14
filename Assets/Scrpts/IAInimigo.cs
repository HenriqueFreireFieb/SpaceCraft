using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInimigo : MonoBehaviour
{
    // Criando uma variavel privada para controlar a velocidade da nave inimiga
    private float _velocidade = 6.0f;
    
    [SerializeField]
    private GameObject _explosaoDoInimgoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movimentando o inimigo para baixo
        transform.Translate(Vector3.down * _velocidade * Time.deltaTime);

        if (transform.position.y < -6.0f){ // Se a posição de y for menor que -6 mude a nave para y 6.0 e x aleatorio ente -7.7 e 7.7
            
            float randomX = Random.Range(7.7f, -7.7f);
            
            transform.position = new Vector3(randomX, 6.0f, 0);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Tiro"){

            Destroy(other.gameObject); // Destrua o laser

            // Ativando a animação de explosão do player
            Instantiate(_explosaoDoInimgoPrefab, transform.position, Quaternion.identity);
        
            Destroy(this.gameObject); // Destrua este objeto

        }else if(other.tag == "Player")
        {
            // Acessando o objeto Player
            Player player = other.GetComponent<Player>();

            if (player != null) // Se o objeto player for diferente de nulo então ...
            {
                player.DanoAoPlayer(); // Cause 1 de dano ao player

                // Ativando a animação de explosão do player
                Instantiate(_explosaoDoInimgoPrefab, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject); // Destrua este objeto
        }

    }

}