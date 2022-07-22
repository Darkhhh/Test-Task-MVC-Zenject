using System.Collections.Generic;
using Tiles;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TilesInstaller : MonoInstaller
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Transform parentObject;
        [SerializeField][Range(1,10)] private int tilesInRow;
        
        public override void InstallBindings()
        {
            CreateTiles();
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
                        new Vector3(currentX, currentY, 0), Quaternion.identity, parentObject);
                    tiles.Add(tileScript);
                    currentX++;
                }
                currentX = -startValue;
                currentY--;
            }

            Container.
                Bind<List<TileScript>>().
                FromInstance(tiles).
                AsSingle();
        }
    }
}