using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace RPG.Interact.Base.NPC.Dialogue
{
	[System.Serializable]
	public class DialogueText
	{

		public string name;

		[TextArea(3, 10)]
		public string[] sentences;

	}

	public class DialogueManager : MonoBehaviour
	{
		#region Singleton
		public static DialogueManager instance;

		private void Awake()
		{
			if (instance != null)
			{
				Debug.LogWarning("More than one insance of Dialogue manager");
				return;
			}
			instance = this;
		}
		#endregion

		public TextMeshProUGUI nameText;
		public TextMeshProUGUI dialogueText;

		public Animator animator;

		private Queue<string> sentences;

		void Start()
		{
			sentences = new Queue<string>();
		}

		public void StartDialogue(DialogueText dialogue)
		{
			animator.SetBool("IsOpen", true);

			nameText.text = dialogue.name;

			sentences.Clear();

			foreach (string sentence in dialogue.sentences)
			{
				sentences.Enqueue(sentence);
			}

			DisplayNextSentence();
		}

		public void DisplayNextSentence()
		{
			if (sentences.Count == 0)
			{
				EndDialogue();
				return;
			}

			string sentence = sentences.Dequeue();
			StopAllCoroutines();
			StartCoroutine(TypeSentence(sentence));
		}

		IEnumerator TypeSentence(string sentence)
		{
			dialogueText.text = "";
			foreach (char letter in sentence.ToCharArray())
			{
				dialogueText.text += letter;
				yield return null;
			}
		}

		void EndDialogue()
		{
			animator.SetBool("IsOpen", false);
		}

	}
}
