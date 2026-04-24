//---------------------------------------------------------
// Script para los turnos de diálogo.
// Rafa Campos García
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------


using UnityEngine;

namespace DialogueSystem
{
    // Permite que Unity pueda serializar esta clase y mostrarla dentro del Inspector cuando esté en una List<>
    [System.Serializable]

    ///<summary>
    /// Una unidad mínima de diálogo. Dice quién y qué habla.
    ///</summary>
    public class DialogueTurn // 
    {
        /// <summary>
        /// Personaje que habla en este turno de diálogo.
        /// Ya que usamos este script en una lista, añadimos esta línea para que salga en el inspector del scriptableObject.
        /// </summary>

        [SerializeField] private DialogueCharacter character;

        public DialogueCharacter GetCharacter() { return character; }

        /// <summary>
        /// Línea de diálogo que dirá el personaje.
        /// TextArea hace que el campo sea más grande en el Inspector (mínimo 2 líneas, máximo 4 visibles sin expandir).
        /// </summary>

        [SerializeField, TextArea(2, 4)]
        private string dialogueLine = string.Empty;

        /// <summary>
        /// Propiedad pública de solo lectura.
        /// Permite acceder al texto desde otros scripts sin poder modificarlo directamente
        /// </summary>
        /// 
        public string DialogueLine => dialogueLine;
        /*  seria como escribir:
         public string GetDialogueLine()
              {
                   return dialogueLine;
              }   de manera abreviada (cambiar esto implica reescribir todos los diálogos)
        */
    }
}