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
    [SerializeField] private GameObject Padlock;
    [SerializeField] private GameObject FirstButton;
    [SerializeField] private AudioSource Sound;
    [SerializeField] private PlayerMovement Player;

    /// <summary>
    /// Script del botón UI
    /// </summary>
    [SerializeField] private FollowObjectUI ButtonInteract;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private bool _nearPad;
    private bool _openPad;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //solo si esta tocando la puerta y presionando E se ejecutara
        if (InputManager.Instance.InteractWasPressedThisFrame() && _nearPad&& !_openPad)
        {
              InteractLock();
        }
        else if(InputManager.Instance.ConfirmWasPressedThisFrame()&&_openPad)
        {
            CloseLock();
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            _nearPad = true;
            ButtonInteract.SetNewTarget(transform);
            ButtonInteract.ChangeText("open lock");
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            _nearPad = false;
            ButtonInteract.Deactivate();
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
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
        Player.enabled = false;
        Padlock.SetActive(true);
        _openPad = true;
        LevelManager.Instance.SetFirstButton(FirstButton);
    }

    public void CloseLock()
    {
        Padlock.SetActive(false);
        Player.enabled = true;
        _openPad = false;
    }
    public void DestroyPad()
    {
        gameObject.SetActive(false);
        Padlock.SetActive(false);
        Player.enabled = true;
    }


    #endregion

} // class ShowPadlock 
// namespace
