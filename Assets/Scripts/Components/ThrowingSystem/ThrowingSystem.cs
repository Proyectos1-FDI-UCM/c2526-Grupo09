//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Diego Martín
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Timeline;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class ThrowingSystem : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [Header("Prefabs")]
    [SerializeField] private GameObject RockPrefab;
    // [SerializeField] private GameObject VasePrefab;
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
    /// Detecta si se ha entrado en el modo lanzamiento o no
    /// </summary>
    private bool _inThrowingState = false;

    /// <summary>
    /// Es True si se ha confirmado la posición del cursor, False en caso contrario
    /// </summary>
    private bool _throwConfirmed = false;
    
    /// <summary>
    /// Es True si el objeto lanzado está en movimiento, False en caso contrario
    /// </summary>
    private bool _objectIsMoving = false;

    /// <summary>
    /// Es True si el jugador ya tiene un objeto, False en caso contrario
    /// </summary>
    private bool _objectInHand = false;

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
                    Debug.Log("llegó al final");
                    _inThrowingState = false;
                    _objectIsMoving = false;
                    _objectInHand = false;
                    _throwConfirmed = false;

                    // volvemos a nuestro estado inicial (habilitamos el jugador y la cámara y escondemos el cursor)
                    Movement.enabled = true;
                    Camera.enabled = true;
                    Cursor.SetActive(false);

                    // posición del cursor (con un pequeño offset a la derecha)
                    Cursor.transform.position = Movement.transform.position + new Vector3(1, 0, 0);
                    //SwitchPublicObjectController();
                }
            }
            else if (InputManager.Instance.ThrowWasPressedThisFrame())
            {
                if (_throwConfirmed)
                {
                    // comenzamos el movimiento del objeto
                    ThrowObject();
                }
                // invertimos el valor del booleano para detectar si empieza o termina el estado de lanzamiento
                _inThrowingState = !_inThrowingState;
            }
            else if (_inThrowingState)
            {
                // movimiento del cursor
                Debug.Log("muevo el booty");
                CursorMovement();
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
        // temporal
        else if (_objectPrefab != null)
        {
            Debug.Log("no tienes objeto");
        }
        
    } // Update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // comprobamos que entramos en el collider de un objeto y no cualquier otro
        _objectPrefab = collision.GetComponent<GetObject>();
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        _objectPrefab = collision.GetComponent<GetObject>();
    }*/


    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool PublicObjectController()
    {
        return _objectInHand;
    }

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

        if (InputManager.Instance.ConfirmThrowWasPressedThisFrame())
        {
            // invertimos el valor
            _throwConfirmed = !_throwConfirmed;
        }

        if (!_throwConfirmed)
        {
            //Obtenemos la dirección del InputManager
            Vector2 direction = InputManager.Instance.MovementVector;
            Vector3 cursorDir = new Vector3(direction.x, direction.y, Cursor.transform.position.z);

            // movimiento del cursor
            Cursor.transform.Translate(cursorDir * CursorSpeed * Time.deltaTime);
        }
        
    } // MovimientoCursor

    /// <summary>
    /// Método para crear y mover el objeto lanzado desde la posición del jugador hasta la posición del cursor
    /// </summary>
    private void ThrowObject()
    {
        // creamos el throwable object con Instantiate al inicio del movimiento
        if (!_objectIsMoving)
        {
            Debug.Log("creo el object");
            // if else para detectar si es roca o jarron
            _object = Instantiate(RockPrefab, Movement.transform.position, Movement.transform.rotation);
            _objectIsMoving = true;
        }

        // movemos el objeto de manera progresiva
        _object.transform.position = Vector3.MoveTowards(_object.transform.position, Cursor.transform.position, 
            ObjectSpeed * Time.deltaTime);
        Debug.Log("NOS MOVEMOS");

    } // ThrowObject 



    #endregion


} // class ThrowingSystem 
// namespace
