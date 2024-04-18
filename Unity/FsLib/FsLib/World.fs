module FsLib.World

open System.Numerics
open FsLib.EntityComponent
open FsLib.Components

    

[<Struct>]
type World = {
    Inputs: Component<Input> list
    AttackAnimations: Component<AttackAnimation> list
    Velocities: Component<Velocity> list
    CurrentPositions: Component<Position> list }

let NewWorld = {
    Inputs = []
    AttackAnimations = [] 
    Velocities = []
    CurrentPositions = [] }

let NewInput (input: Environment.IInput) = {
    Input = input
}

let NewAttackAnimation (animation: Environment.IAttackAnimation) = {
    Animation = animation
}

let AddHero entityId input attackAnimation position health world =
    { world with
        Inputs = { EntityId = entityId; Value = NewInput input } :: world.Inputs
        AttackAnimations = { EntityId = entityId; Value = NewAttackAnimation attackAnimation } :: world.AttackAnimations 
        Velocities = { EntityId = entityId; Value = Velocity(Vector2.Zero) } :: world.Velocities
        CurrentPositions = { EntityId = entityId; Value = Position(position) } :: world.CurrentPositions }

// let SetInput entityId input world =
//     let newInputs = Map.add entityId input world.Inputs
//     { world with Inputs = newInputs }
    
let Update world =
    
    let nextAttackAnimations =
        world.AttackAnimations
        |> nextValueWithSameEntity2 Systems.calcAttackAnimation world.Inputs
        |> List.ofSeq
        
    let nextVelocities =
        world.Velocities
        |> nextValueWithSameEntity2 Systems.calcVelocityWithInput world.Inputs
        |> List.ofSeq
        
    let nextPositions =
        world.CurrentPositions
        |> nextValueWithSameEntity2 Systems.calcPosition nextVelocities
        |> List.ofSeq
        
    { world with
        AttackAnimations = nextAttackAnimations
        Velocities = nextVelocities
        CurrentPositions = nextPositions }
