//---------------------------------------------------------
// Script zona de escondite.
// Rafa Campos García.
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Rendering.Universal;
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
    /// Audio que suena el esconderse
    /// </summary>
    [SerializeField] private AudioSource hidingSound;

    /// <summary>
    /// Script para acceder a métodos del botón de interactuar.
    /// </summary>
    [SerializeField] private FollowObjectUI FollowObject;

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

    private GameObject _bush;
    private Light2D _light;
    private SpriteRenderer _sprite;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    void Start()
    {
        _bush = transform.Find("Bush").gameObject;
        if (_bush != null)
        {
            _light = _bush.GetComponent<Light2D>();
            _sprite = _bush.GetComponent<SpriteRenderer>();
        }
    }
    /// En el Update() va a controlar si está adentro del collider;
    /// Asímismo, comprueba si ya está escondido o no para salir o entrar en el escondite.
    void Update()
    {
        // Si ya estás escondido, solo importa la tecla para salir
        if (_isHiding)
        {
            if (InputManager.Instance.InteractWasPressedThisFrame())
            {
                ExitHiding();
                _light.enabled = true;
                _sprite.color = Color.white;

            }
        }
        // Si no estás escondido, necesitas estar dentro del collider
        else if (_insideCollider && InputManager.Instance.InteractWasPressedThisFrame())
        {
            EnterHiding();
            _light.enabled = false;
            _sprite.color = new Color32(170, 170, 170, 255);

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
            FollowObject.SetNewTarget(transform);
            FollowObject.ChangeText("hide");

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
            FollowObject.Deactivate();
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
        LevelManager.Instance.Hidden(_isHiding);

        //le decimos al player que está escondido
        _player.SetHidden(true);
        
        

        FollowObject.Deactivate();

        _player.transform.position = transform.position;

        // Desactivamos el movimiento visual del jugador
        _player.GetComponent<Animator>().SetInteger("Direction", 0);
        _player.GetComponent<Animator>().speed = 1.0f;
        _player.GetComponent<AudioSource>().enabled = false;

        hidingSprite.sortingOrder = hidingSortingOrder;

        if (hidingSound != null)
        {
            hidingSound.Play();
        }

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
        LevelManager.Instance.Hidden(_isHiding);

        // el player ya no está escondido
        _player.SetHidden(false);

        // Reactivamos el sonido del jugador
        _player.GetComponent<AudioSource>().enabled = true;

        FollowObject.SetNewTarget(transform);

        hidingSprite.sortingOrder = 0;

        if (hidingSound != null)
        {
            hidingSound.Play();
        }

        //EnemyVision.IsPlayerHidden = false; Provisional hasta qe sepamos como funciona el enemigo
        Debug.Log("Jugador salió del escondite");

    }

    #endregion



}
