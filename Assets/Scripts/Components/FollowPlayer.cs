//---------------------------------------------------------
// Permite a la camara seguir al jugador.
// Hao Zheng
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections.Generic;
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

    [SerializeField] private LayerMask OcclusionMask;


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


    /// <summary>
    /// Array con objetos que son transparentes actualmente.
    /// </summary>
    private SpriteRenderer[] _transparentObjects = new SpriteRenderer[MAX];
    private int _transparentObjectsCount = 0;

    /// <summary>
    /// Array con objetos golpeados por el raycast.
    /// </summary>
    private SpriteRenderer[] _currentHits = new SpriteRenderer[MAX];
    private int _currentHitsCount = 0;

    private const int MAX = 50;

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
    /// 

    private void LateUpdate()
    {

        _currentHitsCount = 0;

        // Creamos el rayo para el raycasting
        Vector2  origin = transform.position;
        Vector2 direction = (Target.position - transform.position);
        float distance = direction.magnitude;

        // Uso RaycastAll para que detecte más de un objeto.
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction.normalized, distance, OcclusionMask);

        
        for (int i = 0; i < hits.Length; i++)
        {
            // Guardamos el objeto del array en una variable local.

            RaycastHit2D hit = hits[i];

            // Si es distinto del target (player).
            if (hit.transform != Target)
            {
                // Guardamos el spriterenderer del objeto colisionado si tiene.
                SpriteRenderer rend = hit.collider.GetComponent<SpriteRenderer>();

                if (rend != null)
                {
                    // Evitar duplicados.
                    bool alreadyAdded = false;
                    for (int j = 0; j < _currentHitsCount; j++)
                    {
                        if (_currentHits[j] == rend)
                        {
                            alreadyAdded = true;
                           
                        }
                    }

                    // Si no ha sido guardado aun.
                    if (!alreadyAdded && _currentHitsCount < MAX)
                    {
                        _currentHits[_currentHitsCount] = rend;
                        _currentHitsCount++;

                        // Comprobar si ya es transparente.
                        bool wasAlreadyTransparent = false;

                        for (int j = 0; j < _transparentObjectsCount; j++)
                        {
                            // Lo guardo en un array distinto.
                            if (_transparentObjects[j] == rend)
                            {
                                wasAlreadyTransparent = true;
                               
                            }
                        }

                        if (!wasAlreadyTransparent)
                        {
                            // transparentamos el SpriteRenderer.
                            SetAlpha(rend, 0.3f);
                        }
                    }
                }
                
            }
        }
        // Restaurar opacidad
        for (int i = 0; i < _transparentObjectsCount; i++)
        {
            SpriteRenderer rend = _transparentObjects[i];

            bool stillHit = false;

            for (int j = 0; j < _currentHitsCount; j++)
            {
                if (_currentHits[j] == rend)
                {
                    stillHit = true;
                    
                }
            }

            if (!stillHit)
            {
                SetAlpha(rend, 1f);
            }
        }

        // Guardarmos objeto en el array transparentes.
        _transparentObjectsCount = _currentHitsCount;
        for (int i = 0; i < _currentHitsCount; i++)
        {
            _transparentObjects[i] = _currentHits[i];
        }

        Vector2 moveDir = InputManager.Instance.MovementVector;
        Vector2 panDir = InputManager.Instance.PanVector;
        Vector3 panOffset;
        Vector3 pos = Target.position + Offset;
        Vector3 targetOffset;

            

        if (!PauseManager.Instance.Pause)
        {
            if (moveDir != Vector2.zero && !DialogueManager.Instance.GetIsDialogueInProgress())
            {
                targetOffset = new Vector3(moveDir.x, moveDir.y, 0).normalized * MaxDistanceMain;
                _lookOffset = Vector3.Lerp(_lookOffset, targetOffset, TimePan * Time.deltaTime);
            }
            else
            {
                _lookOffset = Vector3.Lerp(_lookOffset, Vector3.zero, TimeNormal * Time.deltaTime);
            }


            if (panDir != Vector2.zero && !DialogueManager.Instance.GetIsDialogueInProgress())
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
    void SetAlpha(SpriteRenderer rend, float alpha)
    {
        Color color = rend.color;
        color.a = alpha;
        rend.color = color;
    }
    #endregion

} // class FollowPlayer 
// namespace
