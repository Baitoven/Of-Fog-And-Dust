using UnityEngine.Events;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

namespace OfFogAndDust.Utils
{
    internal class Timer
    {
        private float duration;
        private UnityEvent onTimerEnd = new UnityEvent();
        private UnityEvent onTimerStart = new UnityEvent();
        private WaitForSeconds waitForSeconds;
        private Task task;

        public UnityEvent OnTimerEnd => onTimerEnd;
        public UnityEvent OnTimerStart => onTimerStart;

        public float Duration
        {
            get => duration;
            set
            {
                if (task != null && task.Status == TaskStatus.Running)
                {
                    StopTimer();
                }
                waitForSeconds = new WaitForSeconds(value);
                duration = value;
            }
        }

        public Timer(float duration)
        {
            Duration = duration;
            task = new Task(async () => await DoTrackTime());
        }

        public void StartTimer()
        {
            if (task.Status != TaskStatus.Running)
            {
                OnTimerStart.Invoke();
                task.Start();
            }
        }

        public void StopTimer()
        {
            if (task.Status == TaskStatus.Running)
            {
                task.Dispose();
                OnTimerEnd.Invoke();
                task = new Task(async () => await DoTrackTime());
            }
        }

        async Task DoTrackTime()
        {
            await waitForSeconds;
            OnTimerEnd.Invoke();
        }
    }
}
