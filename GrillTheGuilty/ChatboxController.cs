using UnityEngine;
using UnityEngine.UI;

public class ChatBoxController : MonoBehaviour
{
    public InputField inputField;
    public Text chatHistoryText;

    private string aiName = "AI"; // Can be changed to AI name

    void Start()
    {
        inputField.onEndEdit.AddListener(delegate { SubmitQuestion(); });
    }

    public void SubmitQuestion()
    {
        string playerQuestion = inputField.text;

        if (!string.IsNullOrEmpty(playerQuestion))
        {
            
            AddMessageToHistory("Player: " + playerQuestion);

            inputField.text = "";
        }
    }

    void AddMessageToHistory(string message)
    {
        chatHistoryText.text += message + "\n";
    }
}
