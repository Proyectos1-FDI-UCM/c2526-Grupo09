//---------------------------------------------------------
// Posición del juagdor al respawnear en un checkpoint
// Carmen Rosino Vílchez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PlayerPos : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject cam;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    private void Awake()
    {
        Debug.Log("PlayerPos");
        Debug.Log("PLAYER POS: " + transform.position);
        transform.position = CheckPointSystem.Instance.GetLastCheck();
        if (cam != null )
        {
            cam.transform.position = transform.position;
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public void Respawn()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

} // class PlayerPos 
// namespace
