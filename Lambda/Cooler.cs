
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lambda
{
    public class Cooler
    {

        public Cooler(float dd)
        {
            _temperature = dd;
        }
        private float _temperature;

        public float Temperrature
        {
            get
            {
                return _temperature;
            }
            set
            {
                _temperature = value;
            }
        }

        public void OnTemperatureChanged(float newTemperature)
        {
            if(newTemperature >_temperature)
            {
                throw new Exception("Cooler 调用异常");
                //Console.WriteLine("Cooler ON");
            }else
            {
                Console.WriteLine("Cooler OFF");
            }
        }
    }
}