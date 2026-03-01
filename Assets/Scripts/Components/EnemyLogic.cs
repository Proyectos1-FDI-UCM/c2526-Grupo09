//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Alvaro Sosa Rodriguez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class EnemyLogic : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    /// <summary>
    /// El atributo Positions define las posiciones a las que irá el enemigo cuando este patrullando.
    /// </summary>
    [SerializeField]
    private Transform[] Positions;
    /// <summary>
    /// El atributo speed define la velocidad a la que se mueve el enemigo.
    /// </summary>
    [SerializeField]
    private float Speed;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    /// <summary>
    /// La variable _posIndex es el índice que indica en que posición se encuentra el enemigo del array de posiciones
    /// </summary>
    #endregion
    private int _posIndex = 0;
    /// <summary>
    /// La variable _heardNoise determina si el enemigo a entrado en contacto con un sonido.
    /// </summary>
    private bool _heardNoise = false;
    /// <summary>
    /// La variable _isPlayerInRange determina si el jugador está dentro del rango de vision.
    /// </summary>
    private bool _isPlayerInRange = false;

    private Transform _noiseOrigin;
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        UpdateEnemyState();
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
    /// <summary>
    /// Se encarga de mover al enemigo de una posicion a otra a cierta velocidad.
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="time"></param>
    /// <param name="i"></param>
    /// 



    /// <summary>
    /// 
    /// </summary>
    private void PerformPatrol()
    {
        if (_posIndex < Positions.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, Positions[_posIndex].position, Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, Positions[_posIndex].position) < 0.1f)
            {
                _posIndex = (_posIndex + 1) % Positions.Length;
            }
        } 
    }
    /// <summary>
    /// 
    /// </summary>
    private void UpdateEnemyState()
    {
        if (!_heardNoise && !_isPlayerInRange)
        {
            PerformPatrol();
        }
        else if (_heardNoise && !_isPlayerInRange)
        {
            StartCoroutine(PerformChase());
        }
        else if (_heardNoise && _isPlayerInRange)
        {
            PerformAttack();
        }
    }

    /// <summary>
    /// Si el enemigo detecta un sonido, este se movera al origen del mismo.
    /// </summary>
    private IEnumerator PerformChase()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Chase");
        transform.position = Vector3.MoveTowards(transform.position, _noiseOrigin.position, Speed * Time.deltaTime);
    }

    /// <summary>
    /// 
    /// </summary>
    private void PerformAttack()
    {

    }

    /// <summary>
    /// Detecta si el enemigo entra en contacto con una onda de sonido.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hacer Ducktyping para saber si es un sonido. Si lo es, poner _heardNoise a true
        NoiseCircle noiseCircle = collision.gameObject.GetComponent<NoiseCircle>();
        if (noiseCircle != null && !_heardNoise)
        {
            Debug.Log("Detecta ruido");
            _noiseOrigin = noiseCircle.gameObject.transform;
            _heardNoise = true;
        }
    }
    #endregion   

} // class EnemyMovement 
// namespace
