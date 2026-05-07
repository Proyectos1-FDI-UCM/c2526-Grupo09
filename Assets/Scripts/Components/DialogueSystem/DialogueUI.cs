//---------------------------------------------------------
// Script para el UI del diálogo.
// Rafa Campos García.
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Script para el UI del diálogo. Necesita un canvas con cuatro GameObjects:
/// Una caja de diálogo, un textMesh para el nombre, textMesh para el texto y una imágen para la foto del personaje.
/// </summary>
 

namespace DialogueSystem
{
    public class DialogueUI : MonoBehaviour
    {
        // ---- ATRIBUTOS DEL INSPECTOR ----
        #region Atributos del Inspector (serialized fields)
        /// <summary>
        /// Caja de diálogo.
        /// </summary>
        [SerializeField] private RectTransform dialogBox;

        /// <summary>
        /// Imagen del personaje.
        /// </summary>
        [SerializeField] private Image characterPhoto;

        /// <summary>
        /// Nombre del personaje.
        /// </summary>
        [SerializeField] private TextMeshProUGUI characterName;

        /// <summary>
        /// Área de diálogo.
        /// </summary>
        [SerializeField] private TextMeshProUGUI dialogArea;
        #endregion

        // ---- MÉTODOS PÚBLICOS ----
        #region Métodos públicos
        // Documentar cada método que aparece aquí con ///<summary>
        // El convenio de nombres de Unity recomienda que estos métodos
        // se nombren en formato PascalCase (palabras con primera letra
        // mayúscula, incluida la primera letra)
        // Ejemplo: GetPlayerController

        /// <summary>
        /// Mostrar caja de dialogo.
        /// </summary>
        public void ShowDialogBox() //
        {
            dialogBox.gameObject.SetActive(true); 
        }

        /// <summary>
        /// Ocultar caja de diálogo.
        /// </summary>
        public void HideDialogBox() 
        {
            dialogBox.gameObject.SetActive(false); 
        }


        /// <summary>
        /// Establecer la información del personaje.
        /// </summary>
        /// <param name="character"></param>
        public void setCharacterInfo(DialogueCharacter character) 
        {

            //Si no hay personaje añadido, sale del método.
            if (character == null) return;

            //Configurar informacion del personaje.
            characterPhoto.sprite = character.ProfilePhoto;
            characterName.text = character.Name;
        }

        /// <summary>
        /// Borrar texto de la caja de diálogo.
        /// </summary>
        public void ClearDialogArea() 
        {
            //Borrar dialogo.
            dialogArea.text = string.Empty;
        }

        /// <summary>
        /// Secuencia de carácteres uno por uno en el texto del diálogo.
        /// </summary>
        /// <param name="letter"></param>
        public void AppendToDialogArea(string text)
        {
            dialogArea.text += text;
        }

        #endregion
    }
}
