module FsLib.World

open System.Numerics
open FsLib.EntityComponent
open FsLib.Components

    

[<Struct>]
type World = {
    Inputs: Component<Input> array
    AttackAnimations: Component<AttackAnimation> array
    Directions: Component<Direction> array
    Velocities: Component<Velocity> array
    CurrentPositions: Component<Position> array }

let NewWorld = {
    Inputs = [||]
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

let AddHero entityId input attackAnimation position health world =
    { world with
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

let Update world =
    
    let nextAttackAnimations =
        world.AttackAnimations
        |> nextValueWithSameEntity2 Systems.calcAttackAnimation world.Inputs
        |> Array.ofSeq
        
    let nextDirections =
        world.Directions
        |> nextValueWithSameEntity2 Systems.calcDirectionWithInput world.Inputs
        |> Array.ofSeq
        
    let nextVelocities =
        world.Velocities
        |> nextValueWithSameEntity2 Systems.calcVelocityWithInput world.Inputs
        |> Array.ofSeq
        
    let nextPositions =
        world.CurrentPositions
        |> nextValueWithSameEntity2 Systems.calcPosition nextVelocities
        |> Array.ofSeq
        
    { world with
        AttackAnimations = nextAttackAnimations
        Directions = nextDirections 
        Velocities = nextVelocities
        CurrentPositions = nextPositions }
