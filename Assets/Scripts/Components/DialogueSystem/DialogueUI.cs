//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
/// 

namespace DialogueSystem
{
    public class DialogueUI : MonoBehaviour
    {
        // ---- ATRIBUTOS DEL INSPECTOR ----
        #region Atributos del Inspector (serialized fields)
        // Documentar cada atributo que aparece aquí.
        // El convenio de nombres de Unity recomienda que los atributos
        // públicos y de inspector se nombren en formato PascalCase
        // (palabras con primera letra mayúscula, incluida la primera letra)
        // Ejemplo: MaxHealthPoints

        // UI del dialogo
        [SerializeField] private RectTransform dialogBox; // referencia al transform
        [SerializeField] private Image characterPhoto; // imagen del personaje
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private TextMeshProUGUI dialogArea;

        #endregion

        // ---- ATRIBUTOS PRIVADOS ----
        #region Atributos Privados (private fields)
        // Documentar cada atributo que aparece aquí.
        // El convenio de nombres de Unity recomienda que los atributos
        // privados se nombren en formato _camelCase (comienza con _, 
        // primera palabra en minúsculas y el resto con la 
        // primera letra en mayúsculas)
        // Ejemplo: _maxHealthPoints

        #endregion

        // ---- MÉTODOS PÚBLICOS ----
        #region Métodos públicos
        // Documentar cada método que aparece aquí con ///<summary>
        // El convenio de nombres de Unity recomienda que estos métodos
        // se nombren en formato PascalCase (palabras con primera letra
        // mayúscula, incluida la primera letra)
        // Ejemplo: GetPlayerController

        public void ShowDialogBox() //mostrar caja de dialogo
        {
            //Muestra la caja del dialogo.
            dialogBox.gameObject.SetActive(true); 
        }

        public void HideDialogBox() 
        {
            //Ocultar caja de dialogo.
            dialogBox.gameObject.SetActive(false); 
        }

        public void setCharacterInfo(DialogueCharacter character) 
        {

            //Si no hay personaje añadido, sale del método.
            if (character == null) return;

            //Configurar informacion del personaje.
            characterPhoto.sprite = character.ProfilePhoto;
            characterName.text = character.Name;
        }

        public void ClearDialogArea() //cambiar el dialogo
        {
            //Borrar dialogo.
            dialogArea.text = string.Empty;
        }

        public void SetDialogArea(string text) //cambair un dialogo entero directamente
        {
            //Remplaza el texto.
            dialogArea.text = text; 
        }

        public void AppendToDialogArea(char letter)
        {
            //Le pasamos el caracter y se va a ir encadenando.
            dialogArea.text += letter; 
        }

        #endregion

        // ---- MÉTODOS PRIVADOS ----
        #region Métodos Privados
        // Documentar cada método que aparece aquí
        // El convenio de nombres de Unity recomienda que estos métodos
        // se nombren en formato PascalCase (palabras con primera letra
        // mayúscula, incluida la primera letra)

        #endregion

    }
}
