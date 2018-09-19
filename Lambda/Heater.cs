using System;
using System.Collections.Generic;
using System.Text;

namespace Lambda
{
    class Heater
    {
        private float _temperature;

        public Heater(float temperature)
        {
            _temperature = temperature;
        }

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

            if (newTemperature < _temperature)
            {
                Console.WriteLine("Heater ON");
            }
            else
            {
                Console.WriteLine("Heater OFF");
            }
        }
    }
}
