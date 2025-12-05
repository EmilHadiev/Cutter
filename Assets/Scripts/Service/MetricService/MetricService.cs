using System.Collections.Generic;
using YG;

public class MetricService : IMetricService
{
    public void SendMetric(string metricName)
    {
        YG2.MetricaSend(metricName);
    }

    public void SendMetric(string metricName, string subMetric, string param)
    {
        YG2.MetricaSend(metricName, subMetric, param);
    }

    public void SendMetric(string metricName, Dictionary<string, string> data)
    {
        YG2.MetricaSend(metricName, data);
    }
}