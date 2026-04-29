//---------------------------------------------------------
// Script que contiene todo aquello relacionado con lanzar objetos por el mundo: tiene en cuenta la lógica entre los distintos
// objetos del mundo, los controles, junto a el lanzamiento del objeto y el cursor. 
// Diego Martín
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Timeline;
// Añadir aquí el resto de directivas using


/// <summary>
/// Clase que gestiona el sistema de lanzamiento de objetos arrojadizos. En esta se encuentra toda la lógica entre los objetos
/// del mundo, es decir, tiene una variable global para todos ellos la cual le dice a los montones de piedras (objetos)
/// si el jugador ya tiene un objeto en mano o no, ya que sólo puede tener uno a la vez. Además, se encarga de entrar/salir del
/// nuevo modo (lanzamiento) y del movimiento del cursor en dicho modo.
/// Esta clase consta de dos métodos públicos, que ayudan con la gestión de la variable global _objectInHand:
/// PublicObjectController(): devuelve el valor de dicha variable.
/// SwitchPublicObjectController(): cambia el valor de la variable global, útil para cambiar el valor de manera indirecta
/// desde otro script (GetObject.cs).
/// </summary>
public class ThrowingSystem : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [Header("Prefabs")]
    [SerializeField] private GameObject RockPrefab;
    [SerializeField] private GameObject Cursor;

    [Header("Scripts")]
    [SerializeField] private PlayerMovement Movement;
    [SerializeField] private FollowPlayer Camera;

    [Header("Atributos")]
    [SerializeField] private float CursorSpeed;
    [SerializeField] private float ObjectSpeed = 3f;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    
    /// <summary>
    /// Es True si el objeto lanzado está en movimiento, False en caso contrario
    /// </summary>
    private bool _objectIsMoving = false;

    /// <summary>
    /// Es True si el jugador ya tiene un objeto, False en caso contrario
    /// </summary>
    private bool _objectInHand = false;

    private bool _inThrowingState = false;

    private GetObject _objectPrefab;
    private GameObject _object;


    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void Start()
    {
        // desactivamos la visibilidad del cursor
        Cursor.SetActive(false);
        // posición del cursor (con un pequeño offset a la derecha)
        Cursor.transform.position = Movement.transform.position + new Vector3(1, 0, 0);
    } // Start

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        // si tiene un objeto, tienes la opción de entrar al modo lanzamiento
        if (_objectInHand)
        {
            // primero comprobamos si el objeto está en movimiento (si ya se ha creado) para bloquear cualquier otro tipo
            // de input por parte del usuario (movimiento del cursor, jugador, etc) y centrarnos sólo en el movimiento
            // del objeto lanzado
            if (_objectIsMoving)
            {
                ThrowObject();
                // comprobamos si el objeto ha llegado a su destino
                if (_object.transform.position == Cursor.transform.position)
                {
                    // creamos el círculo de sonido cuando el objeto llega a su destino
                    ObjectNoise noise = _object.GetComponent<ObjectNoise>();
                    if (noise != null)
                    {
                        noise.GenerateNoise();
                    }
                    Destroy(_object);

                    // cambiamos todas las variables a sus estados iniciales
                    _objectIsMoving = false;
                    _objectInHand = false;
                    _inThrowingState = false;

                    // volvemos a nuestro estado inicial (habilitamos el jugador y la cámara y escondemos el cursor)
                    Movement.enabled = true;
                    Camera.enabled = true;
                    Cursor.SetActive(false);

                    // posición del cursor (con un pequeño offset a la derecha)
                    Cursor.transform.position = Movement.transform.position + new Vector3(1, 0, 0);
                }
            }
            else if (InputManager.Instance.ThrowWasPressedThisFrame())
            {
                // invertimos el valor del booleano
                _inThrowingState = !_inThrowingState;
            }
            else if (_inThrowingState)
            {
                // comprobamos si el jugador tiene seleccionada la posición para lanzar el objeto o si quiere salir/entrar en el estado de lanzamiento
                if (InputManager.Instance.ConfirmThrowWasPressedThisFrame())
                {
                    // comenzamos el movimiento del objeto
                    ThrowObject();
                    LevelManager.Instance.RockPicked(false);
                }
                else
                {
                    // movimiento del cursor
                    CursorMovement();
                }
            }
            else
            {
                // volvemos a nuestro estado inicial (habilitamos el jugador y la cámara y escondemos el cursor)
                Movement.enabled = true;
                Camera.enabled = true;
                Cursor.SetActive(false);
                // posición del cursor (con un pequeño offset a la derecha)
                Cursor.transform.position = Movement.transform.position + new Vector3(1, 0, 0);
            }
        }
    } // Update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // comprobamos que entramos en el collider de un objeto y no cualquier otro
        _objectPrefab = collision.GetComponent<GetObject>();
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    /// <summary>
    /// Devuelve el valor de la variable global del objeto en mano al script GetObject del objeto 
    /// al que el jugador se acerca
    /// </summary>
    /// <returns></returns>
    public bool PublicObjectController()
    {
        return _objectInHand;
    }

    /// <summary>
    /// Cambia el valor de la variable _objectInHand
    /// </summary>
    public void SwitchPublicObjectController()
    {
        _objectInHand = !_objectInHand;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    /// <summary>
    /// Método que gestiona el movimiento del cursor y la confirmación del mismo, para poder lanzar el objeto a una
    /// posición determinada por el jugador
    /// </summary>
    private void CursorMovement()
    {
        // bloqueamos el movimiento del jugador y hacemos que el cursor aparezca
        Movement.enabled = false;
        Camera.enabled = false;
        Cursor.SetActive(true);

        // obtenemos la dirección del InputManager
        Vector2 direction = InputManager.Instance.MovementVector;
        Vector3 cursorDir = new Vector3(direction.x, direction.y, Cursor.transform.position.z);

        // movimiento del cursor
        Cursor.transform.Translate(cursorDir * CursorSpeed * Time.deltaTime);
        
    } // MovimientoCursor

    /// <summary>
    /// Método para crear y mover el objeto lanzado desde la posición del jugador hasta la posición del cursor
    /// </summary>
    private void ThrowObject()
    {
        // creamos el throwable object con Instantiate al inicio del movimiento
        if (!_objectIsMoving)
        {
            // if else para detectar si es roca o jarron
            _object = Instantiate(RockPrefab, Movement.transform.position, Movement.transform.rotation);
            _objectIsMoving = true;
        }

        // movemos el objeto de manera progresiva
        _object.transform.position = Vector3.MoveTowards(_object.transform.position, Cursor.transform.position, 
            ObjectSpeed * Time.deltaTime);

    } // ThrowObject 
    #endregion

} // class ThrowingSystem 
// namespace
