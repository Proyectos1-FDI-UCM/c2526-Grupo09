//---------------------------------------------------------
// Script para abrir el candado de la puerta final
// Hao Zheng
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class ShowPadlock : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

  
    [SerializeField] private GameObject Padlock;
    [SerializeField] private GameObject FirstButton;
    [SerializeField] private AudioSource Sound;
    /// <summary>
    /// Script del botón UI
    /// </summary>
    [SerializeField] private FollowObjectUI FollowObject;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private bool _nearPad;
    private bool _openPad;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //solo si esta tocando la puerta y presionando E se ejecutara
        if (_nearPad && InputManager.Instance.InteractWasPressedThisFrame())
        {
            if (_openPad)//si está abierto, lo cierra
            {
                CloseLock();
            }
            else InteractLock();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            _nearPad = true;
            FollowObject.SetNewTarget(transform);
            FollowObject.ChangeText("open lock");
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            _nearPad = false;
            FollowObject.Deactivate();
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
    /// Abre el candado 
    /// </summary>
    public void InteractLock()
    {
        //FollowObject.Deactivate();
        if (Sound != null)
        {
            Sound.Play();
        }
        PauseManager.Instance.PauseVariable();
        Padlock.SetActive(true);
        _openPad = true;
        LevelManager.Instance.SetFirstButton(FirstButton);
    }

    public void CloseLock()
    {
        Padlock.SetActive(false);
        PauseManager.Instance.ResumeVariable();
        _openPad=false;
    }
    public void DestroyPad()
    {
        gameObject.SetActive(false);
        Padlock.SetActive(false);
        PauseManager.Instance.ResumeVariable();
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class ShowPadlock 
// namespace
