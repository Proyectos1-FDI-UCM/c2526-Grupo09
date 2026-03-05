//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    /// <summary>
    /// velocidad del jugador al caminar
    /// </summary>
    [SerializeField]
    private float walkSpeed = 2.0f;
    /// <summary>
    /// velocidad del jugador al correr
    /// </summary>
    [SerializeField]
    private float runSpeed = 3.5f;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)


    Vector3 _posAnterior;  //  posición que tenía antes el jugador
    Vector3 _posActual;  // posición actual del jugador

    /// <summary>
    /// booleano que se encarga de saber si es jugador esta escondido o no
    /// </summary>
    private bool _isHidden = false;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// En cada frame se mueve al jugador
    /// </summary>
    void Update()
    {
        if (!_isHidden)
        {
            _posAnterior = transform.position;
            MovePlayer();
            _posActual = transform.position;

            // Solo intentamos hacer ruido si nos hemos movido Y tenemos el script de ruido
            if (_posAnterior != _posActual)
            {
                PlayerNoise playerNoise = GetComponent<PlayerNoise>();
                if (playerNoise != null)
                {
                    playerNoise.PlayerMoving();
                }
            }
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

    /// <summary>
    /// Permite a otros scripts consultar si el jugador está escondido.
    /// </summary>
    public bool GetIsHidden()
    {
        return _isHidden;
    }

    /// <summary>
    /// Permite al escondite cambiar el estado de visibilidad del jugador.
    /// </summary>
    public void SetHidden(bool hidden)
    {
        _isHidden = hidden;
        Debug.Log("se ha cambiado isHiden a: " + GetIsHidden());
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    /// <summary>
    /// Método que se encarga de manejar el movimeinto del jugador
    /// </summary>
    void MovePlayer()
    {
        //Obtenemos la dirección del InputManager
        Vector2 direction = InputManager.Instance.MovementVector;
        //Debug.Log(direction);

        //Variable para guardar la velocidad que se usa en ese momento
        float currentSpeed = walkSpeed;

        //Comprobamos si se pulsa el Shift
        if (InputManager.Instance.RunIsPressed())
        {
            //Usamos la velocidad de correr
            currentSpeed = runSpeed;
            //_correr = true;
        }
        else
        {
            //Usamos la velocidad de caminar
            currentSpeed = walkSpeed;
            //_correr = false;
        }

        //Aplicamos el movimiento
        //Multiplicamos la dirección por la velocidad y el tiempo
        transform.Translate(direction * currentSpeed * Time.deltaTime);
    }
    #endregion

} // class PlayerMovement 
// namespace
