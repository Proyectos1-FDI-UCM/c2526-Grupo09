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
public class Bed : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject panelDormir;
    [SerializeField] private GameObject SleepButton;  // boton de dormir
    [SerializeField] private PlayerMovement Player;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private bool _inCollider = false;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void Update()
    {
        if (_inCollider) 
        {
            if (!GameManager.Instance.GetHaDormido() && InputManager.Instance.InteractWasPressedThisFrame())
            {
                Player.enabled = false;
                panelDormir.SetActive(true);
                LevelManager.Instance.SetFirstButton(SleepButton);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            _inCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            _inCollider = false;
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public void ConfirmSleep()
    {
        Player.enabled = true;
        GameManager.Instance.Sleep();
        panelDormir.SetActive(false);
    }

    public void CancelSleep()
    {
        panelDormir.SetActive(false);
        Player.enabled = true;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion

} // class Bed 
// namespace
