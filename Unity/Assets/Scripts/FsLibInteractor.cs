using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FsLib;
using Vector2 = System.Numerics.Vector2;

public class FsLibInteractor : MonoBehaviour
{
    [SerializeField]
    public GameObject heroPrefab;

    private int _nextEntityId = 1;
    private readonly Dictionary<EntityComponent.EntityId, GameObject> _gameObjects = new Dictionary<EntityComponent.EntityId, GameObject>();
    private readonly Dictionary<EntityComponent.EntityId, Animator> _animators = new Dictionary<EntityComponent.EntityId, Animator>();
    private World.World _world;

    void Start()
    {
        _world = World.NewWorld;

        var entityId = EntityComponent.EntityId.NewEntityId(_nextEntityId);
        
        var newObject = Instantiate(heroPrefab, Vector3.zero, Quaternion.identity);
        var animator = newObject.GetComponent<Animator>();
        _gameObjects.Add(EntityComponent.EntityId.NewEntityId(_nextEntityId), newObject);
        _animators.Add(EntityComponent.EntityId.NewEntityId(_nextEntityId), animator);

        _world = World.AddHero(entityId, new InputEnvironment(), new AttackAnimationEnvironment(animator), Vector2.Zero, 10, _world);

        _nextEntityId++;
    }

    void Update()
    {
        _world = World.Update(_world);

        foreach (var component in _world.Directions)
        {
            var obj = _gameObjects[component.EntityId];
            obj.transform.rotation = Quaternion.Euler(0f, component.Value.Item, 0f);
        }

        foreach (var component in _world.CurrentPositions)
        {
            var obj = _gameObjects[component.EntityId];
            obj.transform.position = new Vector3()
            {
                x = component.Value.Item.X,
                y = obj.transform.position.y,
                z = component.Value.Item.Y,
            };
        }

        foreach (var component in _world.AttackAnimations)
        {
            var animator = _animators[component.EntityId];
            if (component.Value.IsPlaying && !animator.GetCurrentAnimatorStateInfo(0).IsName("HeroAttack"))
            {
                animator.Play("HeroAttack");
            }
        }
        
        
    }
}
