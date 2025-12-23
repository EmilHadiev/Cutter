using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RewardAdder : MonoBehaviour
{
    [SerializeField] private Image _filledImage;

    private IFactory _factory;
    private PlayerProgress _progress;

    [Inject]
    private void Constructor(IFactory factory, PlayerProgress playerProgress)
    {

    }
}