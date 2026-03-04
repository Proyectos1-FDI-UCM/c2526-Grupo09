using UnityEngine;

namespace DialogueSystem
{
    // Permite que Unity pueda serializar esta clase y mostrarla dentro del Inspector cuando esté en una List<>
    [System.Serializable]
    public class DialogueTurn // Una unidad mínima de diálogo. Dice quién y qué habla.
    {
        // Personaje que habla en este turno de diálogo
        // Ya que usamos este script en una lista, añadimos esta línea para que salga en el inspector del scriptableObject.
        [field: SerializeField]
        public DialogueCharacter Character { get; private set; }

        // Línea de diálogo que dirá el personaje
        // TextArea hace que el campo sea más grande en el Inspector (mínimo 2 líneas, máximo 4 visibles sin expandir)
        [SerializeField, TextArea(2, 4)]
        private string dialogueLine = string.Empty;

        // Propiedad pública de solo lectura
        // Permite acceder al texto desde otros scripts sin poder modificarlo directamente
        public string DialogueLine => dialogueLine;
    }
}