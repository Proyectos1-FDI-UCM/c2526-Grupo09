//---------------------------------------------------------
// Controla la lógica del video
// Carmen Rosino Vílchez
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class VideoManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // el video que se va a reproducir
    [SerializeField] private VideoPlayer VideoPlayer;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        // evento de fin de video
        VideoPlayer.loopPointReached += OnVideoFinished;
        VideoPlayer.Play();
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("God");
    }

    void OnDestroy()
    {
        // limpiamos el evento al destruir el objeto
        VideoPlayer.loopPointReached -= OnVideoFinished;
    }
    #endregion   

} // class VideoManager 
// namespace
