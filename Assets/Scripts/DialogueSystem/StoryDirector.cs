using System.Collections;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using Chars.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperMarketGame {

    public class StoryDirector : Singleton<StoryDirector>
    {
        #region visual
        [SerializeField] private GameObject optionPanel = null;
        [SerializeField] private Button buttonPrefab = null;
        [SerializeField] private Text currentSentence = null;
        [SerializeField] private GameObject storyPanel;
        [SerializeField] private Image avatar;
        [SerializeField] private Text name;
        #endregion

        [SerializeField] private float speed = 20f;

        public Property<Actor> currentActor = new Property<Actor>();
        private Choice choiceSelected;
        public bool currentTextFinish = true;
        public GameObject ClickToContinue;

        protected override void Awake()
        {
            base.Awake();
            currentSentence.text = string.Empty;
            storyPanel.SetActive(false);
            currentActor.AddEvent(OnActorChanged, this);
            //choiceSelected.AddEvent(OnChoiceSelected, this);
        }

        private void OnActorChanged(Actor obj)
        {
            name.text = currentActor.Value.name;
            storyPanel.SetActive(true);
            avatar.sprite = currentActor.Value.avatar;

            if (currentActor.Value.story != null && currentActor.Value.story.canContinue)
                AdvanceDialogue();

        }

        /*private void OnChoiceSelected(Choice obj)
        {
            AdvanceFromDecision();
        }*/

        private void Update()
        {

            if (Input.GetMouseButtonDown(0) && currentActor.Value != null
                && currentActor.Value.hasStory)
            {
                if (currentActor.Value.story.canContinue)
                {
                    AdvanceDialogue();
                    if (currentActor.Value.story.currentChoices.Count != 0)
                    {
                        StartCoroutine(ShowChoices());
                    }
                }
                else
                {
                    FinishStory();
                }
            }

        }

        public void FinishStory()
        {
            currentSentence.text = string.Empty;
            storyPanel.SetActive(false);
        }

        private IEnumerator ShowChoices()
        {
            List<Choice> _choices = currentActor.Value.story.currentChoices;

            for (int i = 0; i < _choices.Count; i++)
            {
                Choice choice = currentActor.Value.story.currentChoices[i];
                Button button = UITools.CreateButton(
                    currentActor.Value.story.currentChoices[i].text.Trim(),
                    buttonPrefab, optionPanel.transform
                );

                button.onClick.AddListener(delegate
                {
                    OnClickChoiceButton(choice);
                });
            }

            optionPanel.gameObject.SetActive(true);

            yield return new WaitUntil(() =>
            {
                return choiceSelected != null;
            });

            AdvanceFromDecision();
        }

        private void AdvanceFromDecision()
        {
            ClientDirector.Instance.Populate();
            CleanOptionPanel();
            choiceSelected = null;
            if (currentActor.Value.story.canContinue)
            {
                AdvanceDialogue();
            }
        }

        public void CleanOptionPanel()
        {
            optionPanel.SetActive(false);

            for (int i = 0; i < optionPanel.transform.childCount; i++)
            {
                Destroy(optionPanel.transform.GetChild(i).gameObject);
            }
        }

        LambdaTimer timer = new LambdaTimer(1);
        public void AdvanceDialogue()
        {
            
            //string sentence = currentActor.Value.story.Continue();
            //int index = 0;
            //timer.Delay(() =>
            //{
            //    index++;
            //    currentSentence.text += sentence.Substring(index, sentence.Length);
            //});

            // StopAllCoroutines();
            // StartCoroutine(UITools.TypeText(currentActor.Value.story.Continue(), currentSentence, speed));


        }

        //IEnumerator TypeSentence(string sentence)
        //{
        //    currentSentence.text = "";
        //    foreach (char letter in sentence.ToCharArray())
        //    {
        //        currentSentence.text += letter;
        //        yield return null;
        //    }

        //    yield return null;
        //}

        private void OnClickChoiceButton(Choice choice)
        {
            choiceSelected = choice;
            currentActor.Value.story.ChooseChoiceIndex(choice.index);
        }

    }

}