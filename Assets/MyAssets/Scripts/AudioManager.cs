using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //single sounds events references
    [FMODUnity.EventRef]
    public string ButtonPressedEvent;
    [FMODUnity.EventRef]
    public string StartLevelEvent;
    [FMODUnity.EventRef]
    public string EndLevelEvent;
    [FMODUnity.EventRef]
    public string GrabPizzaEvent;
    [FMODUnity.EventRef]
    public string StealPizzaEvent;
    [FMODUnity.EventRef]
    public string PlayerCollideEvent;
    [FMODUnity.EventRef]
    public string PlayerSlideEvent;
    [FMODUnity.EventRef]
    public string StepEvent;
    [FMODUnity.EventRef]
    public string StepWetEvent;
    [FMODUnity.EventRef]
    public string StepClothEvent;

    //music events
    [FMODUnity.EventRef]
    public string MenuMusicEvent;
    [FMODUnity.EventRef]
    public string MenuAmbienceEvent;
    [FMODUnity.EventRef]
    public string LevelMusicEvent;

    //Sound instances to manually start/stop
    FMOD.Studio.EventInstance menuMusicState;
    FMOD.Studio.EventInstance menuAmbienceState;
    FMOD.Studio.EventInstance levelMusicState;

    //level music parameter
    FMOD.Studio.ParameterInstance levelParam;
    private FMOD.ATTRIBUTES_3D Attributes;

    public static AudioManager instance;

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        //init menu music and ambience
        menuMusicState = FMODUnity.RuntimeManager.CreateInstance(MenuMusicEvent);
        menuAmbienceState = FMODUnity.RuntimeManager.CreateInstance(MenuAmbienceEvent);
        //init level music and parameter
        levelMusicState = FMODUnity.RuntimeManager.CreateInstance(LevelMusicEvent);
        levelMusicState.getParameter("Pizza_Taken", out levelParam);
        levelParam.setValue(0.0f);
        
        StartMenuMusic();
    }

    private void OnDestroy()
    {
        StopMenuMusic();
        StopLevelMusic();

        menuMusicState.release();
        menuAmbienceState.release();
        levelMusicState.release();
    }

    public void StartMenuMusic()
    {
        menuMusicState.start();
        menuAmbienceState.start();
    }

    public void StopMenuMusic()
    {
        menuMusicState.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        menuAmbienceState.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StartLevelMusic()
    {
        setLevelMusicParam(0.0f);
        levelMusicState.start();
    }

    public void setLevelMusicParam(float pizza_taken)
    {
        levelParam.setValue(pizza_taken);
    }

    public void StopLevelMusic()
    {
        levelMusicState.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void PlaySoundOnce(SoundEvent e, Vector3 position)
    {
        switch (e)
        {
            case SoundEvent.ButtonPressed:
                FMODUnity.RuntimeManager.PlayOneShot(ButtonPressedEvent, position);
                break;
            case SoundEvent.StartLevel:
                FMODUnity.RuntimeManager.PlayOneShot(StartLevelEvent, position);
                break;
            case SoundEvent.EndLevel:
                FMODUnity.RuntimeManager.PlayOneShot(EndLevelEvent, position);
                break;

            case SoundEvent.GrabPizza:
                FMODUnity.RuntimeManager.PlayOneShot(GrabPizzaEvent, position);
                break;
            case SoundEvent.StealPizza:
                FMODUnity.RuntimeManager.PlayOneShot(StealPizzaEvent, position);
                break;
            case SoundEvent.PlayerCollide:
                FMODUnity.RuntimeManager.PlayOneShot(PlayerCollideEvent, position);
                break;
            case SoundEvent.PlayerSlide:
                FMODUnity.RuntimeManager.PlayOneShot(PlayerSlideEvent, position);
                break;
            case SoundEvent.Step:
                FMODUnity.RuntimeManager.PlayOneShot(StepEvent, position);
                break;
            case SoundEvent.StepWet:
                FMODUnity.RuntimeManager.PlayOneShot(StepWetEvent, position);
                break;
            case SoundEvent.StepCloth:
                FMODUnity.RuntimeManager.PlayOneShot(StepClothEvent, position);
                break;
        }
    }
}

public enum SoundEvent
{
    ButtonPressed,
    StartLevel,
    EndLevel,
    GrabPizza,
    StealPizza,
    PlayerCollide,
    PlayerSlide,
    Step,
    StepWet,
    StepCloth
}