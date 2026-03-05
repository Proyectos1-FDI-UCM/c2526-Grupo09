//---------------------------------------------------------
// Script zona de escondite.
// Rafa Campos García.
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEditor.Experimental.GraphView;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Script para aquellos sprites que funcionen como escondite para el jugador.
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

    /// <summary>
    /// Sprite del objeto que nos esconde
    /// </summary>
    [SerializeField] private SpriteRenderer hidingSprite;

    /// <summary>
    /// El valor para echar al frente el sprite que nos esconde, ocultándo al jugador.
    /// </summary>
    [SerializeField] private int hidingSortingOrder = 10;

    /// <summary>
    /// Stuff del jugador y el controller
    /// </summary>
    [SerializeField] private InputManager input;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    /// <summary>
    /// Sprite del jugador que va a esconder
    /// </summary>
    private PlayerMovement _player;

    /// <summary>
    /// Una vez entre en colisión con el collider del hidingspot
    /// </summary>
    private bool _insideCollider;

    /// <summary>
    /// Bool que comprueba si ya está escondido o no.
    /// </summary>
    private bool _isHiding;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour



    
    /// En el Update() va a controlar si está adentro del collider;
    /// Asímismo, comprueba si ya está escondido o no para salir o entrar en el escondite.
    void Update()
    {
        // Si ya estás escondido, solo importa la tecla para salir
        if (_isHiding)
        {
            if (input.InteractWasPressedThisFrame())
            {
                ExitHiding();
            }
        }
        // Si no estás escondido, necesitas estar dentro del collider
        else if (_insideCollider && input.InteractWasPressedThisFrame())
        {
            EnterHiding();
        }
    }


    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    #endregion



    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados


    /// <summary>
    /// Una vez el jugador se acerque al collider, se establece como que lo está con _insideCollider.
    /// </summary>
    /// <param name="collision"></param>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Solo asignamos si el objeto tiene el script PlayerMovement
        PlayerMovement foundPlayer = collision.gameObject.GetComponent<PlayerMovement>();

        if (foundPlayer != null)
        {
            _player = foundPlayer;
            _insideCollider = true;
            Debug.Log("Jugador cerca del escondite");
        }
    }

    /// <summary>
    /// Una vez el jugador sale del collider, se establece como !_insideCollider
    /// </summary>
    /// <param name="collision"></param>

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement foundPlayer = collision.gameObject.GetComponent<PlayerMovement>();

        if (foundPlayer != null)
        {
            _player = foundPlayer;
            _insideCollider = false;
        }
        
    }


    //Métodos de entrada y salida del escondite.

    /// <summary>
    /// Método para la entrada de escondite del jugador.
    /// </summary>
    private void EnterHiding()
    {
        //_playerSprite = _player.GetComponent<SpriteRenderer>();
        _isHiding = true;

        //le decimos al player que está escondido
        _player.SetHidden(true);

        _player.transform.position = transform.position;

        hidingSprite.sortingOrder = hidingSortingOrder;

        //EnemyVision.IsPlayerHidden = true; Provisional hasta qe sepamos como funciona el enemigo
        //Igualmente, desactivando al player no debería detectarlo.
        Debug.Log("Jugador escondido");
    }

    /// <summary>
    /// Método de salida de escondite del jugador.
    /// </summary>
    private void ExitHiding()
    {
        if (_player == null) {
            Debug.Log("el jugador ya no existe");
            return; }
        _isHiding = false;

        // el player ya no está escondido
        _player.SetHidden(false);

        hidingSprite.sortingOrder = 0;

        //EnemyVision.IsPlayerHidden = false; Provisional hasta qe sepamos como funciona el enemigo
        Debug.Log("Jugador salió del escondite");

    }

    #endregion



}
