using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace Chars.Tools
{
    public static class UITools
    {
        public static IEnumerator TypeText(string textToType, Text textLabel, float speed)
        {
            float t = 0;
            int i = 0;
            while (i < textToType.Length)
            {
                t += Time.deltaTime * speed;
                i = Mathf.FloorToInt(t);
                i = Mathf.Clamp(i, 0, textToType.Length);
                textLabel.text = textToType.Substring(0, i);
                yield return null;
            }
            textLabel.text = textToType;
        }
        
        public static async Task TypeTextTask(string textToType, Text textLabel, float speed)
        {
            float t = 0;
            int i = 0;
            do
            {
                t += Time.deltaTime * speed;
                i = Mathf.FloorToInt(t);
                i = Mathf.Clamp(i, 0, textToType.Length);
                textLabel.text = textToType.Substring(0, i);
                await Task.Yield();
            } while (i < textToType.Length);
        }

        public static Text CreateText(string text, Transform canvas, Text textPrefab)
        {
            Text storyText = Object.Instantiate(textPrefab);
            storyText.text = text;
            storyText.transform.SetParent(canvas, false);
            return storyText;
        }

        public static Button CreateButton(string text, Button buttonPrefab, Transform canvas)
        {
            Button choice = Object.Instantiate(buttonPrefab, canvas);
            choice.GetComponentInChildren<Text>().text = text;
            choice.GetComponent<HorizontalLayoutGroup>().childForceExpandHeight = false;

            return choice;
        }

        
    }
}
