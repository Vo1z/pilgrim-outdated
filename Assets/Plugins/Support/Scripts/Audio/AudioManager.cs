using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NaughtyAttributes;
namespace Support.AudioManagement { 
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField]
        private AudioPlayer[] audios;
        private Dictionary<string,AudioPlayer> _audioPlayersDict= new Dictionary<string, AudioPlayer>();

        private void Start()
        {
            foreach(var audio in audios)
            {
                _audioPlayersDict.Add(audio.Name, audio);
            }
        }
        private AudioPlayer GetAudioPlayer(string audioPlayer)
        {
            AudioPlayer player;
            if (!_audioPlayersDict.TryGetValue(audioPlayer, out player))
            {
                return null;
            }return player;
        }

        public void PlaySound(string audioPlayer, TypeOfSound type,string audioName)
        {
            var player = GetAudioPlayer(audioPlayer);
            player?.Play(type, audioName);
            
        }

        public void StopSound(string audioPlayer, TypeOfSound type, string audioName)
        {
            var player = GetAudioPlayer(audioPlayer);
            player?.Stop(type, audioName);
        }
    }
}
