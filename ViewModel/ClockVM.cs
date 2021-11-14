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
    class ClockVM : ViewModel // Модель представления часов
    {
        private string time; // Поле для отображения времени цифрами
        public string Time // Конструкция для связи через Binding
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        private string date; // Поле для отображения даты
        public string Date // Конструкция для связи через Binding
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private float hourAngle; // Угол часовой стрелки
        public float HourAngle // Конструкция для связи через Binding
        {
            get { return hourAngle; }
            set
            {
                hourAngle = value;
                OnPropertyChanged("HourAngle");
            }
        }

        private float minuteAngle; //Угол минутной стрелки
        public float MinuteAngle // Конструкция для связи через Binding
        {
            get { return minuteAngle; }
            set
            {
                minuteAngle = value;
                OnPropertyChanged("MinuteAngle");
            }
        }

        private float secondAngle; //Угол секундной стрелки
        public float SecondAngle // Конструкция для связи через Binding
        {
            get { return secondAngle; }
            set
            {
                secondAngle = value;
                OnPropertyChanged("SecondAngle");
            }
        }


        private static Timer aTimer; //Объявление таймера

        ClockModel clockmodel; //Объявление объекта модели часов

        public ClockVM() // Конструктор модели представления
        {
            clockmodel = new ClockModel(); // Создание объекта содели часов

            Time = $"{clockmodel.hour} : {clockmodel.minute} : {clockmodel.second}"; //Запись времени в цифрах

            TimerInit(); //Настройка таймера

        }

        public void Update() //Метод обновления информации. Обновляет значения в модели и записывает в объекты, связанные с представлением
        {
           
            clockmodel.Update(); //Обновляем модель

            HourAngle = clockmodel.hourAngle; //Меняем угол часовой

            MinuteAngle = clockmodel.minuteAngle;//меняем угол минутной

            SecondAngle = clockmodel.secondAngle; //меняем угол секундной

            Date = $"{clockmodel.day} . {clockmodel.month} . {clockmodel.year}"; //меняем дату

            Time = $"{clockmodel.hour} : {clockmodel.minute} : {clockmodel.second}"; // меняем время

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e) //Вызывается при переполнении таймера
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
