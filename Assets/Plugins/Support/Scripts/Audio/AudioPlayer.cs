using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Support.Extensions;
namespace Support.AudioManagement
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {

        [SerializeField]
        private List<AudioWrapper> audios;

        private AudioSource _source;
        private Dictionary<TypeOfSound, AudioWrapper> _audiosDict = new Dictionary<TypeOfSound, AudioWrapper>();
        private Dictionary<Audio, AudioSource> _sourcesDict = new Dictionary<Audio, AudioSource>();

        public List<AudioWrapper> Audios => audios;

        private void Awake()
        {
            foreach(var wrap in audios)
            {
                _audiosDict.Add(wrap.Type, wrap);
            }
        }

        private Audio GetAudio(TypeOfSound type, string name)
        {
            AudioWrapper wrap;
            if (!_audiosDict.TryGetValue(type, out wrap))
            {
                return null;
            }

            var audio = wrap.GetAudio(name);
            return audio;
        }
        /// <summary>
        /// Function finds Audio based on a type and name.
        /// If the audio is already being played, stops.
        /// If not playes it on the main or additional audio source.
        /// </summary>
        public void Play(TypeOfSound type, string name)
        {
            //checks if audio with such name and type exist in THIS audioplayer
            var audio = GetAudio(type, name);
            if(audio == null)
            {
                return;
            }

            //links _source with audio source
           if (_source == null)
            {
                _source = gameObject.GetComponent<AudioSource>();
            }

            //if the audio is already being played, stops
            AudioSource src;
            if (_sourcesDict.TryGetValue(audio, out src))
            {
                return;
            }
            //checks if main Audio source is unoccupied, if not creates a new one 
            if (!_source.isPlaying)
            {
                src = _source;
            }
            else
            {
                src = gameObject.AddComponent<AudioSource>();
            }
            //configures audio
            src.volume = audio.Volume;
            src.clip = audio.Clip;
            src.loop = audio.Loop;
            src.pitch = audio.Pitch;
            src.priority = audio.Prority;
            src.spatialBlend = audio.SpatialBlend;
            src.minDistance = audio.MinDistance;
            src.maxDistance = audio.MaxDistance;
            src.dopplerLevel = audio.DopplerLevel;
            src.spread = audio.Spread;
            //adds audio source to the dictionary of occupied audio sources and starts playing
            _sourcesDict.Add(audio, src);
            src.Play();
            //after audio is ended, remove itself from dictionary and a addition audio surce
            // if(!audio.Loop) 
            this.WaitAndDoCoroutine(src.clip.length, () => Stop(type, name))  ;
        }
        /// <summary>
        /// If the found audio is still being played, forces it to stop and removes additional audio source
        /// </summary>
        public void Stop(TypeOfSound type, string name)
        {
            //checks if audio with such name and type exist in THIS audioplayer
            var audio = GetAudio(type, name);
            if (audio == null)
            {
                return;
            }
            //fetchs an audio source
            AudioSource res;
            if (!_sourcesDict.TryGetValue(audio, out res))
            {
                return;
            }
            res.Stop();
            //remove audio source from the dictionary and checks if the main audio source is not removed 
            Debug.Log(1);
            _sourcesDict.Remove(audio);
            if (_sourcesDict.Count == 0)
            {
                _source = res;
            }
            else
            {
                Destroy(res);
            }
        }

    }
}