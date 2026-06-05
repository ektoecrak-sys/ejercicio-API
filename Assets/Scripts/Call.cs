using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class Call : MonoBehaviour
{
    public ApiData data;
    public string url = "https://gamerpower.com/api/giveaways";
    public RawImage portada;
    public TextMeshProUGUI  title; 
    public TextMeshProUGUI  description;
    public TextMeshProUGUI  publish;
    public TextMeshProUGUI  plataforma;
    private ApiDataWrapper wrapper;
    private ApiData currentGame;
    void Start()
    {
        StartCoroutine(GetGame());
    }
IEnumerator GetGame()
{
    UnityWebRequest request = UnityWebRequest.Get(url);

    yield return request.SendWebRequest();

    if (request.result == UnityWebRequest.Result.Success)
    {
        string json = request.downloadHandler.text;

        json = "{\"items\":" + json + "}";

        wrapper = JsonUtility.FromJson<ApiDataWrapper>(json);

        if (wrapper != null && wrapper.items.Length > 0)
        {
            ShowRandomGame(); 
        }
    }
    else
    {
        Debug.LogError("Error en la petición: " + request.error);
    }
}
   IEnumerator DownloadAndShowTexture(string url)
{
    if (string.IsNullOrEmpty(url))
    {
        Debug.LogError("URL de imagen inválida");
        yield break;
    }

    url = url.Trim();

    using (UnityWebRequest req = UnityWebRequestTexture.GetTexture(url))
    {
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error descargando imagen: " + req.error);
            yield break;
        }
        Texture2D tex = DownloadHandlerTexture.GetContent(req);
        portada.texture = tex;
    }
}
void ShowRandomGame()
{
    if (wrapper == null || wrapper.items.Length == 0)
        return;

    int index = Random.Range(0, wrapper.items.Length);
    currentGame = wrapper.items[index];

    // Actualizar la interfaz con los datos del juego seleccionado

    title.text = currentGame.title;
    description.text = currentGame.description;
    publish.text = currentGame.published_date;
    plataforma.text = currentGame.platforms;

    string url = currentGame.thumbnail.Replace("\\/", "/").Trim();// Asegurarse de que la URL no tenga espacios en blanco
    StartCoroutine(DownloadAndShowTexture(url));// Descargar y mostrar la imagen de portada
}
public void NextGame()
    {
        ShowRandomGame();
    }
}