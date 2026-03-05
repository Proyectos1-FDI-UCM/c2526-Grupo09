//---------------------------------------------------------
// ScriptableObject para las rondas de diálogo.
// Rafa Campos García.
// Bouquet Of Sins
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

///<summary>
///Este script se encarga de crear un scriptableObject de almacenamiento de datos para las rondas de diálogos que queramos mostrar.
///Las encapsula en listas.
///</summary>
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
