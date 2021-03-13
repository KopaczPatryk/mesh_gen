using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MapGen;
using UnityEngine;

public class InstanceFactory {
	private IMapProvider currentMapProvider;

	private static InstanceFactory instance;
	public static InstanceFactory GetInstance() {
		if (instance == null)
		{
			instance = new InstanceFactory();
		}
		return instance;
	}
	public IMapProvider GetMapProvider() {
		if(currentMapProvider == null)
			currentMapProvider = PerlinMapProvider.GetInstance();
		return currentMapProvider;
	}
}
