//---------------------------------------------------------
// Cambia la imagen de fondo progresivamente
// Carmen Rosino Vílchez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] private float fadeDuration = 5f;  // duración fade final
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // VARIABLES PARA GESTIONAR EL CAMBIO DE FONDO
    private float _timer = 0f;  // contador de tiempo
    private int _currentIndex = 0;  // pos actual en el array

    // VARIABLES PARA GESTIONAR EL FADE OUT
    private float _timePassed= 0f;  // tiempo transcurrido
    private bool _isFading = false;  // si esta el fade activo
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
        else if (_isFading)
        {
            _timePassed += Time.deltaTime;
            float endTime = Mathf.Clamp01(_timePassed / fadeDuration);
            Color color = BackgroundImage.color;
            color.a = Mathf.Lerp(1f, 0f, endTime);
            BackgroundImage.color = color;
            if (endTime >= 1f)
            {
                _isFading = false;
                SceneManager.LoadScene("CreditsEnding2");
            }    
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Método que activa el inicio del fade
    /// </summary>
    public void StartFade()
    {
        _timePassed = 0f;
        _isFading = true;
    }
    #endregion
} // class BackgroundManager 
// namespace
