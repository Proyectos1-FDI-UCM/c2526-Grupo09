//---------------------------------------------------------
// Contiene todos los tipos distintos de flores
// Diego Martín
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using

/// <summary>
/// Clase que contiene todos los tipos de flores del último nivel (DefineType()). Cuenta con dos métodos getter para acceder
/// al color y número de pétalos de las flores.
/// </summary>
public class FlowerTypes : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField] Sprite[] SpriteFlower;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    // número de pétalos, usados en el código de la puerta
    int _nPetals = 0;

    string _flowerColor = string.Empty;

    SpriteRenderer _tempColor;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

 
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // métodos getter
    public int NumPetals
    {
        get { return _nPetals; }
    }
    public string FlowerColor
    {
        get { return _flowerColor; }
    }

    public void DefineType(int index)
    {
        _tempColor = GetComponent<SpriteRenderer>();
        // tipos definidos de las flores, cada color tiene un número de pétalos asignado
        // añadir aquí todos los tipos de flores que se deseen (teniendo en cuenta el número posiciones)
        switch (index)
        {
            case 0:
                _nPetals = 2;
                _flowerColor = "RED";
                break;

            case 1:
                _nPetals = 3;
                _flowerColor = "BLUE";
                break;

            case 2:
                _nPetals = 5;
                _flowerColor = "YELLOW";
                break;

            case 3:
                _nPetals = 4;
                _flowerColor = "CYAN";
                break;

            case 4:
                _nPetals = 6;
                _flowerColor = "BLACK";
                break;

            case 5:
                _nPetals = 7;
                _flowerColor = "WHITE";
                break;

        }
        _tempColor.sprite = SpriteFlower[index];
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion

} // class FlowerTypes 
// namespace
