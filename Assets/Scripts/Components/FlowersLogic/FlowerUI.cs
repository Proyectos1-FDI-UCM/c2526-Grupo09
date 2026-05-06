//---------------------------------------------------------
// Script que maneja el UI del nivel final, se encarga de poner el color y el código al recoger una flor.
// Diego Martín
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class FlowerUI : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [Header("Sprites de los colores")]
    [SerializeField] private Sprite Red;
    [SerializeField] private Sprite Blue;
    [SerializeField] private Sprite Cyan;
    [SerializeField] private Sprite Yellow;
    [SerializeField] private Sprite Black;
    [SerializeField] private Sprite White;

    [Header("GUI")]
    // introducir en orden de abajo a arriba
    [SerializeField] private TextMeshProUGUI[] TextFields;
    [SerializeField] private Image[] ImageFields;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private Sprite _colorUI;
    private int _counter = 0;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    private void Start()
    {
        for (int i = 0;  i < TextFields.Length; i++)
            TextFields[i].text = string.Empty;

        for (int i = 0; i < ImageFields.Length; i++)
            ImageFields[i].enabled = false;
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Gestiona el UI de los colores y código de las flores.
    /// </summary>
    /// <param name="flowerCode"></param>
    /// <param name="flowerColor"></param>
    /// <param name="colorUI"></param>
    public void ManageUI(int flowerCode, string flowerColor)
    {
        PutColorAndCode(flowerColor, flowerCode);
        if (_counter < ImageFields.Length)
        {
            ImageFields[_counter].sprite = _colorUI;
            ImageFields[_counter].enabled = true;
            // vamos aumentando el contador para desplazarnos hacia arriba en el UI y no sobreescribir imágenes ya colocadas
            _counter++;
        }
    }

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    /// <summary>
    /// Selecciona la imagen del color de la flor y el texto.
    /// </summary>
    /// <param name="colorUI"></param>
    /// <param name="flowerColor"></param>
    private void PutColorAndCode(string flowerColor, int flowerCode)
    {
        TextFields[_counter].text = "= " + flowerCode;
        switch (flowerColor)
        {
            case "RED": _colorUI = Red; break;
            case "BLUE": _colorUI = Blue; break;
            case "YELLOW": _colorUI = Yellow; break;
            case "CYAN": _colorUI = Cyan; break;
            case "BLACK": _colorUI = Black; break;
            case "WHITE": _colorUI = White; break;
        }
    }

    #endregion   

} // class FlowerUI 
// namespace
