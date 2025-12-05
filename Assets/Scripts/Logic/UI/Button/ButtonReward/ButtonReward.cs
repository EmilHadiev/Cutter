using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public abstract class ButtonReward : MonoBehaviour
{
    [SerializeField] private Button _button;

    protected IAdvService AdvService;
    protected IMetricService MetricService;

    private IUISoundContainer _sound;

    private void OnValidate() => _button ??= GetComponent<Button>();

    private void OnEnable() => _button.onClick.AddListener(AddReward);

    private void OnDisable() => _button.onClick.RemoveListener(AddReward);

    [Inject]
    private void Constructor(IAdvService adv, ICoinsStorage coinsStorage, 
        IUISoundContainer sounds, IMetricService metric)
    {
        AdvService = adv;
        MetricService = metric;
        _sound = sounds;
    }

    private void ProcessReward()
    {
        SetReward();
        SendMetric();

        _sound.Stop();
        _sound.Play(SoundsName.AddRewardCoins);
    }

    private void AddReward()
    {
        AdvService.ShowReward(ProcessReward);
    }

    protected abstract void SetReward();
    protected abstract void SendMetric();
}