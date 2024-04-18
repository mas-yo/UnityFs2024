using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FsLib;
using Vector2 = System.Numerics.Vector2;

public class FsLibInterface : MonoBehaviour
{
    [SerializeField]
    public GameObject heroPrefab;

    private int _nextEntityId = 1;
    private Dictionary<EntityComponent.EntityId, GameObject> _gameObjects = new Dictionary<EntityComponent.EntityId, GameObject>();
    private World.World _world;
    // Start is called before the first frame update
    void Start()
    {
        _world = World.NewWorld;

        var entityId = EntityComponent.EntityId.NewEntityId(_nextEntityId);
        
        _world = World.AddHero(entityId, Vector2.Zero, 10, _world);
        var newObject = Instantiate(heroPrefab, Vector3.zero, Quaternion.identity);
        _gameObjects.Add(EntityComponent.EntityId.NewEntityId(_nextEntityId), newObject);

        _nextEntityId++;
    }

    // Update is called once per frame
    void Update()
    {
        _world = World.SetInput(EntityComponent.EntityId.NewEntityId(1),
            new Components.Input(Input.GetKeyDown(KeyCode.A), Input.GetKeyDown(KeyCode.D), Input.GetKeyDown(KeyCode.W),
                Input.GetKeyDown(KeyCode.S), false), _world);
        _world = World.Update(_world);

        foreach (var pos in _world.CurrentPositions)
        {
            var obj = _gameObjects[pos.EntityId];
            obj.transform.position = new Vector3()
            {
                x = pos.Value.Item.X,
                y = obj.transform.position.y,
                z = pos.Value.Item.Y,
            };
        }
        
        
    }
}