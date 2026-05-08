//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Inés de la Peña Kures
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
    [SerializeField] private FollowObjectUI ButtonInteract;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private bool _inCollider = false;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void Update()
    {
        if(_inCollider)
        {
            if (InputManager.Instance.InteractWasPressedThisFrame() && !GameManager.Instance.GetHaDormido())
            {
                Player.enabled = false;
                panelDormir.SetActive(true);
                LevelManager.Instance.SetFirstButton(SleepButton);
            }
            else if(GameManager.Instance.GetHaDormido()&& InputManager.Instance.ConfirmWasPressedThisFrame()) 
            {
                Player.enabled = false;
                panelDormir.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            _inCollider = true;
            if (!GameManager.Instance.GetHaDormido())
            {
                ButtonInteract.SetNewTarget(transform);
                ButtonInteract.ChangeText("sleep");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            _inCollider = false;
            ButtonInteract.Deactivate();

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
        ButtonInteract.Deactivate();
        LevelManager.Instance.QuitHomeDialogue();
    }

    public void CancelSleep()
    {
        panelDormir.SetActive(false);
        Player.enabled = true;
    }

    #endregion

} // class Bed 
// namespace
