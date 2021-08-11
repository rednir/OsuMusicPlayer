using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using OsuMusicPlayer.Models;
using System.ComponentModel;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Media.Imaging;
using SharpAudio.Codec;
using SharpAudio;

namespace OsuMusicPlayer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _nowPlayingText = "Loading...";

        private int _sliderValue = 0;

        private Bitmap? _backgroundImage = null;

        private bool _isPlaying = true;

        private int _songIndex = 0;

        public MainWindowViewModel()
        {
            DefaultBackground = new Bitmap(Assets.Open(new Uri("avares://OsuMusicPlayer/Assets/default-bg.jpg")));

            //DirectoryInfo dir = new(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\osu!\\Songs");
            DirectoryInfo dir = new("/home/davran/.osu/Songs");
            UserData.Songs.Populate(dir);
        }

        public IAssetLoader Assets { get; set; } = AvaloniaLocator.Current.GetService<IAssetLoader>();

        public string NowPlayingText
        {
            get => _nowPlayingText;
            set
            {
                _nowPlayingText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NowPlayingText)));
            }
        }

        public int SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SliderValue)));
            }
        }

        public string PausePlayButtonContent
        {
            get => _isPlaying ? "Pause" : "Play";
        }

        public Bitmap BackgroundImage
        {
            get => _backgroundImage ?? DefaultBackground;
            set
            {
                _backgroundImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundImage)));
            }
        }

        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PausePlayButtonContent)));
            }
        }

        public int SongIndex
        {
            get => _songIndex;
            set
            {
                if (value >= UserData.Songs.Count)
                    _songIndex = 0;
                else if (value < 0)
                    _songIndex = UserData.Songs.Count - 1;
                else
                    _songIndex = value;

                SetSong(UserData.Songs[_songIndex]);
            }
        }

        public SoundStream? SoundStream { get; set; } = null;

        private UserData UserData { get; set; } = new();

        private Bitmap DefaultBackground { get; }

        private AudioEngine AudioEngine { get; } = AudioEngine.CreateDefault();

        public void OnPausePlayButtonClicked() => IsPlaying = !IsPlaying;

        public void OnPreviousButtonClicked() => SongIndex--;

        public void OnNextButtonClicked() => SongIndex++;

        private void SetSong(OsuSong song)
        {
            SoundStream?.Stop();

            NowPlayingText = song.Artist + " - " + song.Title;
            BackgroundImage = song.BackgroundFileName == null ? DefaultBackground : new Bitmap(song.FolderPath + "/" + song.BackgroundFileName);

            SoundStream = new(File.OpenRead(song.FolderPath + "/" + song.AudioFileName), AudioEngine.CreateDefault());
            SoundStream.Volume = 0.4f;
            SoundStream.Play();
        }
    }
}
