//---------------------------------------------------------
// Script zona de escondite
// Rafael Campos García
// 
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class HidingSpot : MonoBehaviour
{

    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    //Sprite

    //Sprite del objeto que nos esconde
    [SerializeField] private SpriteRenderer hidingSprite;
    //El valor para echar al frente el sprite que nos esconde, ocultándo al jugador.
    [SerializeField] private int hidingSortingOrder = 10;
    //Stuff del jugador y el controller
    [SerializeField] private InputManager input;
    [SerializeField] private GameObject player;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    //Sprite del jugador que va a esconder
    private playerMovement _player;
    //Una vez entre en colisión con el collider del hidingspot
    private bool _insideCollider;
    //Bool que comprueba si ya está escondido o no.
    private bool IsHiding;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour



    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>

    /// En el Update() va a controlar si está adentro del collider;
    /// Asímismo, comprueba si ya está escondido o no para salir o entrar en el escondite.
    void Update()
    {
       
        if (_insideCollider && input.InteractWasPressedThisFrame())
        {
            if (IsHiding)
            {
                ExitHiding();
            }
            else
            {
                EnterHiding();
            }
        }
        
    }


    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
   
    #endregion
    
    

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    
    //Una vez el jugador se acerque al collider, se establece como que lo está con _insideCollider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.gameObject.GetComponent<playerMovement>();
        if (_player != null)
        {
            _insideCollider = true;
            Debug.Log("tusmuertos");
 
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerMovement player = collision.GetComponent<playerMovement>();
        if (player != null)
        {
            _player = null;
        }
    }


    //Métodos de entrada y salida del escondite.
    private void EnterHiding()
    {
        //_playerSprite = _player.GetComponent<SpriteRenderer>();
        IsHiding = true;
        player.SetActive(false);
        //_player.SetHidden(true);
        hidingSprite.sortingOrder = hidingSortingOrder;

        //EnemyVision.IsPlayerHidden = true; Provisional hasta qe sepamos como funciona el enemigo
        //Igualmente, desactivando al player no debería detectarlo.
        Debug.Log("Jugador escondido");
    }
    private void ExitHiding()
    {
        IsHiding = false; 
        hidingSprite.sortingOrder = 0;
        player.SetActive(true);

        //_player.SetHidden(false);
        //EnemyVision.IsPlayerHidden = false; Provisional hasta qe sepamos como funciona el enemigo
        Debug.Log("Jugador salió del escondite");

    }

    #endregion



}
