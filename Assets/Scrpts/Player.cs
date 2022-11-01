using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variavel responsavel pela vida do player
    public int vidas = 3;

    // Variavel responsavel por permitir ou não dar o tiro triplo
    public bool possoDarDisparoTriplo = false;

    // Variavel responsavel por permitir ou não realizar o aumento de velocidade
    public bool possoAumentarVelocidade = false;

    // Variavel responsavel por permitir ou não ultilizar o campo de força
    public bool possoUsarOCampoDeForca = false;

    // Tiro triplo
    [SerializeField]
    private GameObject _disparoTriploPrefab;

    // Variavel responsavel pelo colldown entre os tiros
    [SerializeField]
    private float _tempoDeDisparo = 0.30f;

    // Animação do campo de força (escudo)
    [SerializeField]
    private GameObject _CampoDeForca;

    // Articulador da comunicação entre o scprit Player e o script GerenciadorIU
    private GerenciadorIU _IuGerenciador;
    
    // Variavel responsavel por checar se o colldown dos tiros ja acabou
    private float _podeDisparar = 0.0f;    
    
    // Velocidade
    [SerializeField]
    private float _veloc = 3;

    // laser (tiro padrão [prefab])
    [SerializeField]
    private GameObject _pflaser;
    
    // Entrada Horizontall
    [HideInInspector]public float entradaHorizontal;

    // Animação de explosão do player (Animação de morte)
    [SerializeField]
    private GameObject _explosaoPlayerPrefab;
    
    // Acessando o script Gerenciador do jogo
    private GerenciadorDoJogo _gerenciadorDoJogo;
    
    // Acessando o script GerenciadorDeObjetos
    private GerenciadorDeObjetos _gerenciadorDeObjetos;

    // Criando articulador para acessar o componente responsavel por reproduzior o som de disparo
    private AudioSource _audioLaser;

    // Variavel responsavel por gerenciar os danos nos motores
    [SerializeField]
    private GameObject[] _motores;

    // Variavel responsavel por contar quantos danos a nave do jogador recebeu
    private int _contadorDeDanos;
    
    // Metodo start
    void Start()
    {
        // Log de erros
        Debug.Log("Metodo Start"+ this.name);
        
        // posição inicial do jogador
        transform.position = new Vector3(0,0,0);

        // Acessando o componente de script GerenciadorIU
        _IuGerenciador = GameObject.Find("Canvas").GetComponent<GerenciadorIU>();
        
        // Verificando se a variavel _IuGerenciador não possui um valor nulo
        if (_IuGerenciador != null)
        {
            //Se não possuir um valor nulo atualize as vidas na tela do player
            _IuGerenciador.AtualizarVidas(vidas);
        }

        // Ocultando campo de força
        _CampoDeForca.SetActive(false);

        // Acessando o componente de script GerenciadorDoJogo através do objeto GerenciadorDoJogo
        _gerenciadorDoJogo = GameObject.Find("GerenciadorDoJogo").GetComponent<GerenciadorDoJogo>();

        // Acessando o componente de script GerenciadorDeObjetos através do objeto GerenciadorDeObjetos
        _gerenciadorDeObjetos = GameObject.Find("GerenciadorDeObjetos").GetComponent<GerenciadorDeObjetos>();

        // Ativando a geração de inimigos e power ups
        if (_gerenciadorDeObjetos != null)
        {
        _gerenciadorDeObjetos.IniciarCoroutines();
        }

        // Acessando o componente Audio Source do prefab Player.
        _audioLaser = GetComponent<AudioSource>();

        // Zerando a variavel contadorDeDanos
        _contadorDeDanos = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        //chamando o movimento
        movimento();

        //atirando (caso espaço ou potão esquerdo do mouse seja pressionado)
        if(Input.GetKeyDown(KeyCode.Space)|Input.GetMouseButtonDown(0)){
            Disparo();
        }


        // Ativando campo de força
        if (possoUsarOCampoDeForca == true){
            LigarCampoDeForca();
        }
    }

    private void movimento(){
        // Movimentando na horizontal
        float entradaHorizontal = Input.GetAxis("Horizontal"); // criando variavel de movimento horizontal
            
            if(possoAumentarVelocidade == true){
                transform.Translate(Vector3.right*entradaHorizontal*Time.deltaTime*_veloc*2.3f);
            }else{
             transform.Translate(Vector3.right*entradaHorizontal*Time.deltaTime*_veloc);
            }


        //Movimentando na vertical
        float entradaVertical = Input.GetAxis("Vertical"); // criando variavel de movimentação vertical
        
        if(possoAumentarVelocidade == true){
            transform.Translate(Vector3.up*entradaVertical*Time.deltaTime*_veloc*2.3f);
        }else{
        transform.Translate(Vector3.up*entradaVertical*Time.deltaTime*_veloc);
        }

        // Limitando a movimentaçãao na vertical
        if(transform.position.y > 0){
            transform.position = new Vector3(transform.position.x,0,0); // limitando a movimentação para cima
        }else if(transform.position.y < -3.75){
            transform.position = new Vector3(transform.position.x,-3.75f,0);// Limitando a movimentação para baixo
        }

        // Limitando a movimentação na horizontal
        if(transform.position.x < -9.68){
            transform.position = new Vector3(9.68f,transform.position.y,0);// limitando a movimentação para a esquerda
        }else if(transform.position.x > 9.68){
            transform.position = new Vector3(-9.68f,transform.position.y,0);// limitando a movimentação para a direita
        }

    }

    public void DanoAoPlayer()
    {
        
        if (possoUsarOCampoDeForca == true){
        
            possoUsarOCampoDeForca = false; // Desativando o campo de força
            _CampoDeForca.SetActive(false); // Desativando visualmente o campo de força
            return; // Retornando ao inicio do metodo DanoAoPlayer()

        }
        
        _contadorDeDanos ++; // Aumentando o Contador de danos

        if (_contadorDeDanos == 1)
        {
            // Exibir danoMotorEsq
            _motores[0].SetActive(true);
        }else if (_contadorDeDanos == 2)
        {
            // Exibir danoMotorEsq
            _motores[1].SetActive(true);
        }
        
        vidas--; // Diminuindo 1 de vida
        _IuGerenciador.AtualizarVidas(vidas); // Atualizando a barra de vida do Hud

        if (vidas < 1){

            Instantiate(_explosaoPlayerPrefab, transform.position, Quaternion.identity); // Criando explosão de morte
            
            _gerenciadorDoJogo.fimDeJogo = true; // Declarando GameOver
            
            _IuGerenciador.MostrarTelaInicial();

            Destroy(this.gameObject);
        }
    }
    public void LigarPuDisparoTriplo(){
        // Ativando o disparo triplo
        possoDarDisparoTriplo = true;

        // Ativando o temporizador do disparo triplo
        StartCoroutine(DisparoTriploRotina());
    }

    public IEnumerator DisparoTriploRotina(){
    
    yield return new WaitForSeconds(7.0f); // Aguarde 7 segundos em seguida execute
    possoDarDisparoTriplo = false; // desativando disparo triplo
}

    private void Disparo(){
        // Verificando se o tempo de colldown ja passou
            if(Time.time > _podeDisparar){
            
            _audioLaser.Play(); // Tocando o audio de disparo

            //criando instancia do prefab laser/laser triplo
            if(possoDarDisparoTriplo == true){// Perguntando se posso usar disparo triplo
                Instantiate(_disparoTriploPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
                
            }else{ // Caso o disparo triplo esteja inativo atirando com o disparo simples
                Instantiate(_pflaser, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
            }
                _podeDisparar = Time.time + _tempoDeDisparo; // Reatribuindo o valor da variavel para reiniciar o colldown
            
            }
    }

    public void LigarAumentoVelocidade(){
    possoAumentarVelocidade = true;

    StartCoroutine(AumentoVelocidadeRotina());
}

    public IEnumerator AumentoVelocidadeRotina(){
        yield return new WaitForSeconds(7.0f);// Aguarde 7 segundos em seguida execute
        possoAumentarVelocidade = false; // desativando disparo triplo
}

    public void LigarCampoDeForca(){
        // Habilitando o campo de força
        possoUsarOCampoDeForca = true;

        // Ativando visualmente o campo de força
        _CampoDeForca.SetActive(true);
    }

    /*public IEnumerator CampoDeForcaRotina(){
        yield return new WaitForSeconds(7.0f);// Agurde 7 segundos em seguida execute
        possoUsarOCampoDeForca = false; // desativando campo de força
    }*/

}
