using System;
using System.Collections;
using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Managers
{
    [AddComponentMenu("Manager/AudioManager")]
    [ExecuteInEditMode]
    public class AudioManager : MonoBehaviour
    {
        public int NumAudioSources;
        [SerializeField]
        private List<AudioClip> _clips;
        [SerializeField]
        private List<MultiCue> _cues;
        [SerializeField]
        private List<LoopingCue> _loops;
        [SerializeField]
        private List<LevelLoopingCue> _levelLoops;

        private Dictionary<string, AudioClip> _oneShotList;
        private Dictionary<CueName, MultiCue> _cueDict;
        private Dictionary<LoopName, LoopingCue> _loopDict;
        private Dictionary<LoopName, LevelLoopingCue> _levelLoopDict; 

        private List<AudioSource> _sources;
        GameObject _sourceHolder;

        Dictionary<string, int> _queuedClipDict;
        List<string> _keys;

        private bool muted = false;
        int _nextSourceIndex;

        private static AudioManager _instance;
        public static AudioManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<AudioManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        void Awake()
        {
            UpdateManager();
        }
        
        protected void FixedUpdate()
        {
            if (muted)
            {
                foreach (var key in _keys)
                {
                    _queuedClipDict[key] = 0;
                }
                return;
            }

            if(_loops != null)
                foreach (var loop in _loops)
                    loop.Update();

            if (_queuedClipDict == null)
                return;

            foreach (var key in _keys)
            {
                if (!_queuedClipDict.ContainsKey(key))
                    continue;
                if (_queuedClipDict[key] <= 0) 
                    continue;
                AudioSource s = getNextAvailableSource();
                if (s == null)
                {
                    Debug.Log("Cannot find an AudioSource for " + name);
                    return;
                }
                s.volume = 1.0f;
                s.clip = _oneShotList[key];
                s.Play();
                _queuedClipDict[key] = 0;
            }
        }

        public void Mute()
        {
            muted = true;
        }

        public void UnMute()
        {
            muted = false;
        }

        public void UpdateManager()
        {
            DeleteClips();
            _nextSourceIndex = 0;

            foreach (var clip in Resources.LoadAll<AudioClip>("Arts/Music"))
                _clips.Add(clip);

            foreach (AudioClip clip in _clips)
            {
                _oneShotList[clip.name] = clip;
                _queuedClipDict.Add(clip.name, 0);
            }

            _sourceHolder = new GameObject("SourceHolder");
            _sourceHolder.transform.parent = gameObject.transform;
            AudioConstants.CreateCustomCues();

            foreach (MultiCue cue in _cues)
                _cueDict[cue.CueName] = cue;


            for (int i = 0; i < NumAudioSources; i++)
                _sources.Add(_sourceHolder.AddComponent<AudioSource>());

            _keys = new List<string>(_queuedClipDict.Keys);
                        
        }
        public void DeleteClips()
        {
            _sources = new List<AudioSource>();
            _clips = new List<AudioClip>();
            _cues = new List<MultiCue>();
            _loops = new List<LoopingCue>();
            _levelLoops = new List<LevelLoopingCue>();
            _oneShotList = new Dictionary<string, AudioClip>();
            _cueDict = new Dictionary<CueName, MultiCue>();
            _loopDict = new Dictionary<LoopName, LoopingCue>();
            _levelLoopDict = new Dictionary<LoopName, LevelLoopingCue>();
            _queuedClipDict = new Dictionary<string, int>();
            foreach(AudioSource source in GetComponents<AudioSource>())
                DestroyImmediate(source);
            DestroyImmediate(GameObject.Find("SourceHolder"));
        }
        //////////////////////////////////////////////
        // SINGLE AUDIOCLIPS
        //////////////////////////////////////////////
        public bool PlayClip(ClipName name, GameObject sourceObject = null, float volume = 1.0f)
        {
            if (muted)
                return false;
            string clipName = AudioConstants.GetClipName(name);
            if (!_oneShotList.ContainsKey(clipName))
            {
                Debug.Log("Cannot find the Clip >>" + name + "<<\n");
                return false;
            }
            _queuedClipDict[clipName]++;
            return true;
        }
        public bool PlayClipImmediate(ClipName name, GameObject sourceObject = null, float volume = 1.0f)
        {
            if (muted)
                return false;
            string clipName = AudioConstants.GetClipName(name);
            if (!_oneShotList.ContainsKey(clipName))
            {
                Debug.Log("Cannot find the Clip >>" + name + "<<\n");
                return false;
            }
            AudioSource.PlayClipAtPoint(findClip(name),transform.position);
            return true;
        }
        //////////////////////////////////////////////
        // SINGLE AUDIOCLIPS
        //////////////////////////////////////////////
        public bool PlayClip(ClipName name, AudioSource s)
        {
            if (muted)
                return false;
            string clipName = AudioConstants.GetClipName(name);
            if (!_oneShotList.ContainsKey(clipName))
            {
                Debug.Log("Cannot find the Clip >>" + name + "<<\n");
                return false;
            }
            s.clip = findClip(name);
            s.volume = 1.0f;
            s.Play();
            return true;
        }

        public bool PlayClipDelayed(ClipName name, float delayTime, GameObject sourceObject = null, float volume = 1.0f)
        {
            if (muted)
                return false;
            string clipName = AudioConstants.GetClipName(name);
            if (!_oneShotList.ContainsKey(clipName))
                return false;
            GameObject obj = sourceObject ? sourceObject.gameObject : gameObject;
            AudioSource s = obj.AddComponent<AudioSource>();
            s.clip = findClip(name);
            s.loop = false;
            s.volume = volume;
            s.PlayDelayed(delayTime);
            StartCoroutine(this.TimedDespawn(s,delayTime+s.clip.length));
            return true;
        }
        //////////////////////////////////////////////
        // MULTI CUES
        //////////////////////////////////////////////
        public bool PlayCue(CueName name, GameObject sourceObject = null, float volume = 1.0f)
        {
            if (muted)
                return false;
            if(_cueDict.ContainsKey(name))
                _cueDict[name].Play();
            return true;
        }
        public bool PlayCue(CueName name, AudioSource source)
        {
            if (muted)
                return false;
            if (_cueDict.ContainsKey(name))
                _cueDict[name].Play(source);
            return true;
        }
        //////////////////////////////////////////////
        // LOOPING CUES
        //////////////////////////////////////////////
        public bool PlayLoop(LoopName name, float volume = 1.0f)
        {
            if (muted)
                return false;
            if (!_loopDict.ContainsKey(name))
                return false;
            if (_loopDict[name].running)
                return false;
            _loopDict[name].volume = volume;
            _loopDict[name].Play();
            return true;
        }
        public bool SwapLoopTrack(LoopName name)
        {
            if (muted)
                return false;
            if (!_loopDict.ContainsKey(name)) 
                return false;
            _loopDict[name].SwitchTrack();
            return true;
        }
        public bool StopLoop(LoopName name)
        {
            if (!_loopDict.ContainsKey(name))
                return false;
            _loopDict[name].Stop();
            return true;
        }
        public bool PlayLevelLoop(LoopName name, float volume = 1.0f)
        {
            if (muted)
                return false;
            if (!_levelLoopDict.ContainsKey(name))
                return false;
            if (_levelLoopDict[name].running)
                return false;
            _levelLoopDict[name].volume = volume;
            _levelLoopDict[name].Play();
            return true;
        }
        public bool StopLevelLoop(LoopName name)
        {
            if (!_levelLoopDict.ContainsKey(name))
                return false;
            _levelLoopDict[name].Stop();
            return true;
        }
        //////////////////////////////////////////////
        // Creation Methods
        //////////////////////////////////////////////
        public bool CreateMultiCueRandom(CueName name, List<ClipName> clipList)
        {
            List<ProportionValue<ClipName>> list = new List<ProportionValue<ClipName>>();
            foreach( var clip in clipList)
                list.Add(ProportionValue.Create(1.0f / clipList.Count, clip));
            _cues.Add(new MultiCue(name, list));
            return true;
        }
        public bool CreateMultiCueParallel(CueName name, List<ClipName> clipList)
        {
            _cues.Add(new MultiCue(name, clipList));
            return true;
        }
        public bool CreateLoop(LoopName loopName, List<ClipName> clipList, float volume = 1.0f)
        {
            if (_loopDict.ContainsKey(loopName))
                return false;
            if (_instance == null) 
                return true;

            LoopingCue cue = new LoopingCue(loopName)
            {
                clips = clipList,
                volume = volume,
                name = AudioConstants.GetLoopName(loopName)
            };
            _loopDict[loopName] = cue;
            _loops.Add(cue);
            return true;
        }
        public bool CreateLevelLoop(LoopName levelLoopName, List<ClipName> clipList, float volume = 1.0f)
        {
            if (_levelLoopDict.ContainsKey(levelLoopName))
                return false;

            if (_instance != null)
            {
                LevelLoopingCue cue = new LevelLoopingCue(levelLoopName,clipList);
                cue.volume = volume;
                cue.name = AudioConstants.GetLoopName(levelLoopName);
                _levelLoopDict[levelLoopName] = cue;
                _levelLoops.Add(cue);
            }
            return true;
        }

        public void SetMediumIntensity(LoopName levelLoopName)
        {
            if (!_levelLoopDict.ContainsKey(levelLoopName))
                return;

            _levelLoopDict[levelLoopName].EnableMedium();
        }
        public void SetHeavyIntensity(LoopName levelLoopName)
        {
            if (!_levelLoopDict.ContainsKey(levelLoopName))
                return;

            _levelLoopDict[levelLoopName].EnableHeavy();
        }

        public void SetLightIntensity(LoopName levelLoopName)
        {
            if (!_levelLoopDict.ContainsKey(levelLoopName))
                return;
            _levelLoopDict[levelLoopName].EnableLight();
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HELPER FUNCTIONS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public AudioClip findClip(ClipName clipToFind)
        {
            string clipName = AudioConstants.GetClipName(clipToFind);
            return _oneShotList.ContainsKey(clipName) ? _oneShotList[clipName] : null;
        }

        public MultiCue findCue(CueName name)
        {
            return _cueDict.ContainsKey(name) ? _cueDict[name] : null;
        }

        private IEnumerator TimedDespawn(AudioSource s, float time)
        {
            yield return new WaitForSeconds(time);
            DestroyImmediate(s);
        }
        private AudioSource getNextAvailableSource()
        {
            if (_nextSourceIndex >= NumAudioSources)
                _nextSourceIndex = 0;
            if (_sources[_nextSourceIndex].isPlaying)
                _sources[_nextSourceIndex].Stop();
            return _sources[_nextSourceIndex++];
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HELPER CLASSES
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public class LoopingCue
        {
            [HideInInspector]
            public string name;
            public LoopName loopName;
            public bool running = false;
            public int _curTrack;
            [Range(0.0f, 1.0f)]
            public float volume = 1.0f;
            public List<ClipName> clips;
            List<AudioSource> audioSources;
            private double nextEventTime;

            public LoopingCue(LoopName name)
            {
                this.name = AudioConstants.GetLoopName(name);
                audioSources = new List<AudioSource>();
            }
            public void Play()
            {
                for (int i = 0; i < clips.Count; i++)
                {
                    audioSources[i] = Instance.gameObject.AddComponent("AudioSource") as AudioSource;
                    audioSources[i].playOnAwake = false;
                }
                nextEventTime = AudioSettings.dspTime;
                running = true;
            }
            public void Stop()
            {
                running = false;
                foreach (var source in audioSources)
                    source.Stop();
            }
            public void SwitchTrack()
            {
                if (_curTrack == clips.Count - 1)
                    _curTrack = 0;
                else
                    _curTrack++;
            }
            public void Update()
            {
                if (!running)
                    return;

                // Perfectly In Sync with Unity Audio System
                double time = AudioSettings.dspTime;

                if (time > nextEventTime)
                {
                    audioSources[_curTrack].clip = Instance.findClip(clips[_curTrack]);
                    audioSources[_curTrack].volume = volume;
                    audioSources[_curTrack].Play();
                    nextEventTime += audioSources[_curTrack].clip.length;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HELPER CLASSES
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public class LevelLoopingCue
        {
            [HideInInspector]
            public string name;
            public LoopName loopName;
            public bool running = false;
            public int _curTrack;

            [Range(0.0f, 1.0f)]
            public float volume = 1.0f;

            private List<AudioSource> sources; 

            private AudioSource lightSource;
            public ClipName lightClip;
            private AudioSource mediumSource;
            public ClipName mediumClip;
            private AudioSource heavySource;
            public ClipName heavyClip;

            private double nextEventTime;

            public LevelLoopingCue(LoopName name, List<ClipName> clips)
            {
                this.name = AudioConstants.GetLoopName(name);
                lightClip = clips[0];
                mediumClip = clips[1];
                heavyClip = clips[2];
                sources = new List<AudioSource>();
                lightSource = GameObject.Find("SourceHolder").AddComponent("AudioSource") as AudioSource;
                lightSource.clip = Instance.findClip(lightClip);
                lightSource.loop = true;
                lightSource.volume = 1.0f;
                lightSource.playOnAwake = false;
                sources.Add(lightSource);

                mediumSource = GameObject.Find("SourceHolder").AddComponent("AudioSource") as AudioSource;
                mediumSource.clip = Instance.findClip(mediumClip);
                mediumSource.volume = 0.0f;
                mediumSource.loop = true;
                mediumSource.playOnAwake = false;
                sources.Add(mediumSource);

                heavySource = GameObject.Find("SourceHolder").AddComponent("AudioSource") as AudioSource;
                heavySource.clip = Instance.findClip(heavyClip);
                heavySource.volume = 0.0f;
                heavySource.loop = true;
                heavySource.playOnAwake = false;
                sources.Add(heavySource);

            }
            public void Play()
            {
                foreach(var source in sources)
                    source.Play();
                heavySource.volume = 0.0f;
                mediumSource.volume = 0.0f;
                running = true;
            }
            public void Stop()
            {
                running = false;
                foreach (var source in sources)
                    source.Stop();
                heavySource.volume = 0.0f;
                mediumSource.volume = 0.0f;
            }

            public void EnableLight()
            {
                if (!mediumSource || !heavySource || !running)
                    return;
                if (mediumSource.volume > .99f)
                    Instance.StartCoroutine(this.FadeOut(mediumSource));
                if (heavySource.volume > .99f)
                    Instance.StartCoroutine(this.FadeOut(heavySource));
            }
            public void EnableMedium()
            {
                if (!mediumSource || !heavySource || !running)
                    return;
                if (mediumSource.volume < .01f)
                    Instance.StartCoroutine(this.FadeIn(mediumSource));
                if (heavySource.volume > .99f)
                    Instance.StartCoroutine(this.FadeOut(heavySource));
            }
            public void EnableHeavy()
            {
                if (!mediumSource || !heavySource || !running)
                    return;
                if(mediumSource.volume > .99f)
                    Instance.StartCoroutine(this.FadeOut(mediumSource));
                if(heavySource.volume < .01f)
                    Instance.StartCoroutine(this.FadeIn(heavySource));
            }

            public IEnumerator FadeIn(AudioSource s)
            {
                float fadeInTime = 2.0f;
                float fadingVolume = 0.0f;
                while (fadeInTime > 0)
                {
                    yield return new WaitForSeconds(.01f);
                    fadingVolume += .01f/fadeInTime;
                    s.volume = fadingVolume;
                    fadeInTime -= Time.deltaTime;
                }
                s.volume = 1.0f;
            }
            public IEnumerator FadeOut(AudioSource s)
            {
                float fadeOutTime = 2.0f;
                float fadingVolume = 1.0f;
                while (fadeOutTime > 0)
                {
                    yield return new WaitForSeconds(.01f);
                    fadingVolume -= .01f/fadeOutTime;
                    s.volume = fadingVolume;
                    fadeOutTime -= Time.deltaTime;
                }
                s.volume = 0.0f;
            }
        }

        public enum MultiCueType
        {
            Parallel,
            Random
        };

        [Serializable]
        public class MultiCue
        {
            [HideInInspector]
            public string Name;
            [HideInInspector]
            public CueName CueName;
            private readonly MultiCueType _cueType;
            [Range(0, 1.0f)]
            public float Volume;
            [SerializeField] 
            List<ClipName> clips; // List of all Clips
            ProportionValue<ClipName>[] _cueWeightProportions; // Percentage based Cue List
            private AudioSource source;

            public MultiCueType type
            {
                get
                {
                    return _cueType;
                }
            }

            // Random MultiCue
            public MultiCue(CueName name, List<ProportionValue<ClipName>> list)
            {
                this.CueName = name;
                this.Name = AudioConstants.GetCueName(name);
                clips = new List<ClipName>();
                foreach (var pv in list)
                    clips.Add(pv.Value);
                _cueType = MultiCueType.Random;
                Volume = 1.0f;
                _cueWeightProportions = list.ToArray();
            }
            // Parallel MultiCue
            public MultiCue(CueName name, List<ClipName> clipNameList)
            {
                this.CueName = name;
                this.Name = AudioConstants.GetCueName(name);
                clips = clipNameList;
                _cueType = MultiCueType.Parallel;
                Volume = 1.0f;
            }

            // plays this MultiCue at sourceObj's world position
            public bool Play(GameObject sourceObj = null)
            {
                GameObject obj = sourceObj
                    ? sourceObj
                    : Instance.gameObject;

                switch (_cueType)
                {
                    case MultiCueType.Random:
                    {
                        ClipName c = _cueWeightProportions.ChooseByRandom();
                        return Instance.PlayClip(c, obj, Volume);
                    }
                    case MultiCueType.Parallel:
                        if (clips.Count == 0)
                            return false;
                        foreach (var clip in clips)
                            Instance.PlayClip(clip, obj, Volume);
                        return true;
                }
                return false;
            }
            public bool Play(AudioSource s)
            {
                switch (_cueType)
                {
                    case MultiCueType.Random:
                        {
                            ClipName c = _cueWeightProportions.ChooseByRandom();
                            return Instance.PlayClip(c, s);
                        }
                    case MultiCueType.Parallel:
                        if (clips.Count == 0)
                            return false;
                        foreach (var clip in clips)
                            Instance.PlayClip(clip, s);
                        return true;
                }
                return false;
            }

            // sets the volume this multiCue will be played at
            public bool SetVolume(float f)
            {
                if (f < 0 || f > 1)
                    return false;
                Volume = f;
                return true;
            }

            public float GetCurrentTrackLength()
            {
                if (_cueType != MultiCueType.Parallel)
                    return 0.0f;
                float maxTime = 0.0f;
                foreach(var clip in clips)
                    if (Instance.findClip(clip).length > maxTime)
                        maxTime = Instance.findClip(clip).length;
                return maxTime;
            }
        }
    }
}