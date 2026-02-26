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
/// Script para la gestión de recogida e interacción de objetos. Como el jugador sólo puede tener un objeto que se puede lanzar (throwable objects) a la vez,
/// este script prohibe la interacción con el objeto al que este componente está unido mientras no se haya lanzado/tirado el anterior objeto.
/// Está programado para que cuando el jugador entre en el collider de la entidad, tenga la oportunidad de realizar la acción asociada a "Interact" para recoger un
/// throwable object.
/// </summary>
public class GetObject : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    /// <summary>
    /// Variable que representa el InputManager, se debe configurar en el editor para que reconozca al gameObject. 
    /// Se utiliza para llamar a los métodos public del Script InputManager.
    /// </summary>
    public InputManager Input;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

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
    /// True si está dentro, False en caso contrario.
    /// </summary>
    private bool _insideCollider = false;


    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

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
        if (_insideCollider && !_hasObject) { // si el jugador está dentro del collider y no tiene objeto, entra en el if
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
