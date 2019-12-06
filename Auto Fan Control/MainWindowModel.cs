using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Auto_Fan_Control.Configuration;
using Auto_Fan_Control.FanControl.Events;
using JetBrains.Annotations;

namespace Auto_Fan_Control
{
    public class MainWindowModel : INotifyPropertyChanged, IAsyncDisposable
    {
        private readonly FanControl.FanControl _fanControl;
        
        private TrackingEvent _trackingEvent;
        private bool _fanRunning;

        public TrackingEvent TrackingEvent
        {
            get => _trackingEvent;
            set
            {
                if (Equals(value, _trackingEvent)) return;
                _trackingEvent = value;
                OnPropertyChanged();
            }
        }

        public bool FanRunning
        {
            get => _fanRunning;
            set
            {
                if (value == _fanRunning) return;
                _fanRunning = value;
                OnPropertyChanged();
            }
        }

        public Options Options { get; }

        public MainWindowModel()
        {
            Options = new Options(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.json"));

            _fanControl = new FanControl.FanControl(Options);
            _fanControl.DataRecieved += e =>
            {
                TrackingEvent = e;
                return Task.CompletedTask;
            };
            _fanControl.FanStatus += e =>
            {
                FanRunning = e;
                return Task.CompletedTask;
            };
        }

        public async Task Init()
        {
            await Options.Load();
            _fanControl.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ValueTask DisposeAsync() => _fanControl.DisposeAsync();
    }
}