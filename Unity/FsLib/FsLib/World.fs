module FsLib.World

open System.Numerics
open FsLib.EntityComponent
open FsLib.Components

    

[<Struct>]
type World = {
    Teams: Component<Team> array
    Inputs: Component<Input> array
    MoveTargets: Component<MoveTarget> array
    AttackAnimations: Component<AttackAnimation> array
    Directions: Component<Direction> array
    Velocities: Component<Velocity> array
    CurrentPositions: Component<Position> array }

let NewWorld = {
    Teams = [||]
    Inputs = [||]
    MoveTargets = [||] 
    AttackAnimations = [||]
    Directions = [||] 
    Velocities = [||]
    CurrentPositions = [||] }

let NewInput (input: Environment.IInput) = {
    Input = input
}

let NewAttackAnimation (animation: Environment.IAttackAnimation) = {
    IsPlaying = false
    Animation = animation
}

let AddHero entityId input attackAnimation position world =
    { world with
        Teams =
            world.Teams |> Array.append [| { EntityId = entityId; Value = Team(0) } |]
        Inputs =
            world.Inputs |> Array.append [| { EntityId = entityId; Value = NewInput input } |]
        AttackAnimations =
            world.AttackAnimations |> Array.append [| { EntityId = entityId; Value = NewAttackAnimation attackAnimation } |]
        Directions =
            world.Directions |> Array.append [| { EntityId = entityId; Value = Direction(0f) } |]
        Velocities =
            world.Velocities |> Array.append [| { EntityId = entityId; Value = Velocity(Vector2.Zero) } |]
        CurrentPositions =
            world.CurrentPositions |> Array.append [| { EntityId = entityId; Value = Position(position) } |]
            }
    
let AddEnemy entityId position world =
    { world with
        Teams =
            world.Teams |> Array.append [| { EntityId = entityId; Value = Team(1) } |]
        MoveTargets =
            world.MoveTargets |> Array.append [| { EntityId = entityId; Value = MoveTarget(None) } |]
        Directions =
            world.Directions |> Array.append [| { EntityId = entityId; Value = Direction(0f) } |]
        Velocities =
            world.Velocities |> Array.append [| { EntityId = entityId; Value = Velocity(Vector2.Zero) } |]
        CurrentPositions =
            world.CurrentPositions |> Array.append [| { EntityId = entityId; Value = Position(position) } |]
            }

let Update world =
    
    let nextMoveTargets =
        world.MoveTargets
        |> withSameEntity3 world.Teams world.CurrentPositions
        
        |> Seq.map (fun (entityId, moveTarget, thisTeam, thisPosition) ->
            let otherEntityIds = world.Teams |> Seq.choose (fun x -> if x.Value <> thisTeam then Some(x.EntityId) else None) |> Seq.toArray
            let otherPositions = world.CurrentPositions |> Seq.choose (fun x -> if (otherEntityIds |> Seq.contains x.EntityId) then Some(x.Value) else None)
            let target = Systems.calcMoveTarget otherPositions moveTarget thisPosition
            { EntityId = entityId; Value = MoveTarget(target) })
        |> Array.ofSeq
    
    let nextAttackAnimations =
        world.AttackAnimations
        |> nextValueWithSameEntity2 Systems.calcAttackAnimation world.Inputs
        |> Array.ofSeq
        
    let nextDirectionsWithInput =
        world.Directions
        |> nextValueWithSameEntity2 Systems.calcDirectionWithInput world.Inputs
        
    let nextDirectionsWithMoveTarget =
        world.Directions
        |> nextValueWithSameEntity3 Systems.calcDirectionWithMoveTarget world.CurrentPositions world.MoveTargets
        
    let nextDirections =
        nextDirectionsWithInput
        |> Seq.append nextDirectionsWithMoveTarget
        |> Array.ofSeq

    let nextVelocitiesWithInput =
        world.Velocities
        |> nextValueWithSameEntity2 Systems.calcVelocityWithInput world.Inputs
        
    let nextVelocitiesWithMoveTarget =
        world.Velocities
        |> nextValueWithSameEntity3 Systems.calcVelocityWithMoveTarget world.CurrentPositions world.MoveTargets
        
    let nextVelocities =
        nextVelocitiesWithInput
        |> Seq.append nextVelocitiesWithMoveTarget
        |> Array.ofSeq
        
    let nextPositions =
        world.CurrentPositions
        |> nextValueWithSameEntity2 Systems.calcPosition nextVelocities
        |> Array.ofSeq
        
    { world with
        MoveTargets = nextMoveTargets
        AttackAnimations = nextAttackAnimations
        Directions = nextDirections 
        Velocities = nextVelocities
        CurrentPositions = nextPositions }
