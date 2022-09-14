using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUp : MonoBehaviour
{
    [SerializeField]
    private float _velocidade = 3.5f; // Velocidade do power up

    /*Criando a variavel responsavel por checar qual power up foi coletado
    0 = Disparo triplo, 1 = Velocidade, 2 = Escudo*/
    [SerializeField]
    private int PUpID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _velocidade * Time.deltaTime);


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>(); // Importando o script Player

        if (other.tag == "Player")// Verificando se o objeto est� em colis�o com o player ou outro objeto
        {
            if(player != null){
            
            // Power up Disparo triplo
            if(PUpID == 0){ 
                
                player.LigarPuDisparoTriplo();

                Destroy(this.gameObject); // Destruindo o power up
                
            }else if(PUpID == 1){
                
                // Power up Velocidade
                player.LigarAumentoVelocidade();

                Destroy(this.gameObject);
            
            }else if(PUpID == 3){
                
                // Power up Escudo


            }
            }else{
                Debug.Log("O objeto "+this.name+" esta em colisão com "+other.name);
            }
        }
    }

}
