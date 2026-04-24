//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Diego Martín
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class FlowerTypes : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

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
    public int NumPetals
    {
        get { return _nPetals; }
    }



    public void DefineType(int index)
    {
        // #############
        // TODO LO DEL SPRITE RENDERER HAY QUE CAMBIARLO PARA QUE PONGA EL SPRITE DE LA FLOR Y NO CAMBIE EL COLOR DEL CUADRADO

        _tempColor = GetComponent<SpriteRenderer>();
        // tipos definidos de las flores, cada color tiene un número de pétalos asignado
        // añadir aquí todos los tipos de flores que se deseen (teniendo en cuenta el número posiciones)
        switch (index)
        {
            case 0:
                _flowerColor = "RED";
                _nPetals = 2;
                _tempColor.material.color = Color.red;
            break;

            case 1:
                _flowerColor = "BLUE";
                _nPetals = 3;
                _tempColor.material.color = Color.blue;
            break;

            case 2:
                _flowerColor = "YELLOW";
                _nPetals = 5;
                _tempColor.material.color = Color.yellow;
            break;
            
            case 3:
                _flowerColor = "ORANGE";
                _nPetals = 4;
                _tempColor.material.color = Color.cyan;
            break;

            case 4:
                _flowerColor = "PINK";
                _nPetals = 3;
                _tempColor.material.color = Color.black;
            break;

            case 5:
                _flowerColor = "PURPLE";
                _nPetals = 2;
                _tempColor.material.color = Color.green;
            break;
        }
    }

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion   

} // class FlowerTypes 
// namespace
