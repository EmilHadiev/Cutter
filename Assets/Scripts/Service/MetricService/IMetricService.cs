using System.Collections.Generic;

public interface IMetricService
{
    void SendMetric(string metricName);
    void SendMetric(string metricName, string subMetric, string param);
    void SendMetric(string metricName, Dictionary<string, string> data);
}