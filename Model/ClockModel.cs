using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalogClock.Model
{
    public class ClockModel
    {
        public int year;

        public int month;

        public int day;

        public int hour;
        public float hourAngle;

        public int minute;
        public float minuteAngle;

        public int second;
        public float secondAngle;

        public ClockModel()
        {
            Update();
        }

        public void Update()
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
