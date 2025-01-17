﻿using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System;
#if __IOS__
using AVFoundation;
using Foundation;
#elif __ANDROID__
using Android.Net;
using Android.Media;
using Java.IO;
using Xamarin.Forms;
using Stream = System.IO.Stream;
using System.Linq;
using Math = Java.Lang.Math;
using IEnumerator = System.Collections.IEnumerator;
using IEnumerable = System.Collections.IEnumerable;
#elif NETFX_CORE
using System.Runtime.InteropServices;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Streams;
//using static Windows.ApplicationModel.Package;
#endif

namespace InnoTecheLearning
{
    public partial class Utils
    {
        /// <summary>
        /// A <see cref="StreamPlayer"/> that plays streams.
        /// </summary>
        public class StreamPlayer : ISoundPlayer, IDisposable
        {
            private StreamPlayer() : base() { }

            #region IDisposable Support
            public bool Disposed { get; private set; } = false; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (!Disposed)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects).
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                    // TODO: set large fields to null.
                    _player.Stop();
#if __IOS__
                    _player.Dispose();
#elif __ANDROID__
                    _prepared = false;
                    _player.Flush();
                    _player.Release();
                    _player.Dispose();
                    _content = null;
#elif NETFX_CORE
                    _player.ClearValue(MediaElement.SourceProperty);
#endif
                    _player = null;
                    Disposed = true;
                }
            }

            // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
            ~StreamPlayer()
            {
                // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                Dispose(false);
            }

            // This code added to correctly implement the disposable pattern.
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                Dispose(true);
                // TODO: uncomment the following line if the finalizer is overridden above.
                System.GC.SuppressFinalize(this);
            }
            #endregion
            public static StreamPlayer Create(Sounds Sound, bool Loop = false, float Volume = 1) =>
                Create(new PcmWavStream(Sound, Loop, Volume));
            public static StreamPlayer Play(Sounds Sound, bool Loop = false, float Volume = 1) =>
                Play(new PcmWavStream(Sound, Loop, Volume));
            /*
            public static StreamPlayer Create(int Frequency, TimeSpan Duration, bool Loop = false, float Volume = 1) =>
                Create(new ToneStream(Frequency, Duration, Loop, Volume));
            public static StreamPlayer Play(int Frequency, TimeSpan Duration, bool Loop = false, float Volume = 1) =>
                Play(new ToneStream(Frequency, Duration, Loop, Volume));
            public static StreamPlayer Create(int Frequency, int Seconds = 1, bool Loop = false, float Volume = 1) =>
                Create(new ToneStream(Frequency, Seconds, Loop, Volume));
            public static StreamPlayer Play(int Frequency, int Seconds = 1, bool Loop = false, float Volume = 1) =>
                Play(new ToneStream(Frequency, Seconds, Loop, Volume));
                */
            public static StreamPlayer Play(MusicStream Stream)
            {
                var Return = Create(Stream);
                Return.Play();
                return Return;
            }
#if __IOS__
            public static StreamPlayer Create(MusicStream Wave)
            {
                var Return = new StreamPlayer();
                Return.Init(Wave);
                return Return;
            }
            protected void Init(MusicStream Wave)
            {
                _player = AVAudioPlayer.FromData(NSData.FromStream(Wave.Content));
                _player.NumberOfLoops = Wave.Loop ? 0 : -1;
                _player.Volume = Wave.Volume;
            }
            AVAudioPlayer _player;
            [System.Obsolete("Only used in 0.10.0a105. Use Create(MusicStream).")]
            public static StreamPlayer Create(Stream Content, bool Loop = false, float Volume = 1)
            {
                var Return = new StreamPlayer();
                Return.Init(Content, Loop, Volume);
                return Return;
            }
            [System.Obsolete("Only used in 0.10.0a105. Use Init(MusicStream).")]
            protected void Init(Stream Content, bool Loop, float Volume)
            {
                _player = AVAudioPlayer.FromData(NSData.FromStream(Content));
                _player.NumberOfLoops = Loop ? 0 : -1;
                _player.Volume = Volume;
                //_player.FinishedPlaying += (object sender, AVStatusEventArgs e) => { _player = null; };
            }
            public void Play()
            { _player.Play(); }
            public void Pause()
            { _player.Pause(); }
            public void Stop()
            { _player.Stop(); }
            public event System.EventHandler Complete
            {
                add { _player.FinishedPlaying += (System.EventHandler<AVStatusEventArgs>)(System.MulticastDelegate)value; }
                remove { _player.FinishedPlaying -= (System.EventHandler<AVStatusEventArgs>)(System.MulticastDelegate)value; }
            }
            public float Volume { get { return _player.Volume; } set { _player.Volume = value; } }
            public bool Loop { get { return _player.NumberOfLoops == -1; } set { _player.NumberOfLoops = value ? -1 : 0; } }
