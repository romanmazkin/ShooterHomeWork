using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    //Звук почему то работает только при клике мышкой через апдейт, в остальных случаях
    //если я пытаюсь воспроизвести звук через другие скрипты, то ничего не работает
    //Выскакивает ошибка в инспекторе

    private const int RightMouseButton = 1;
    private const float OffVolume = -80;
    private const float OnVolume = 0;
    private const int OnVolumeSaveKey = 1;
    private const int OffVolumeSaveKey = -1;

    private const string MainSoundKey = "MainSoundVolume";
    private const string GameSoundsKey = "GameSoundsVolume";

    [SerializeField] private AudioSource _movePointAudioSource;
    [SerializeField] private AudioSource _explosionAudioSource;
    [SerializeField] private AudioClip _movePointAudioClip;
    [SerializeField] private AudioMixerGroup _masterGroup;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        PlayMovePointSound();
    }

    public void Initialize()
    {
        int mainSoundSaveKey = PlayerPrefs.GetInt(MainSoundKey);

        if (mainSoundSaveKey == 0 || mainSoundSaveKey == OnVolumeSaveKey)
            OnMainSound();
        else
            OffMainSound();

        int gameSoundsSaveKey = PlayerPrefs.GetInt(GameSoundsKey);

        if (gameSoundsSaveKey == 0 || gameSoundsSaveKey == OnVolumeSaveKey)
            OnGameSounds();
        else
            OffGameSounds();
    }

    public bool IsMainMusicOn() => PlayerPrefs.GetInt(MainSoundKey) == OnVolumeSaveKey;

    public bool IsGameSoundsOn() => PlayerPrefs.GetInt(GameSoundsKey) == OnVolumeSaveKey;

    public void PlayExplosionSound() => _explosionAudioSource.Play();

    public void PlayMovePointSound()
    {
        if (Input.GetMouseButtonDown(RightMouseButton))
        {
            float pitch = Random.Range(0.8f, 1.0f);
            _movePointAudioSource.pitch = pitch;
            _movePointAudioSource.Play();
        }
    }

    public void OffMainSound()
    {
        _masterGroup.audioMixer.SetFloat(MainSoundKey, OffVolume);
        PlayerPrefs.SetInt(MainSoundKey, OffVolumeSaveKey);
    }

    public void OnMainSound()
    {
        _masterGroup.audioMixer.SetFloat(MainSoundKey, OnVolume);
        PlayerPrefs.SetInt(MainSoundKey, OnVolumeSaveKey);
    }

    public void OffGameSounds()
    {
        _masterGroup.audioMixer.SetFloat(GameSoundsKey, OffVolume);
        PlayerPrefs.SetInt(GameSoundsKey, OffVolumeSaveKey);
    }

    public void OnGameSounds()
    {
        _masterGroup.audioMixer.SetFloat(GameSoundsKey, OnVolume);
        PlayerPrefs.SetInt(GameSoundsKey, OnVolumeSaveKey);
    }

    public void ChangeMainSoundVolume()
    {
        _masterGroup.audioMixer.GetFloat(MainSoundKey, out float value);

        if(value == OnVolume)
            OffMainSound();
        else
            OnMainSound();
    }

    public void ChangeGameSoundsVolume()
    {
        _masterGroup.audioMixer.GetFloat(GameSoundsKey, out float value);

        if (value == OnVolume)
            OffGameSounds();
        else
            OnGameSounds();
    }
}
