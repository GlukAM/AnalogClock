using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalogClock.Model
{
    public class ClockModel //Модель часов. Является источником информации
    {
        public int year; //Год

        public int month; //Месяц

        public int day; //День

        public int hour; //Час
        public float hourAngle;//Угол часовой стрелки часов

        public int minute;//минута
        public float minuteAngle;//Угол минутной стрелки часов

        public int second;//секунда
        public float secondAngle;//угол секундной стрелки часов

        public ClockModel()//конструктор модели
        {
            Update();
        }

        public void Update()//Метод, обновляющий все поля
        {
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;
            second = DateTime.Now.Second;

            int hourForFront;
            if (hour >= 12) { hourForFront = hour - 12; }
            else { hourForFront = hour; }

            hourAngle = (second + (minute * 60) + (hourForFront * 60 * 60)) / 43200f * 360;
            minuteAngle = (second + (minute * 60)) / 3600f * 360;
            secondAngle = second / 60f * 360;
        }
    }
}
