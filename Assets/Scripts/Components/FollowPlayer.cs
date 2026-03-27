//---------------------------------------------------------
// Permite a la camara seguir al jugador.
// Hao Zheng
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;
// Añadir aquí el resto de directivas using


/// <summary>
/// Script que permite a la camara seguir al jugador indicado en el editor. 
/// Sigue al jugador con un retraso pequeño, utilizando Lerp para suavizar la transicion. 
/// También se implementa el funcionamiento del paneo de camara con retraso aparte.
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    /// <summary>
    /// Asignar desde el inspector el transform del GameObject que la cámara debe seguir.
    /// </summary>
    [SerializeField] private Transform Target;

    /// <summary>
    /// El offset que tiene que tener la cámara en base del gameobject que debe seguir.
    /// </summary>
    [SerializeField] private Vector3 Offset = new Vector3(0, 0, -10);

    [SerializeField] private float MaxDistanceMain = 2f;
    /// <summary>
    /// Velocidad de interpolación cuando la cámara sigue al gameobject, Contola lo rápido que la cámara alcanza la posición del jugador
    /// valores bajos hace que el movimiento sea suave y con más delay, mientras que valores altos hace lo contrario.
    /// </summary>
    [SerializeField] private float TimeNormal = 1f;
    [Header("Parámetros del paneo")]
    [SerializeField] private float TimePan = 1f;
    /// <summary>
    /// Es la distancia máxima que se puede desplazar la cámara respecto al jugador en el momento de hacer el paneo.
    /// </summary>
    [SerializeField] private float MaxDistancePan = 1f;


    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private Vector3 _panOffset;
    private Vector3 _lookOffset;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        transform.position = transform.position + Offset;
    }
    /// <summary>
    /// Se ejecuta cada frame, después de que se han llamado todas las funciones.
    /// Se utiliza esto para garantizar que la posición de la cámara se actualice después de que se haya movido el jugador.
    /// </summary>
    private void LateUpdate()
    {
        Vector2 moveDir = InputManager.Instance.MovementVector;
        Vector2 panDir = InputManager.Instance.PanVector;
        Vector3 panOffset;
        Vector3 pos = Target.position + Offset;
        Vector3 targetOffset;

        if (!PauseManager.Instance.Pause)
        {
            if (moveDir != Vector2.zero)
            {
                targetOffset = new Vector3(moveDir.x, moveDir.y, 0).normalized * MaxDistanceMain;
                _lookOffset = Vector3.Lerp(_lookOffset, targetOffset, TimePan * Time.deltaTime);
            }
            else
            {
                _lookOffset = Vector3.Lerp(_lookOffset, Vector3.zero, TimeNormal * Time.deltaTime);
            }


            if (panDir != Vector2.zero)
            {
                panOffset = new Vector3(panDir.x, panDir.y, 0).normalized * MaxDistancePan;
            }
            else
            {
                panOffset = Vector3.zero;
            }
            _panOffset = Vector3.Lerp(_panOffset, panOffset, TimePan * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, pos + _panOffset + _lookOffset, TimeNormal * Time.deltaTime);
        }


    }
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

    #endregion

} // class FollowPlayer 
// namespace
