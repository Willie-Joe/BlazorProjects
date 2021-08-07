using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PomodoroTiimer.Pages
{
    public class TimerBase : ComponentBase, IDisposable
    {
        private int timeLeft = 25 * 60;

        protected string remaining => TimeSpan.FromSeconds(timeLeft).ToString(@"mm\:ss");

        protected System.Threading.Timer timer;

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        protected bool IsRunning = false;
        protected bool IsStopped { get { return !IsRunning; } }

        protected string Colour = "#000000";


        protected void Start()
        {
            timer?.Dispose();
            timer = new System.Threading.Timer(_ =>
            {
                if (timeLeft > 0)
                {
                    timeLeft -= 1;
                    Console.WriteLine("timer ticked"); // add this
                    SetRandomColour();
                    InvokeAsync(StateHasChanged);
                    JSRuntime.InvokeVoidAsync("setTitle", remaining);
                    IsRunning = true;

                }
            }, null, 0, 1000);
        }

        public void Reset()
        {

            Dispose();
            timeLeft = 25 * 60;
        }
        

        public void Dispose()
        {

            timer?.Dispose();
            IsRunning = false;
            Colour = "#000000";
        }

        protected void SetRandomColour()
        {
            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));
            Colour = color;
        }
    }
}
