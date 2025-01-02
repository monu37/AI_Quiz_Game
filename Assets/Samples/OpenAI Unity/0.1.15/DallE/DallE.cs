using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;

namespace OpenAI
{
    public class DallE : MonoBehaviour
    {

        public static DallE instance;

        //[SerializeField] private InputField inputField;
        //[SerializeField] private Button button;
        //[SerializeField] private Image image;
        [SerializeField] private GameObject loadingLabel;

        private OpenAIApi openai = new OpenAIApi();


        private void Awake()
        {
            instance = this;
        }


        public async void ShowImage(Image img, string imgname)
        {
            img.sprite = null;
            loadingLabel.SetActive(true);
            var response = await openai.CreateImage(new CreateImageRequest
            {
                Prompt = imgname,
                Size = ImageSize.Size512
            });

            if (response.Data != null && response.Data.Count > 0)
            {
                using (var request = new UnityWebRequest(response.Data[0].Url))
                {
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.SetRequestHeader("Access-Control-Allow-Origin", "*");
                    request.SendWebRequest();

                    while (!request.isDone) await Task.Yield();

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(request.downloadHandler.data);
                    var sprite = Sprite.Create(texture, new Rect(0, 0, 512, 512), Vector2.zero, 1f);
                    img.sprite = sprite;


                }
            }
            else
            {
                Debug.LogWarning("No image was created from this prompt.");
            }

            loadingLabel.SetActive(false);
            img.gameObject.SetActive(true);
        }
    }
}
