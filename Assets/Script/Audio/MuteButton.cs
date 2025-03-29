using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Sprite[] iconSprite = new Sprite[2];

    private bool isMuteMusic = false;
    private AudioService audioService;

    private Button _myButton;
    private Image myImage;


    private void Start()
    {
        audioService = ServiceLocator.Get<AudioService>();

        _myButton = GetComponent<Button>();
        _myButton.onClick.AddListener(OnButtonClicked);

        myImage = GetComponent<Image>();

        isMuteMusic = audioService.GetIsMuteMusic();
        UpdateMuteSprite();
    }

    private void OnButtonClicked()
    {
        if (isMuteMusic)
        {
            audioService.MuteAudio(false);
            isMuteMusic = false;
        }
        else
        {
            audioService.MuteAudio(true);
            isMuteMusic = true;
        }

        UpdateMuteSprite();
    }

    private void UpdateMuteSprite()
    {
        if (isMuteMusic)
            myImage.sprite = iconSprite[1];
        else
            myImage.sprite = iconSprite[0];
    }
}
