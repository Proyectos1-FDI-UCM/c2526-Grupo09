//---------------------------------------------------------
// Script que detecta cuando el jugador (un gameObject con el script PlayerController) entra en el collider (trigger) del objeto para dar la opción de 
// poder recoger el objeto, y lo añade a la mano del jugador (con un bool).
// Diego Martín Gutiérrez
// Bouquet of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class GetObject : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    /// <summary>
    /// Variable que representa el InputManager, se debe configurar en el editor para que reconozca al gameObject. 
    /// Se utiliza para llamar a los métodos public del Script InputManager.
    /// </summary>
    public InputManager Input;

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
    /// Variable que contiene el script PlayerController, se utiliza para detectar al gameObject Player.
    /// </summary>
    private PlayerController _player;

    /// <summary>
    /// Es True cuando el jugador posee un objeto, False si no tiene ninguno.
    /// </summary>
    private bool _hasObject = false;

    /// <summary>
    /// Detecta si un gameObject con el script PlayerController (el jugador) está dentro del trigger del objeto.
    /// </summary>
    private bool _insideCollider = false;


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
    void Start()
    {
        // comprobamos que el componente está bien configurado desde el editor y que la escena esté bien montada
        if (Input == null) {
            Debug.Log("gameObject InputManager not found, please check that it is attached to this script in the editor");
            Destroy(this);
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_insideCollider && !_hasObject) { 
            Debug.Log("no tiene objeto, puedes cogerlo");
                if (Input.InteractWasPressedThisFrame()) {
                    _hasObject = true;
                    Debug.Log("has cogido el objeto yayyy");
                }
        }
    } // Update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.gameObject.GetComponent<PlayerController>();
        if (_player != null) {
            _insideCollider = true;
            // if (!_hasObject)
                // activa el GUI de la burbuja
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _player = collision.gameObject.GetComponent<PlayerController>();
        if (_player != null) {
            _insideCollider = false;
            // if (!_hasObject)
                // desactiva el GUI de la burbuja
        }
    }

    /* NO BORRARLO X AHORA; PROBLEMA: ESTE METODO SOLO SE EJECUTA EN LOS FRAMES DONDE SE MUEVE ALGUN COLLIDER/RIGIDBODY, SI EL PLAYER ESTA QUIETO NO FURULA
    private void OnTriggerStay2D (Collider2D collision) // mientras el jugador permanezca dentro del trigger, tendrá la opción de recoger el objeto
    {
        _player = collision.gameObject.GetComponent<PlayerController>();
        if (_player != null)
            if (!_hasObject) {
                // aparece GUI q muestra el botón (mando/teclado --> lo debe diferenciar) q sea un metodo a parte del gameManager o algo
                // ...

                Debug.Log("no tiene objeto, puedes cogerlo");
                if (Input.InteractWasPressedThisFrame()) {
                    _hasObject = true;
                    Debug.Log("has cogido el objeto yayyy");
                }
            }
    } // OnTriggerStay2D
    */

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

} // class GetObject 
// namespace
