using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MapGen;
using Assets.Scripts.TreeGen.SchemaGenerators;
using UnityEngine;

public class InstanceFactory {
    private static InstanceFactory instance;
    private IMapProvider currentMapProvider;
    private System.Random randomProvider;
    private PartGenerator treeGenerator;
    public static InstanceFactory GetInstance() {
        if (instance == null) {
            instance = new InstanceFactory();
        }
        return instance;
    }
    public IMapProvider GetMapProvider() {
        if (currentMapProvider == null)
            currentMapProvider = PerlinMapProvider.GetInstance();
        return currentMapProvider;
    }

    public System.Random GetRandom() {
        if (randomProvider == null) {
            var r = new System.Random();
            // var seed = new System.Random().Next();
            var seed = 913652544;
            // Debug.Log($"Seed {seed}");

            randomProvider = new System.Random(seed);
        }

        return randomProvider;
    }

    public PartGenerator GetTreeGenerator() {
        if (treeGenerator == null)
            treeGenerator = new PartGenerator(GetRandom());
        return treeGenerator;
    }
}