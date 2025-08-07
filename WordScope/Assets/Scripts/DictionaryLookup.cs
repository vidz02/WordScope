using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class DictionaryLookup : MonoBehaviour
{
    [SerializeField] 
    private TMP_InputField wordInput;

    [SerializeField] 
    private Button searchButton;

    private const string BaseUrl = "https://api.dictionaryapi.dev/api/v2/entries/en/";

    private void Start()
    {
        searchButton.onClick.AddListener(OnSearchClicked);
    }

    private void OnSearchClicked()
    {
        string word = wordInput.text.Trim().ToLower();
        if (ValidationUtils.IsValidWord(word))
            StartCoroutine(FetchDefinition(word));
    }

    private IEnumerator FetchDefinition(string word)
    {
        Debug.Log("Loading...");

        using UnityWebRequest req = UnityWebRequest.Get(BaseUrl + word);
        yield return req.SendWebRequest();

        switch (req.result)
        {
            case UnityWebRequest.Result.Success:
                {
                    string definition = JsonUtils.GetFirstDefinition(req.downloadHandler.text);

                    if (string.IsNullOrEmpty(definition))
                    {
                        Debug.LogWarning("Definition not found or JSON format invalid.");
                    }
                    else
                    {
                        Debug.Log($"<b>{word.FirstCharacterToUpper()}</b>: {definition}");
                    }

                    break;
                }

            case UnityWebRequest.Result.InProgress:
                Debug.Log("Request in progress...");
                break;

            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError($"Network error: {req.error}");
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError($"Data processing error: {req.error}");
                break;

            default:
                Debug.LogError("Error: " + req.error);
                break;
        }
    }
}
