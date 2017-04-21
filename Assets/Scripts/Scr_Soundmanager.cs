using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Scr_Soundmanager : MonoBehaviour {

    // Standart Parameter:
    // Diese werden verwndet wenn nichts anderes angegeben wurde
    public AudioMixerGroup DefaultMixerGroup;
    public float DefaultVolumeMax = 1f;
    public float DefaultVolumeMin = 1f;
    public float DefaultPitchMax = 1f;
    public float DefaultPitchMin = 1f;
    public float DefaultSpread = 90f;
    public float Default3D = 1f;
    public float DefaultRange = 500f;

    // Target: GameObject auf dem der Sound abgespielt werden soll
    // Clip: Der Audioclip der gespielt werden soll
    GameObject Target;
    AudioClip Clip;

    // Temporäre Parameter:
    // Diese werden mit den jeweilig angegebenen Werten ersetzt
    AudioMixerGroup _mixerGroup;
    float _volumeMax;
    float _volumeMin;
    float _pitchMax;
    float _pitchMin;
    float _spread;
    float _3D;
    float _range;

    // Listen von Loops die ein oder ausgeblendet werden sollen und deren Blend-Geschwindigkeiten
    List<AudioSource> FadeInLoops = new List<AudioSource>();
    List<Vector2> FadeInTimes = new List<Vector2>();
    List<AudioSource> FadeOutLoops = new List<AudioSource>();
    List<Vector2> FadeOutTimes = new List<Vector2>();

    // Eine statische Referenz auf sich selbst, die es einfacher macht den Soundmanager zu finden
    public static Scr_Soundmanager Sound;

    void Awake() {
        if (Sound == null) {
            Sound = this;
        }
        else {
            Debug.Log("Problem: There's more than one SoundManager in the Scene.");
        }
    }

    // Erstellt auf dem Ziel eine AudioSource mit den angegebenen Parametern
    void CreateAudioSource() {
        AudioSource source = Target.AddComponent<AudioSource>();
        source.loop = false;
        source.clip = Clip;
        if (_mixerGroup != null) {
            source.outputAudioMixerGroup = _mixerGroup;
        }
        source.volume = Mathf.Clamp01(Random.Range(_volumeMin, _volumeMax));
        source.pitch = Mathf.Clamp01(Random.Range(_pitchMin, _pitchMax));
        source.spread = _spread;
        source.spatialBlend = _3D;
        source.maxDistance = _range;

        source.Play();

        // Zerstört die Audiosource nachdem sie abgespielt wurde
        StartCoroutine(AutodestroySource(Clip.length / source.pitch + 0.1f, source));
    }

    IEnumerator AutodestroySource(float timing, AudioSource source) {
        yield return new WaitForSeconds(timing);
        Destroy(source);
    }

    // Mit Play( ... ) können andere Objekte einen Sound abspielen lassen
    // Die verschiedenen Varianten geben die Freiheit mehr oder weniger genaue Angaben zu machen. Was nicht angegeben wird, wird mit den Standart-Parametern ersetzt.

    public void Play(AudioClip clip, GameObject target) {
        Clip = clip;
        Target = target;
        _mixerGroup = DefaultMixerGroup;
        _volumeMax = DefaultVolumeMax;
        _volumeMin = DefaultVolumeMin;
        _pitchMax = DefaultPitchMax;
        _pitchMin = DefaultPitchMin;
        _spread = DefaultSpread;
        _3D = Default3D;
        _range = DefaultRange;

        CreateAudioSource();
    }

    public void Play(AudioClip clip, GameObject target, float volumeMin, float volumeMax) {
        Clip = clip;
        Target = target;
        _mixerGroup = DefaultMixerGroup;
        _volumeMax = volumeMax;
        _volumeMin = volumeMin;
        _pitchMax = DefaultPitchMax;
        _pitchMin = DefaultPitchMin;
        _spread = DefaultSpread;
        _3D = Default3D;
        _range = DefaultRange;

        CreateAudioSource();
    }

    public void Play(AudioClip clip, GameObject target, float volumeMin, float volumeMax, float pitchMin, float pitchMax) {
        Clip = clip;
        Target = target;
        _mixerGroup = DefaultMixerGroup;
        _volumeMax = volumeMax;
        _volumeMin = volumeMin;
        _pitchMax = pitchMax;
        _pitchMin = pitchMin;
        _spread = DefaultSpread;
        _3D = Default3D;
        _range = DefaultRange;

        CreateAudioSource();
    }

    public void Play(AudioClip clip, GameObject target, float volumeMin, float volumeMax, float pitchMin, float pitchMax, float range) {
        Clip = clip;
        Target = target;
        _mixerGroup = DefaultMixerGroup;
        _volumeMax = volumeMax;
        _volumeMin = volumeMin;
        _pitchMax = pitchMax;
        _pitchMin = pitchMin;
        _spread = DefaultSpread;
        _3D = Default3D;
        _range = range;

        CreateAudioSource();
    }

    public void Play(AudioClip clip, GameObject target, float volumeMin, float volumeMax, float pitchMin, float pitchMax, float range, AudioMixerGroup mixerGroup) {
        Clip = clip;
        Target = target;
        _mixerGroup = mixerGroup;
        _volumeMax = volumeMax;
        _volumeMin = volumeMin;
        _pitchMax = pitchMax;
        _pitchMin = pitchMin;
        _spread = DefaultSpread;
        _3D = Default3D;
        _range = range;

        CreateAudioSource();
    }

    public void Play(AudioClip clip, GameObject target, float volumeMin, float volumeMax, float pitchMin, float pitchMax, float range, float spacialBlend) {
        Clip = clip;
        Target = target;
        _mixerGroup = DefaultMixerGroup;
        _volumeMax = volumeMax;
        _volumeMin = volumeMin;
        _pitchMax = pitchMax;
        _pitchMin = pitchMin;
        _spread = DefaultSpread;
        _3D = spacialBlend;
        _range = range;

        CreateAudioSource();
    }

    public void Play(AudioClip clip, GameObject target, float volumeMin, float volumeMax, float pitchMin, float pitchMax, float range, float spacialBlend, AudioMixerGroup mixerGroup) {
        Clip = clip;
        Target = target;
        _mixerGroup = mixerGroup;
        _volumeMax = volumeMax;
        _volumeMin = volumeMin;
        _pitchMax = pitchMax;
        _pitchMin = pitchMin;
        _spread = DefaultSpread;
        _3D = spacialBlend;
        _range = range;

        CreateAudioSource();
    }

    public void Play(AudioClip clip, GameObject target, float volumeMin, float volumeMax, float pitchMin, float pitchMax, float range, float spacialBlend, float spread) {
        Clip = clip;
        Target = target;
        _mixerGroup = DefaultMixerGroup;
        _volumeMax = volumeMax;
        _volumeMin = volumeMin;
        _pitchMax = pitchMax;
        _pitchMin = pitchMin;
        _spread = spread;
        _3D = spacialBlend;
        _range = range;

        CreateAudioSource();
    }

    public void Play(AudioClip clip, GameObject target, float volumeMin, float volumeMax, float pitchMin, float pitchMax, float range, float spacialBlend, float spread, AudioMixerGroup mixerGroup) {
        Clip = clip;
        Target = target;
        _mixerGroup = mixerGroup;
        _volumeMax = volumeMax;
        _volumeMin = volumeMin;
        _pitchMax = pitchMax;
        _pitchMin = pitchMin;
        _spread = spread;
        _3D = spacialBlend;
        _range = range;

        CreateAudioSource();
    }



    // LOOPS

    void CreateAudioLoop(float time, float volume) {
        AudioSource source = Target.AddComponent<AudioSource>();
        source.loop = true;
        source.clip = Clip;
        if (_mixerGroup != null) {
            source.outputAudioMixerGroup = _mixerGroup;
        }
        source.volume = 0;
        source.pitch = _pitchMax;
        source.spread = _spread;
        source.spatialBlend = _3D;
        source.maxDistance = _range;

        FadeInLoops.Add(source);
        FadeInTimes.Add(new Vector2(time, volume));

        source.Play();
    }

    void Update() {
        // Fade In Loops
        for (int i = 0; i < FadeInLoops.Count; i++) {
            AudioSource source = FadeInLoops[i];
            source.volume += (FadeInTimes[i].y / FadeInTimes[i].x) * Time.deltaTime;
            if (source.volume >= FadeInTimes[i].y) {
                source.volume = FadeInTimes[i].y;
                FadeInLoops.Remove(source);
                FadeInTimes.Remove(FadeInTimes[i]);
            }
        }

        // Fade Out Loops
        for (int i = 0; i < FadeOutLoops.Count; i++) {
            AudioSource source = FadeOutLoops[i];
            source.volume -= (FadeOutTimes[i].y / FadeOutTimes[i].x) * Time.deltaTime;
            if (source.volume <= 0) {
                FadeOutLoops.Remove(source);
                FadeOutTimes.Remove(FadeOutTimes[i]);
            }
        }
    }

    public void PlayLoop(AudioClip clip, GameObject target, float LoopTime) {
        Clip = clip;
        Target = target;
        _mixerGroup = DefaultMixerGroup;
        _volumeMax = DefaultVolumeMax;
        _pitchMax = DefaultPitchMax;
        _spread = DefaultSpread;
        _3D = Default3D;
        _range = DefaultRange;

        CreateAudioLoop(LoopTime, _volumeMax);
    }

    public void PlayLoop(AudioClip clip, GameObject target, float LoopTime, float volume) {
        Clip = clip;
        Target = target;
        _mixerGroup = DefaultMixerGroup;
        _volumeMax = volume;
        _pitchMax = DefaultPitchMax;
        _spread = DefaultSpread;
        _3D = Default3D;
        _range = DefaultRange;

        CreateAudioLoop(LoopTime, _volumeMax);
    }

    public void PlayLoop(AudioClip clip, GameObject target, float LoopTime, float volume, float range) {
        Clip = clip;
        Target = target;
        _mixerGroup = DefaultMixerGroup;
        _volumeMax = volume;
        _pitchMax = DefaultPitchMax;
        _spread = DefaultSpread;
        _3D = Default3D;
        _range = range;

        CreateAudioLoop(LoopTime, _volumeMax);
    }

    public void PlayLoop(AudioClip clip, GameObject target, float LoopTime, float volume, float range, AudioMixerGroup mixerGroup) {
        Clip = clip;
        Target = target;
        _mixerGroup = mixerGroup;
        _volumeMax = volume;
        _pitchMax = DefaultPitchMax;
        _spread = DefaultSpread;
        _3D = Default3D;
        _range = range;

        CreateAudioLoop(LoopTime, _volumeMax);
    }

    public void PlayLoop(AudioClip clip, GameObject target, float LoopTime, float volume, float range, float spacialBlend) {
        Clip = clip;
        Target = target;
        _mixerGroup = DefaultMixerGroup;
        _volumeMax = volume;
        _pitchMax = DefaultPitchMax;
        _spread = DefaultSpread;
        _3D = spacialBlend;
        _range = range;

        CreateAudioLoop(LoopTime, _volumeMax);
    }

    public void PlayLoop(AudioClip clip, GameObject target, float LoopTime, float volume, float range, float spacialBlend, AudioMixerGroup mixerGroup) {
        Clip = clip;
        Target = target;
        _mixerGroup = mixerGroup;
        _volumeMax = volume;
        _pitchMax = DefaultPitchMax;
        _spread = DefaultSpread;
        _3D = spacialBlend;
        _range = range;

        CreateAudioLoop(LoopTime, _volumeMax);
    }

    public void PlayLoop(AudioClip clip, GameObject target, float LoopTime, float volume, float range, float spacialBlend, float spread) {
        Clip = clip;
        Target = target;
        _mixerGroup = DefaultMixerGroup;
        _volumeMax = volume;
        _pitchMax = DefaultPitchMax;
        _spread = spread;
        _3D = spacialBlend;
        _range = range;

        CreateAudioLoop(LoopTime, _volumeMax);
    }

    public void PlayLoop(AudioClip clip, GameObject target, float LoopTime, float volume, float range, float spacialBlend, float spread, AudioMixerGroup mixerGroup) {
        Clip = clip;
        Target = target;
        _mixerGroup = mixerGroup;
        _volumeMax = volume;
        _pitchMax = DefaultPitchMax;
        _spread = spread;
        _3D = spacialBlend;
        _range = range;

        CreateAudioLoop(LoopTime, _volumeMax);
    }



    public void StopLoop (GameObject target, float LoopTime) {
        AudioSource source = FindLoop(target);
        FadeOutLoops.Add(source);
        FadeOutTimes.Add(new Vector2(LoopTime, source.volume));
    }

    AudioSource FindLoop(GameObject target) {
        foreach (AudioSource source in target.GetComponents<AudioSource>()) {
            if (source.loop) {
                return source;
            }
        }
    Debug.Log("No Loop to end on: " + target.name);
    return null;
    }
}
