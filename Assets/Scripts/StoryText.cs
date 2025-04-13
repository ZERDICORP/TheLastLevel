// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System.Collections;

// public class StoryText : MonoBehaviour
// {
//     public GameObject dialogUI;
//     public TMP_Text dialogText;
//     [SerializeField] private string[] storyLines = { "Первый сюжетный элемент...", "Второй сюжетный элемент..." };
//     [SerializeField] private int currentLine = 0;

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     private void Start()
//     {
//         dialogText.text = storyLines[currentLine];
//     }

//     // Update is called once per frame
//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.T))
//         {
//             currentLine++;
//             if (currentLine < storyLines.Length)
//                 dialogText.text = storyLines[currentLine];
//             else
//                 dialogUI.gameObject.SetActive(false); // Скрыть диалоговую панель после последней строки
//         }
//     }
// }
