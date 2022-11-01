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

    [SerializeField]
    private AudioClip _audioPU;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _velocidade * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>(); // Importando o script Player

        if (other.tag == "Player")// Verificando se o objeto está em colisão com o player ou outro objeto
        {
            if(player != null){
            AudioSource.PlayClipAtPoint(_audioPU, Camera.main.transform.position, 1.0f);
            
            // Power up Disparo triplo
            if(PUpID == 0){ 
                
                player.LigarPuDisparoTriplo();

                Destroy(this.gameObject); // Destruindo o power up
                
            }else if(PUpID == 1){
                
                // Power up Velocidade
                player.LigarAumentoVelocidade();

                Destroy(this.gameObject);
            
            }else if(PUpID == 2){
                
                // Power up Escudo
                player.LigarCampoDeForca();

                Destroy(this.gameObject);
            }
            }else{
                Debug.Log("O objeto "+this.name+" esta em colisão com "+other.name);
            }
        }
    }

}
