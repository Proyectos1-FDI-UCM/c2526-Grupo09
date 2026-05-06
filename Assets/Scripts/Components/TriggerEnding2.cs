//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
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
public class TriggerEnding2 : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] private GameObject Panel;  // panel dónde está la imagen
    [SerializeField] private Image Image;  // imagen del final
    [SerializeField] private float fadeDuration = 5f;  // duración fade final
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private float _timer = 0f;  // contador de tiempo
    private float _timePassed = 0f;  // tiempo transcurrido
    private bool activate = false;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (activate)
        {
            _timer += Time.deltaTime;

            if (_timer >= 3f)
            {
                _timePassed += Time.deltaTime;
                float endTime = Mathf.Clamp01(_timePassed / fadeDuration);
                Color color = Image.color;
                color.a = Mathf.Lerp(1f, 0f, endTime);
                Image.color = color;
                if (endTime >= 1f)
                {
                    SceneManager.LoadScene("CreditsEnding");
                }
            }
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public void ActivateImage()
    {
        Panel.SetActive(true);
        activate = true;
    }
    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion   

} // class TriggerEnding2 
// namespace
