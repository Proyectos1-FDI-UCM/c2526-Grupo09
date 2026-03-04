using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    //Para el editor
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Object/Dialogue Round")]

    public class DialogueRound : ScriptableObject
    {
        //Lista de turnos para los dialogos
        [SerializeField] private List<DialogueTurn> dialogueTurnsList; 

        //para poder acceder desde un script externo
        public List<DialogueTurn> DialogueTurnsList => dialogueTurnsList; 
    }
}
