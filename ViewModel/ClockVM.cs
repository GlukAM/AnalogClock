using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using AnalogClock.Model;

namespace AnalogClock.ViewModel
{
    internal abstract class ViewModel : INotifyPropertyChanged //интервейс для динамической поддержки изменения значений
    {

        public event PropertyChangedEventHandler PropertyChanged;//Для реализации INotifyPropertyChangedинтерфейса необходимо зарегистрировать PropertyChangedEventHandler делегата как событие.

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {//Разрешает кольцевые изменения
            if (Equals(field, value)) return false; // если значение менять не надо то false
            field = value; //изменяем поле
            OnPropertyChanged(PropertyName); //генерируем союытие об изменении
            return true;
        }

        public ViewModel()
        {

        }

    }
    class ClockVM : ViewModel
    {
        public string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        public string date;
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public float hourAngle;
        public float HourAngle
        {
            get { return hourAngle; }
            set
            {
                hourAngle = value;
                OnPropertyChanged("HourAngle");
            }
        }

        public float minuteAngle;
        public float MinuteAngle
        {
            get { return minuteAngle; }
            set
            {
                minuteAngle = value;
                OnPropertyChanged("MinuteAngle");
            }
        }

        public float secondAngle;
        public float SecondAngle
        {
            get { return secondAngle; }
            set
            {
                secondAngle = value;
                OnPropertyChanged("SecondAngle");
            }
        }


        private static Timer aTimer;

        ClockModel clockmodel;

        public ClockVM()
        {
            clockmodel = new ClockModel();

            Time = $"{clockmodel.hour} : {clockmodel.minute} : {clockmodel.second}";

            TimerInit();

        }

        public void Update()
        {
           
            clockmodel.Update();

            HourAngle = clockmodel.hourAngle;

            MinuteAngle = clockmodel.minuteAngle;

            SecondAngle = clockmodel.secondAngle;

            Date = $"{clockmodel.day} . {clockmodel.month} . {clockmodel.year}";

            Time = $"{clockmodel.hour} : {clockmodel.minute} : {clockmodel.second}";

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Update();
        }

        private void TimerInit()
        {
            aTimer = new Timer(1);

            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            aTimer.Enabled = true;
        }

    }
}
