//---------------------------------------------------------
// Cambia la imagen de fondo progresivamente
// Carmen Rosino Vílchez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class BackgroundManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private Image BackgroundImage;
    [SerializeField] private Sprite[] fondos;  // 4 fondos en orden
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _timer = 0f;  // contador de tiempo
    private int _currentIndex = 0;  // pos actual en el array
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        BackgroundImage.sprite = fondos[0];
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // si ya estamos en el último fondo no se hace nada
        if (_currentIndex < fondos.Length - 1)
        {
            _timer += Time.deltaTime;

            if (_timer >= 2f)
            {
                _timer = 0f;
                _currentIndex++;
                BackgroundImage.sprite = fondos[_currentIndex];
            }
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion   

} // class BackgroundManager 
// namespace
