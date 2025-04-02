using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Graphics;

namespace D424.ViewModels;

public partial class TimerViewModel : ObservableObject
{
    private CancellationTokenSource _cancellationTokenSource;
    private TimeSpan _remainingTime;
    private bool _isRunning;

    [ObservableProperty]
    private string timerDisplay = "00:00";

    [ObservableProperty]
    private string phaseDisplay = "Idle"; 

    [ObservableProperty]
    private string startStopButtonText = "Start";

    private int _selectedRunMinutes = 1;
    public int SelectedRunMinutes
    {
        get => _selectedRunMinutes;
        set
        {
            SetProperty(ref _selectedRunMinutes, value);
            UpdateTimerDisplay();
        }
    }

    private int _selectedRunSeconds = 0;
    public int SelectedRunSeconds
    {
        get => _selectedRunSeconds;
        set
        {
            SetProperty(ref _selectedRunSeconds, value);
            UpdateTimerDisplay();
        }
    }

    private int _selectedRestMinutes = 0;
    public int SelectedRestMinutes
    {
        get => _selectedRestMinutes;
        set
        {
            SetProperty(ref _selectedRestMinutes, value);
            UpdateTimerDisplay();
        }
    }

    private int _selectedRestSeconds = 30;
    public int SelectedRestSeconds
    {
        get => _selectedRestSeconds;
        set
        {
            SetProperty(ref _selectedRestSeconds, value);
            UpdateTimerDisplay();
        }
    }
    
    private int _selectedIntervals = 1;
    public int SelectedIntervals
    {
        get => _selectedIntervals;
        set
        {
            SetProperty(ref _selectedIntervals, value);
            UpdateTimerDisplay();
        }
    }
    
    public ObservableCollection<int> MinuteOptions { get; } = [];
    public ObservableCollection<int> SecondOptions { get; } = [];
    public ObservableCollection<int> IntervalOptions { get; } = [];

    
    private Color _phaseTextColor = Colors.White;
    public Color PhaseTextColor
    {
        get => _phaseTextColor;
        set => SetProperty(ref _phaseTextColor, value);
    }
    
    private Color _startStopButtonColor = Colors.LimeGreen;
    public Color StartStopButtonColor
    {
        get => _startStopButtonColor;
        set => SetProperty(ref _startStopButtonColor, value);
    }
    
    private Color _timerBackgroundColor = Colors.Gray;
    public Color TimerBackgroundColor
    {
        get => _timerBackgroundColor;
        set => SetProperty(ref _timerBackgroundColor, value);
    }
    
    public TimerViewModel()
    {
        for (int i = 0; i < 60; i++)
        {
            MinuteOptions.Add(i);
            SecondOptions.Add(i);
        }
        for (int i = 1; i <= 60; i++)
        {
            IntervalOptions.Add(i);
        }
        
        UpdateTimerDisplay();
    }

    [RelayCommand]
    private void StartStop()
    {
        if (_isRunning)
        {
            StopTimer();
        }
        else
        {
            StartTimer();
        }
    }

    private async void StartTimer()
{
    _isRunning = true;
    StartStopButtonText = "Stop";
    StartStopButtonColor = Colors.Red;

    _cancellationTokenSource = new CancellationTokenSource();

    try
    {
        PhaseDisplay = "Get Ready";
        TimerBackgroundColor = Colors.Orange;
        PhaseTextColor = Colors.Orange;

        for (int i = 5; i > 0; i--)
        {
            TimerDisplay = i.ToString();
            await Task.Delay(1000, _cancellationTokenSource.Token);
        }

        for (int i = 0; i < SelectedIntervals; i++)
        {
            PhaseDisplay = "Run";
            TimerBackgroundColor = Colors.LimeGreen;
            PhaseTextColor = Colors.LimeGreen;
            _remainingTime = TimeSpan.FromMinutes(SelectedRunMinutes).Add(TimeSpan.FromSeconds(SelectedRunSeconds));
            await CountdownPhase(_cancellationTokenSource.Token);

            if (i < SelectedIntervals - 1 && (SelectedRestMinutes > 0 || SelectedRestSeconds > 0))
            {
                PhaseDisplay = "Rest";
                TimerBackgroundColor = Colors.Red;
                PhaseTextColor = Colors.Red;
                _remainingTime = TimeSpan.FromMinutes(SelectedRestMinutes).Add(TimeSpan.FromSeconds(SelectedRestSeconds));
                await CountdownPhase(_cancellationTokenSource.Token);
            }
        }
    }
    catch (OperationCanceledException)
    {
        // Timer stopped
    }

    StopTimer();
}


    private async Task CountdownPhase(CancellationToken cancellationToken)
    {
        while (_remainingTime.TotalSeconds > 0)
        {
            TimerDisplay = $"{_remainingTime:mm\\:ss}";
            
            if (_remainingTime.TotalSeconds == 5)
            {
                TimerBackgroundColor = Colors.Orange;
                PhaseTextColor = Colors.Orange;
            }

            await Task.Delay(1000, cancellationToken);
            _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
        }
    }

    private void StopTimer()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = null;

        _isRunning = false;
        StartStopButtonText = "Start";
        StartStopButtonColor = Colors.LimeGreen;
        TimerBackgroundColor = Colors.Gray;
        PhaseTextColor = Colors.White; 
        PhaseDisplay = "Idle";
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        TimerDisplay = $"{SelectedRunMinutes:D2}:{SelectedRunSeconds:D2}";
    }
}