#elif __ANDROID__
            AudioTrack _player;
            public bool _prepared { get; private set; }
            bool _loop;
            float _volume;
            int _frames, _buffersize;
            AudioTrackMode _mode;
            TimeSpan _duration;
            byte[] _content;
            protected MusicStream _Wave { get; private set; }
            public static StreamPlayer Create(MusicStream Wave)
            {
                return new StreamPlayer { _Wave = Wave };
            }
            protected void Init(MusicStream Wave)
            {
                _mode = (AudioTrackMode)Wave.Mode;
                if (_mode == AudioTrackMode.Static)
                    _player = new AudioTrack(
                    // Stream type
                    (Android.Media.Stream)Wave.Type,
                    // Frequency
                    Wave.SampleRate,
                    // Mono or stereo
                    (ChannelOut)Wave.Config,
                    // Audio encoding
                    (Encoding)Wave.Format,
                    // Length of the audio clip.
                    (int)Wave.SizeInBytes,
                    // Mode. Stream or static.
                    AudioTrackMode.Static);
                else _player = new AudioTrack(
                // Stream type
                (Android.Media.Stream)Wave.Type,
                // Frequency
                Wave.SampleRate,
                // Mono or stereo
                (ChannelOut)Wave.Config,
                // Audio encoding
                (Encoding)Wave.Format,
                // Length of the audio clip.
                _buffersize = AudioTrack.GetMinBufferSize(Wave.SampleRate,
                    (ChannelOut)Wave.Config, (Encoding)Wave.Format),
                // Mode. Stream or static.
                AudioTrackMode.Stream);
                _duration = Wave.Duration;
                _loop = Wave.Loop;
                _frames = Wave.Samples;
                _player.SetVolume(_volume = Wave.Volume);
                _player.SetNotificationMarkerPosition(_frames * 31 / 32);
                if (_mode == AudioTrackMode.Static)
                    _player.Write(Wave.Content.ReadFully(true), 0, (int)Wave.Content.Length);
                else
                {
                    Set((sender, e) => { if (_loop) { _player.Release(); Init(_Wave); Write(); }; });
                    _content = Wave.Content.ReadFully(true);
                }
                _prepared = true;
            }
            Task _play;
            protected virtual void Write()
            {
                _play = Task.Run(() => { 
                for (int i = 0; i <= _content.Length; i += _buffersize)
                {
                    _player.Write(_content, i, _buffersize);
                } });
            }
            public void Play()
            {
                Stop();
                Init(_Wave);
                if (_mode == AudioTrackMode.Static)
                {
                    if (_loop) _player.SetLoopPoints(0, _frames, -1);
                }
                else
                {
                    Write();
                    Device.StartTimer(_duration,
                        () => { Complete?.Invoke(this, EventArgs.Empty); return !Disposed && _loop; });
                }
                _player.Play();
            } 
            public void Pause()
            { if (_prepared) _player.Pause(); }
            public void Stop()
            {
                if (_player == null)
                    return;
                if (_loop) _player.SetLoopPoints(0, 0, 0);
                _player.Stop();
                _player.Release();
                _player = null;
            }
            bool _Set;
            void Set(EventHandler Handler)
            { if (_Set) return; Complete += Handler; _Set = true; }
            public event EventHandler Complete;
            public float Volume { get { return _volume; } set { _player?.SetVolume(_volume = value); } }
            public bool Loop { get { return _loop; } set { _loop = value;
                    if (_mode == AudioTrackMode.Static && _prepared && !value) _player.SetLoopPoints(0, 0, 0); } }
