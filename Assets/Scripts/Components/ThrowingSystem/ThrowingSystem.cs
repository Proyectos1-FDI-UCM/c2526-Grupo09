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

    [SerializeField] private GetObject ObjectPrefab;
    [SerializeField] private GameObject RockPrefab;
    // [SerializeField] private GameObject VasePrefab;
    [SerializeField] private GameObject Cursor;
    [SerializeField] private float CursorSpeed;
    [SerializeField] private PlayerMovement Movement;
    [SerializeField] private FollowPlayer Camera;

    [SerializeField] private float ObjectSpeed = 3f;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private bool _inThrowingState = false;
    private bool _throwConfirmed = false;
    private bool _objectIsMoving = false;

    GameObject _obj;


    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void Start()
    {
        // desactivamos la visibilidad del cursor
        Cursor.SetActive(false);
        // posición del cursor (con un pequeño offset a la derecha)
        Cursor.GetComponent<Transform>().position = Movement.GetComponent<Transform>().position + new Vector3(1, 0, 0);
    } // Start

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (ObjectPrefab._hasObject)
        {
            // si tiene un objeto, tienes la opción de entrar al modo lanzamiento
            if (InputManager.Instance.ThrowWasPressedThisFrame())
            {
                // comprobamos si el objeto sigue en movimiento o si el movimiento acaba de empezar
                if (_throwConfirmed && InputManager.Instance.ThrowWasPressedThisFrame())
                {
                    _objectIsMoving = true;
                }
                else if (_objectIsMoving)
                {
                    Debug.Log("detecta lanzar objeto");
                    ThrowObject();
                    // comprobamos si el objeto ha llegado a su destino
                    if (_obj.transform.position == Cursor.transform.position)
                    {
                        Debug.Log("llegó al final negracas");
                        _inThrowingState = false;
                        _objectIsMoving = false;
                    }
                }
                else
                {
                    // invertimos el valor del booleano para detectar si empieza o termina el estado de lanzamiento
                    _inThrowingState = !_inThrowingState;
                }
                    
            }

            if (_inThrowingState)
            {
                Debug.Log("muevo el booty");
                CursorMovement();
            }
            else
            {
                // volvemos a nuestro estado inicial (movemos el jugador y escondemos el cursor)
                Movement.enabled = true;
                Cursor.SetActive(false);

                // posición del cursor (con un pequeño offset a la derecha)
                Cursor.GetComponent<Transform>().position = Movement.GetComponent<Transform>().position + new Vector3(1, 0, 0);
            }
        }
        else
        {
            Debug.Log("no tienes objeto pedazo de queer");
        }
    } // Update

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    /// <summary>
    /// descripción
    /// </summary>
    private void CursorMovement()
    {
        // bloqueamos el movimiento del jugador y hacemos que el cursor aparezca
        Movement.enabled = false;
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
            Vector3 cursorDir = new Vector3(direction.x, direction.y, Cursor.GetComponent<Transform>().position.z);

            // movimiento del cursor
            Cursor.transform.Translate(cursorDir * CursorSpeed * Time.deltaTime);
        }
        
    } // MovimientoCursor

    /// <summary>
    /// 
    /// </summary>
    private void ThrowObject()
    {
        // creamos el throwable object con Instantiate al inicio del movimiento
        if (!_objectIsMoving)
        {
            Debug.Log("creo el object");
            _obj = Instantiate(RockPrefab, Movement.transform.position, Movement.transform.rotation);
        }

        // movemos el objeto de manera progresiva
        _obj.transform.position = Vector3.MoveTowards(_obj.transform.position, Cursor.transform.position, ObjectSpeed * Time.deltaTime);
        Debug.Log("NOS MOVEMOS NEGRAS");

    } // ThrowObject

    #endregion


} // class ThrowingSystem 
// namespace
