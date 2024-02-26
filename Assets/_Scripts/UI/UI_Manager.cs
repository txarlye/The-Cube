using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_Manager : Singleton<UI_Manager>

{
    public RawImage[] life;
    public Slider damageSlider;
    public Text puntuacion;
    public Text level;
    public GameObject gameOver;
    public GameObject playingGameUI;
    public float alturaNewLevel=50;
    
    //una pila para guardar nuestras vidas 
    private Stack<RawImage> lifeStack = new Stack<RawImage>();
    
    //una pila para guardar las vidas que perdemos o que podemos ganar 
    private Stack<RawImage> deathStack = new Stack<RawImage>();
 
    
    private void Awake()
    {
        startGameUI();
        
    }

    public void startGameUI()
    {
        playingGameUI.SetActive(true);
        int lives = GetComponent<GameManager>().GetNumLives();
        int initialLevel = GetComponent<GameManager>().getLevel();
        
        int initialScore = 0;
        puntuacion.text = initialScore.ToString();
        level.text = initialLevel.ToString();
        for (int i = life.Length - 1; i >= 0; i--)
        {
            if (i >= lives)
            {
                deathStack.Push(life[i]);
                life[i].gameObject.SetActive(false);
            }
        }

        for (int j = 0;j < lives; j++)
        {
            lifeStack.Push(life[j]);
            life[j].gameObject.SetActive(true);
        }
        
        // Imprimir el contenido de la pila "lifeStack"
        foreach (RawImage item in lifeStack)
        {
            Debug.Log("lifeStack item: " + item.name);
        }

        // Imprimir el contenido de la pila "deathStack"
        foreach (RawImage item in deathStack)
        {
            Debug.Log("deathStack item: " + item.name);
        }
        
        // Configurar el Slider
        int numerDam = GameManager.instance.GetReceiveDam();
        damageSlider.minValue = 0f;
        damageSlider.maxValue = numerDam;
        damageSlider.value = numerDam; 
    }
    
    /*  // Al inicio hacia reiniciar el juego desde la misma escena
    public void startGameAgain()
    {
        Debug.Log("start again works");
        int auxLive=GameManager.instance.GetNumLives();
        Debug.Log(auxLive);
        // cuando mueres y quieres jugar de nuevo:
        //GameManager.instance.getPlayer().SetActive(true);
        GameManager.instance.resetCounterPointsToLevelUp();
        GameManager.instance.resetCounterPointsToWinLife();
        //playerPrefab.GetComponent<NumLives>().setLive(numLifePlayer);
        gameOver.SetActive(false);
        GameManager.instance.startGame();
        GameManager.instance.getPlayer().GetComponent<NumLives>().setLive(GameManager.instance.getInitialNumLives());
        GameManager.instance.setInitialpoints(0);
        startGameUI();
        //SceneManager.LoadScene("TheGame");   v
    }
    */
    public void restOneHeart()
    {
        // Comprobar si la pila de corazones "vivos" está vacía
        if (lifeStack.Count > 0)
        {
            // Obtener el objeto RawImage de la cima de la pila de corazones "vivos"
            RawImage lifeImage = lifeStack.Peek();

            // Desactivamos el objeto RawImage
            lifeImage.gameObject.SetActive(false);

            // Metemos el corazón en la pila en la pila de corazones muertos
            deathStack.Push(lifeImage);
            // Quitamos el objeto de la pila
            lifeStack.Pop();
            SoundManager.instance.AudioLoseOneLive();
        }
        else
        {
            // Al inicio hacia el GameOver desde la misma escena
            // Tenemos cero vidas !! hemos muerto!
            /*
            gameOver.SetActive(true);
            playingGameUI.SetActive(false); 
            //apagamos el suelo
            GameObject[,] suelo = MapGenerator.instance.getSuelo();
            for (int i = 0; i < suelo.GetLength(0); i++)
            {
                for (int j = 0; j < suelo.GetLength(1); j++)
                {
                    suelo[i,j].SetActive(false);
                }
            } 
             */
            SceneManager.LoadScene("GameOver"); 
        }
    }

    
     public void makeLevelUp()
    {
        Player.instance.cantMove();
        // Raycast hacia abajo para detectar el suelo bajo el jugador
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Si el objeto detectado es suelo, lo movemos hacia arriba junto con el jugador
            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                // Movemos el objeto y el jugador hacia arriba
                hit.collider.gameObject.transform.Translate(0, alturaNewLevel, 0);
                transform.Translate(0, alturaNewLevel, 0);
            }
        }
        MapGenerator.instance.ReiniciarMapa(alturaNewLevel); 
        Player.instance.setcanMove();
        level.text = GameManager.instance.getLevel().ToString();
    }
     
    public void updateDamageSlide()
    { 
      int numerDam = GameManager.instance.GetReceiveDam();
      damageSlider.value = numerDam;
    }

    public void addOneHeart()
    {
        Debug.Log("deathStack count before adding heart: " + deathStack.Count);
        if (deathStack.Count > 0)
        {
            // Obtener el objeto RawImage de la cima de la pila de corazones "muertos"
            RawImage lifeImage = deathStack.Peek();

            // Comprobar si el objeto RawImage está desactivado
            if (!lifeImage.gameObject.activeSelf)
            {
                // Activamos el objeto RawImage
                lifeImage.gameObject.SetActive(true);

                // Metemos el corazón en la pila de corazones "vivos"
                lifeStack.Push(lifeImage);

                // Quitamos de la pila de corazones "muertos"
                deathStack.Pop();   
            }   
        }
        SoundManager.instance.AudioWinOneLive();
    } 
    public void WinPoints(int newPoints)
    {
        int initialPoints=GameManager.instance.getPoints();
        int newValue = initialPoints + newPoints;
        // actualizar puntuación en mi GameManager:
        GameManager.instance.setInitialpoints(newValue);
        // actualizar puntuación en la UI:
        puntuacion.text = newValue.ToString();
        SoundManager.instance.AudioHover();
        //Para comprobar si ganamos vida o no: actualizamos valores : le sumamos la ppuntuacion nueva
        GameManager.instance.addCounterPointsToWinLife(newPoints);
        GameManager.instance.addCounterPointsToLevelUp(newPoints);
    }
}