#elif __ANDROID__ && RESAMPLE
            AudioTrack _player;
            Stream _content;
            public bool _prepared { get; private set; }
            bool _loop;
            float _volume;
            int Rate, SampleRate;
            public static StreamPlayer Create(MusicStream Wave)
            {
                var Return = new StreamPlayer();
                Return.Init(Wave);
                return Return;
            }
            protected void Init(MusicStream Wave)
            {// To get preferred buffer size and sampling rate.
                AudioManager audioManager = (AudioManager)
                    Forms.Context.GetSystemService(Android.Content.Context.AudioService);
                Rate = int.Parse(audioManager.GetProperty(AudioManager.PropertyOutputSampleRate));
                //string Size = audioManager.GetProperty(AudioManager.PropertyOutputFramesPerBuffer);
                SampleRate = Wave.SampleRate;

                _content = Wave.Content;
                int SizeInBytes = Wave.SizeInBytes - 44;
                _player = new AudioTrack(
                // Stream type
                (Android.Media.Stream)Wave.Type,
                // Frequency
                Rate,
                // Mono or stereo
                (ChannelOut)Wave.Config,
                // Audio encoding
                (Encoding)Wave.Format,
                // Length of the audio clip.
                SizeInBytes,
                // Mode. Stream or static.
                (AudioTrackMode)Wave.Mode);
                _loop = Wave.Loop;
                _volume = Wave.Volume;
                _player.SetVolume(_volume = Wave.Volume);
#if true
                //int ch = Wave.Channels;
                //_start = 0;// (int)Wave.Content.Length / ch;
                //_stop = (int)Wave.Content.Length;// / ch / 2 / 2 + 16000
#elif false
                _player.SetNotificationMarkerPosition(SizeInBytes / 2);
                _player.MarkerReached += (object sender, AudioTrack.MarkerReachedEventArgs e) =>
                       { if (_loop) e.Track.SetPlaybackHeadPosition(0); };
#elif false
                Device.StartTimer(Wave.Duration, () => { if (_loop) _player.SetPlaybackHeadPosition(0); return _loop; });
#endif
                _prepared = true;
            }
            [Obsolete("Only used in 0.10.0a105. Use Create(MusicStream).")]
            public static StreamPlayer Create(Stream Content, bool Loop = false, float Volume = 1)
            {
                var Return = new StreamPlayer();
                Return.Init(Content, Loop, Volume);
                return Return;
            }
            [Obsolete("Only used in 0.10.0a105. Use Init(MusicStream).")]
            protected void Init(Stream Content, bool Loop, float Volume)
            {
                _content = Content;
                _player = new AudioTrack(
                // Stream type
                Android.Media.Stream.Music,
                // Frequency
                11025,
                // Mono or stereo
                ChannelOut.Mono,
                // Audio encoding
                Encoding.Pcm16bit,
                // Length of the audio clip.
                (int)Content.Length,
                // Mode. Stream or static.
                AudioTrackMode.Stream);
                _loop = Loop;
                _volume = Volume;
                _player.SetVolume(_volume = Volume);
                _player.SetNotificationMarkerPosition((int)Content.Length / 2);
                _prepared = true;
            }
            Iterator<byte> Resampled;
            const int ShortBuffer = 256;
            bool _pause, _stop;
            Task Ongoing;
            void PlayTask()
            {
                if(!_pause)Resampled = new Iterator<byte>(
                    Resample(_content.ReadFully(true).Skip(44).ToArray(), 44, SampleRate, Rate));
                _stop = _pause = false;
                int x;
                do
                {
                    do
                    {
                        x = _player.PlaybackHeadPosition;
                        _player.Write(Resampled.Take(ShortBuffer), 0, ShortBuffer);
                        do
                        {   // Montior playback to find when done
                        } while (_player.PlaybackHeadPosition < x + ShortBuffer);
                    } while (Resampled.HasPeek && !_pause && !_stop);
                } while (_loop && !_pause && !_stop);
            }
            public void Play()
            {
                if (!_prepared) return;
                _player.Play();
                Ongoing = Task.Run(new Action(PlayTask));
            }
            public void Pause()
            { if (_prepared) _pause = true; }
            public void Stop()
            {
                if (_player == null)
                    return;
                _stop = true;
                Do(Ongoing);

                _player.Stop();
                _player.Dispose();
                _player = null;
                _prepared = false;
            }
            public event EventHandler Complete
            {
                add
                {
                    _player.MarkerReached += MarkerReachedEventHandler(value);
                }
                remove
                {
                    _player.MarkerReached -= MarkerReachedEventHandler(value);
                }
            }
            protected EventHandler<AudioTrack.MarkerReachedEventArgs>
                MarkerReachedEventHandler(EventHandler value)
            {
                return (object sender, AudioTrack.MarkerReachedEventArgs e) =>
                   {
                       value(sender, e);
                   };
            }
            public float Volume { get { return _volume; } set { _player.SetVolume(_volume = value); } }
            public bool Loop { get { return _loop; } set { _loop = value; } }
            ~StreamPlayer()
            { Stop(); }
            public static IEnumerable<byte> Resample(byte[] samples, int fromSampleRate, int toSampleRate, int quality = 10)
            {
                int srcLength = samples.Length;
                var destLength = (long)samples.Length * toSampleRate / fromSampleRate;
                var dx = srcLength / destLength;

                // fmax : nyqist half of destination sampleRate
                // fmax / fsr = 0.5;
                var fmaxDivSR = 0.5;
                var r_g = 2 * fmaxDivSR;

                // Quality is half the window width
                var wndWidth2 = quality;
                var wndWidth = quality * 2;

                var x = 0;
                int i, j;
                double r_y;
                int tau;
                double r_w;
                double r_a;
                double r_snc;
                for (i = 0; i < destLength; ++i)
                {
                    r_y = 0.0;
                    for (tau = -wndWidth2; tau < wndWidth2; ++tau)
                    {
                        // input sample index
                        j = x + tau;

                        // Hann Window. Scale and calculate sinc
                        r_w = 0.5 - 0.5 * Math.Cos(2 * Math.Pi * (0.5 + (j - x) / wndWidth));
                        r_a = 2 * Math.Pi * (j - x) * fmaxDivSR;
                        r_snc = 1.0;
                        if (r_a != 0)
                            r_snc = Math.Sin(r_a) / r_a;

                        if ((j >= 0) && (j < srcLength))
                        {
                            r_y += r_g * r_w * r_snc * samples[j];
                        }
                    }
                    yield return (byte)r_y;
                    x += (int)dx;
                }
            }
            public class Iterator<T> : IEnumerator<T>//, IEnumerable<T>
            {
                private IEnumerator<T> _enumerator;
                private T _peek;
                private bool _didPeek;

                public Iterator(IEnumerable<T> enumerable) : this(enumerable.GetEnumerator()) { }

                public Iterator(IEnumerator<T> enumerator)
                {
                    if (enumerator == null)
                        throw new ArgumentNullException("enumerator");
                    _enumerator = enumerator;
                }

#region IEnumerator implementation
                public bool MoveNext()
                {
                    return _didPeek ? !(_didPeek = false) : _enumerator.MoveNext();
                }

                public void Reset()
                {
                    _enumerator.Reset();
                    _didPeek = false;
                }

                object IEnumerator.Current { get { return this.Current; } }
#endregion

#region IDisposable implementation
                public void Dispose()
                {
                    _enumerator.Dispose();
                }
#endregion

#region IEnumerator<T> implementation
                public T Current
                {
                    get { return _didPeek ? _peek : _enumerator.Current; }
                }
#endregion
                /*
#region IEnumerable implementation
                public IEnumerator<T> GetEnumerator()
                {
                    return this; 
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return this; 
                }
#endregion
                */
                private void TryFetchPeek()
                {
                    if (!_didPeek && (_didPeek = _enumerator.MoveNext()))
                    {
                        _peek = _enumerator.Current;
                    }
                }

                public T Peek
                {
                    get
                    {
                        TryFetchPeek();
                        if (!_didPeek)
                            throw new InvalidOperationException("Enumeration already finished.");

                        return _peek;
                    }
                }

                public bool HasPeek
                { get { try { DoNothing(Peek); return true; } catch (InvalidOperationException) { return false; } } }

                public T[] Take(int Count)
                {
                    var Return = new T[Count];
                    for (int i = 0; i < Count && HasPeek; i++)
                    { MoveNext(); Return[i] = Current; }
                    return Return;
                }

                public void Skip(int Count)
                {
                    for (int i = 0; i < Count && HasPeek; i++)
                        MoveNext();
                }
            }
