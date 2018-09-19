using System;
using System.Collections.Generic;
using System.Text;

namespace Lambda
{
    /// <summary>
    /// 事件发布者
    /// </summary>
    public class Thermostat
    {
        public Action<float> OnTemperatureChanged { get; set; }

        public float CurrentTemperature
        {
            get { return _currentTemperature; }
            set
            {
                if(value != _currentTemperature)
                {
                    _currentTemperature = value;
                    List<Exception> exceptionList = new List<Exception>();
                    foreach (Action<float> handle in OnTemperatureChanged.GetInvocationList())
                    {
                        try
                        {
                            handle(_currentTemperature);
                        }
                        catch (Exception ex)
                        {

                            exceptionList.Add(ex);
                        }
                    }
                    if(exceptionList.Count > 0)
                    {
                        Console.WriteLine("调用链发生了异常:{0}个异常",exceptionList.Count);
                    }
               
                  
                }
              
            }
        }
        private float _currentTemperature;
    }
}
