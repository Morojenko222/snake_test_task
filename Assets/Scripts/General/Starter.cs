using System;
using Config;
using UnityEngine;
using Views;

public class Starter : MonoBehaviour
{
	[SerializeField] private MainConfig _mainConfig;
	[SerializeField] private SceneView _sceneView;
	
	private GameBootstrap _gameBootstrap;
	
    private void Start()
    {
	    _gameBootstrap = new GameBootstrap(_mainConfig, _sceneView);
	    _gameBootstrap.Start();
    }

    private void Update()
    {
	    _gameBootstrap.Update();
    }

    private void OnDestroy()
    {
	    _gameBootstrap.Destroy();
    }
}