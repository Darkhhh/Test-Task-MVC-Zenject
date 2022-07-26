using System.Collections.Generic;
using Tiles;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [Header("Scene Objects")]
        [SerializeField] private View view;
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Transform tilesParentObject;
        
        [Header("Variables")]
        [SerializeField] private int winGoldNumber;
        [SerializeField] private int defaultShovelsNumber;
        [SerializeField] [Range(1, 10)]private int tilesInRow;
        [SerializeField] [Range(0f, 1f)] private float goldProbability;
        [SerializeField] private int defaultDepth;
        
        public override void InstallBindings()
        {
            CreateTiles();
            Container.Bind<Model>().AsSingle().WithArguments(defaultShovelsNumber, winGoldNumber, tilesInRow, goldProbability, defaultDepth).NonLazy();
            Container.Bind<View>().FromInstance(view);
            Container.Bind<Controller>().AsSingle().WithArguments(view).NonLazy();
        }
        private void CreateTiles()
        {
            List<TileScript> tiles = new List<TileScript>();
            var startValue = (float)tilesInRow / 2 - 0.5f * ((tilesInRow + 1) % 2);
            float currentX = -startValue, currentY = startValue;
            for(var i = 0; i < tilesInRow; i++){
                for(var j = 0; j< tilesInRow; j++)
                {
                    TileScript tileScript = Container.InstantiatePrefabForComponent<TileScript>(tilePrefab,
                        new Vector3(currentX, currentY, 0), Quaternion.identity, tilesParentObject);
                    tiles.Add(tileScript);
                    currentX++;
                }
                currentX = -startValue;
                currentY--;
            }

            Container.
                Bind<List<TileScript>>().
                FromInstance(tiles).
                AsSingle().
                NonLazy();
        }
    }
}