using UnityEngine;
using System.Collections;

public class SoundHelper : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioRest;

    [SerializeField]
    private AudioClip _audioMoveOn;

    [SerializeField]
    private AudioClip _audioSpawn;

    [SerializeField]
    private AudioClip _audioRun;

    [SerializeField]
    private AudioClip _audioLeftBehind;

    [SerializeField]
    private AudioClip _audioSelected;

    [SerializeField]
    private AudioClip _audioApplyToCommand;

    [SerializeField]
    private AudioClip _audioAttack_01;

    [SerializeField]
    private AudioClip _audioAttack_02;

    [SerializeField]
    private AudioClip _audioAttack_03;

    [SerializeField]
    private AudioClip _audioHurt_01;

    [SerializeField]
    private AudioClip _audioHurt_02;

    [SerializeField]
    private AudioClip _audioDie_01;

    [SerializeField]
    private AudioClip _audioDie_02;

    [SerializeField]
    private AudioClip _audioSpecialAbility;

    [SerializeField]
    private AudioClip _audioIdle_01;

    [SerializeField]
    private AudioClip _audioIdle_02;

    [SerializeField]
    private AudioSource _audioSource_1;

    [SerializeField]
    private AudioSource _audioSource_2;

    private int _audioSourceToChoose;

    private bool _dontOverlap;

    private int _chosenClip;

    // Start setup
    void Start()
    {
        _audioSourceToChoose = 1;
        _chosenClip = 0;
        _dontOverlap = false;
    }

    public void Stop()
    {
        _audioSource_1.Stop();
        _audioSource_2.Stop();
    }

    public void Play(string soundToPlay)
    {
        SetAudioSources(soundToPlay);
    }

    // Loads designated AudioSource with new clip and activates fading
    public void SetAudioSources(string soundToPlay)
    {
        if (_audioSource_1.isPlaying)
        {
            _audioSourceToChoose = 2;
        }
        if (_audioSource_2.isPlaying)
        {
            _audioSourceToChoose = 1;
        }

            AudioClip clip = null;

        switch (soundToPlay)
        {
            case "rest":
                clip = _audioRest;
                _dontOverlap = true;
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;

            case "move":
                clip = _audioMoveOn;
                _dontOverlap = true;
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;

            case "spawn":
                clip = _audioSpawn;
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;

            case "run":
                clip = _audioRun;
                _dontOverlap = true;
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;

            case "leftBehind":
                clip = _audioLeftBehind;
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;

            case "selected":
                clip = _audioSelected;
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;

            case "applyToCommand":
                clip = _audioApplyToCommand;
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;

            case "attack":

                if (_audioAttack_01 != null && _audioAttack_02 != null && _audioAttack_03 != null)
                {
                    _chosenClip = ChooseRandomClip(1, 3);
                    if (_chosenClip == 1)
                    {
                        clip = _audioAttack_01;
                    }
                    if (_chosenClip == 2)
                    {
                        clip = _audioAttack_02;
                    }
                    if (_chosenClip == 3)
                    {
                        clip = _audioAttack_03;
                    }
                    
                    _chosenClip = 0;
                }
                else
                {
                    clip = _audioAttack_01;
                }
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;

            case "hurt":
                if (_audioHurt_01 != null && _audioHurt_02 != null)
                {
                    _chosenClip = ChooseRandomClip(1, 2);
                    if (_chosenClip == 1)
                    {
                        clip = _audioHurt_01;
                    }
                    if (_chosenClip == 2)
                    {
                        clip = _audioHurt_02;
                    }

                    _chosenClip = 0;
                }
                else
                {
                    clip = _audioHurt_01;
                }
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;

            case "die":
                if (_audioDie_01 != null && _audioDie_02 != null)
                {
                    _chosenClip = ChooseRandomClip(1, 2);
                    if (_chosenClip == 1)
                    {
                        clip = _audioDie_01;
                    }
                    if (_chosenClip == 2)
                    {
                        clip = _audioDie_02;
                    }

                    _chosenClip = 0;
                }
                else
                {
                    clip = _audioDie_01;
                }
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;

            case "useSpecialAbility":
                clip = _audioSpecialAbility;
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;

            case "idle":
                if (_audioIdle_01 != null && _audioIdle_02 != null)
                {
                    _chosenClip = ChooseRandomClip(1, 2);
                    if (_chosenClip == 1)
                    {
                        clip = _audioIdle_01;
                    }
                    if (_chosenClip == 2)
                    {
                        clip = _audioIdle_02;
                    }
                 
                    _chosenClip = 0;
                }
                else
                {
                    clip = _audioIdle_01;
                }
                Debug.Log("Setting AudioSource_" + _audioSourceToChoose + " Clip to " + clip + "!");
                break;
        }

        if (_audioSourceToChoose == 1)
        {
            _audioSource_1.Stop();
            if(clip != null)
            {
                _audioSource_1.clip = clip;
                if(_dontOverlap)
                {
                    _audioSource_2.Stop();
                    _dontOverlap = false;
                }
                _audioSource_1.Play();
            }
            else
            {
                Debug.Log("Angefordertes Soundfile nicht verfügbar! (Serialisierung im Script SoundHelper prüfen)");
            }
        }

        if (_audioSourceToChoose == 2)
        {
            _audioSource_2.Stop();
            if (clip != null)
            {
                _audioSource_2.clip = clip;
                if(_dontOverlap)
                {
                    _audioSource_1.Stop();
                    _dontOverlap = false;
                }
                _audioSource_2.Play();
            }
            else
            {
                Debug.Log("Angefordertes Soundfile nicht verfügbar! (Serialisierung im Script SoundHelper prüfen)");
            }
        }
    }

    private int ChooseRandomClip(int min, int max)
    {
        int randomClip = Random.Range(min, max + 1);
        Debug.Log("Random Clip Number: " + randomClip);
        return randomClip;
    }
}