#elif NETFX_CORE
            MediaElement _player;
            public static StreamPlayer Create(MusicStream Wave)
            {
                var Return = new StreamPlayer();
                Return.Init(Wave);
                return Return;
            }
            protected void Init(MusicStream Wave)
            {
                _player = new MediaElement
                {
                    IsMuted = false,
                    Position = new TimeSpan(0, 0, 0),
                    Volume = Wave.Volume,
                    IsLooping = Wave.Loop,
                };
                _player.SetSource(Wave.Content.AsRandomAccessStream(), Wave.MimeType);
            }/*
            [Obsolete("Only used in 0.10.0a105. Use Init(MusicStream).")]
            public static StreamPlayer Create(Stream Content, bool Loop = false, float Volume = 1)
            {
                var Return = new StreamPlayer();
                Return.Init(Content, Loop, Volume);
                return Return;
            }
            [Obsolete("Only used in 0.10.0a105. Use Init(MusicStream).")]
            protected void Init(Stream Content, bool Loop, float Volume)
            {
                _player = new MediaElement
                {
                    IsMuted = false,
                    Position = new TimeSpan(0, 0, 0),
                    Volume = Volume,
                    IsLooping = Loop
                };
                _player.SetSource(Content.AsRandomAccessStream(), GetMime(Content.ReadFully()));
            }*/
            public void Play()
            { _player?.Play(); }
            public void Pause()
            { _player?.Pause(); }
            public void Stop()
            { _player?.Stop(); }
            public float Volume { get { return (float)_player.Volume; } set { _player.Volume = value; } }
            public event EventHandler Complete
            {
                add { _player.MediaEnded += (global::Windows.UI.Xaml.RoutedEventHandler)(MulticastDelegate)value; }
                remove { _player.MediaEnded -= (global::Windows.UI.Xaml.RoutedEventHandler)(MulticastDelegate)value; }
            }
            public bool Loop { get { return _player.IsLooping; } set { _player.IsLooping = value; } }
            /*
            [DllImport(@"urlmon.dll"), Obsolete("Only used in 0.10.0a105. Will very likely be removed soon.", true)]
            private extern static uint FindMimeFromData(uint pBC, [MarshalAs(UnmanagedType.LPStr)] string pwzUrl,
                                                        [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, uint cbSize,
                                                        [MarshalAs(UnmanagedType.LPStr)] string pwzMimeProposed,
                                                        uint dwMimeFlags, out uint ppwzMimeOut, uint dwReserverd);
            [Obsolete("Only used in 0.10.0a105. Will very likely be removed soon.", true)]
            public static string GetMime(byte[] buffer)
            {
                try
                {
                    uint mimetype;
                    FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                    IntPtr mimeTypePtr = new IntPtr(mimetype);
                    string mime = Marshal.PtrToStringUni(mimeTypePtr);
                    Marshal.FreeCoTaskMem(mimeTypePtr);
                    return mime;
                }
                catch (Exception)
                {
                    return "unknown/unknown";
                }
            }*/
#endif
        }
    }
}