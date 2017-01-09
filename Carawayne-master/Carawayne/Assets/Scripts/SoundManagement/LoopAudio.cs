using UnityEngine;
using System.Collections;

public class LoopAudio : MonoBehaviour
{
    [SerializeField]
    private string _clipSwitcher;

    [SerializeField]
    private AudioClip _audioTitle;

    [SerializeField]
    private AudioClip _audioGameplay;

    [SerializeField]
    private AudioClip _audioCombat;

    [SerializeField]
    private AudioClip _jingleWonGame;

    [SerializeField]
    private AudioClip _jingleLostGame;

    [SerializeField]
    private AudioSource _audioSource_1;

    [SerializeField]
    private AudioSource _audioSource_2;

    [SerializeField]
    private AudioClip _ambientDesert;

    [SerializeField]
    private AudioClip _ambientOasis;

    [SerializeField]
    private AudioClip _ambientForest;

    [SerializeField]
    private AudioClip _ambientMountain;

    [SerializeField]
    private AudioClip _ambientSandstorm;

    [SerializeField]
    private AudioSource _audioSourceAmbient;

    private float _playbackTime_1;
    private float _playbackTime_2;

    private int _audioSourceToChoose;
    private bool _fadeAudio;
    private string _clipSwitcherLastFrame;

    // Start setup
    void Start()
    {
        _fadeAudio = false;
        _audioSourceToChoose = 1;
        _clipSwitcher = "";

        _clipSwitcherLastFrame = "";
        _playbackTime_1 = 0f;
        _playbackTime_2 = 0f;
    }

    // Update once per frame
    void Update()
    {
        if(_audioSource_1.isPlaying)
        {
            _playbackTime_1 = _audioSource_1.time;
        }
        if(_audioSource_2.isPlaying)
        {
            _playbackTime_2 = _audioSource_2.time;
        }

        if (_clipSwitcherLastFrame != _clipSwitcher)
        {
            StartCoroutine(WaitForBeatToFinish(_clipSwitcher));
        }
        _clipSwitcherLastFrame = _clipSwitcher;

        if(_fadeAudio)
        {
            FadeVolume();
        }
    }

    public void PlayLoops(string musicClip)
    {
        WaitForBeatToFinish(musicClip);
    }
    public void PlayAmbients(string terrainType)
    {
        SetAudioSourceAmbient(terrainType);
    }

    private void PlayJingles(bool won)
    {
        _audioSource_1.Stop();
        _audioSource_2.Stop();
        _audioSourceAmbient.Stop();
        _audioSourceAmbient.loop = false;
        if (won)
        {
            _audioSourceAmbient.clip = _jingleWonGame;
        }
        if (!won)
        {
            _audioSourceAmbient.clip = _jingleLostGame;
        }

    }

    // Unity coroutine that holds execution until the current beat has played to end
    IEnumerator WaitForBeatToFinish(string musicClip)
    {
        if (_audioSource_1.isPlaying)
        {
            _audioSourceToChoose = 2;
            Debug.Log("AudioSource_1 is waiting for beat to finish...");
            yield return new WaitUntil(() => _playbackTime_1 % 2f <= 0.05f);
        }
        if (_audioSource_2.isPlaying)
        {
            _audioSourceToChoose = 1;
            Debug.Log("AudioSource_2 is waiting for beat to finish...");
            yield return new WaitUntil(() => _playbackTime_2 % 2f <= 0.05f);
        }
        Debug.Log("Beat has finished!");

        switch (musicClip)
        {
            case "title":
                SetAudioSource(_audioTitle);
                break;

            case "gameplay":
                SetAudioSource(_audioGameplay);
                break;

            case "combat":
                SetAudioSource(_audioCombat);
                break;
        }
    }

    // Loads designated AudioSource with new clip and activates fading
    private void SetAudioSource(AudioClip clip)
    {
        Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");

        if (_audioSourceToChoose == 1)
        {
            _audioSource_1.Stop();
            _audioSource_1.volume = 0f;
            _audioSource_1.clip = clip;
            _audioSource_1.Play();
            _fadeAudio = true;
            Debug.Log("Fading volumes!");
        }

        if (_audioSourceToChoose == 2)
        {
            _audioSource_2.Stop();
            _audioSource_2.volume = 0f;
            _audioSource_2.clip = clip;
            _audioSource_2.Play();
            _fadeAudio = true;
            Debug.Log("Fading volumes!");
        }
    }

    private void SetAudioSourceAmbient(string terrainType)
    {
        AudioClip clip = null;
        switch(terrainType)
        {
            case "desert":
                clip = _ambientDesert;
                break;
            case "oasis":
                clip = _ambientOasis;
                break;
            case "forest":
                clip = _ambientForest;
                break;
            case "mountain":
                clip = _ambientMountain;
                break;
            case "sandstorm":
                clip = _ambientSandstorm;
                break;
        }
        _audioSourceAmbient.loop = true;
        Debug.Log("Setting AudioSourceAmbient Clip to " + terrainType + "!");
        _audioSourceAmbient.Stop();
        _audioSourceAmbient.clip = clip;
        _audioSourceAmbient.Play();
    }

    // Crossfades the volumes of both AudioSources
    private void FadeVolume()
    {
        if (_audioSourceToChoose == 1)
        {
            _audioSource_1.volume += 0.02f;
            _audioSource_2.volume -= 0.02f;

            if (_audioSource_2.volume == 0f && _audioSource_1.volume == 1f)
            {
                _audioSource_2.Stop();
                _fadeAudio = false;
                Debug.Log("Done fading!");
            }
        }

        if (_audioSourceToChoose == 2)
        {
            _audioSource_2.volume += 0.02f;
            _audioSource_1.volume -= 0.02f;

            if (_audioSource_1.volume == 0f && _audioSource_2.volume == 1f)
            {
                _audioSource_1.Stop();
                _fadeAudio = false;
                Debug.Log("Done fading!");
            }
        }
    }
}
