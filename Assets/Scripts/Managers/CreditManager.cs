//---------------------------------------------------------
// Gestiona que cuando se terminen los créditos vuelva a menú
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
public class CreditManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private Transform Credits;  // creditos
    [SerializeField] private int FinalPos;  // posición final
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // tiempo de espera para cambiar de escena
    private float _waitingTime = 10f;
    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // cuando los créditos lleguen a la posición final
        if (Credits.position.y >= FinalPos)
        {
            // espera 3 segundos y cambia de escena
            if (_waitingTime > 0)
            {
                _waitingTime -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
    #endregion

} // class CreditManager 
// namespace
