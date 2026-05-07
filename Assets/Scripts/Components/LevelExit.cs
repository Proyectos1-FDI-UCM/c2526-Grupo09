//---------------------------------------------------------
// Salida del nivel
// Carmen Rosino Vílchez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class LevelExit : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject WarningMessage;
    [SerializeField] private Padlock padlock;
    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        WarningMessage.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (LevelManager.Instance.GetFlowerPicked())
            {
                if (GameManager.Instance.GetCurrentDay() == 1)
                {
                    SceneManager.LoadScene("GoingHome");
                }
                else
                {
                    SceneManager.LoadScene("GoingHome2");
                }
            }
            else if (padlock.GetDoorOpen())
            {
                SceneManager.LoadScene("PostBattle");
            }
            else
            {
                WarningMessage.SetActive(true);
            }
        }
    }
    #endregion  

} // class LevelExit 
// namespace
