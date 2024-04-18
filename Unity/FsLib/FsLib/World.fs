module FsLib.World

open System.Numerics
open FsLib.EntityComponent
open FsLib.Components

    

[<Struct>]
type World = {
    Inputs: Component<Input> list
    Velocities: Component<Velocity> list
    CurrentPositions: Component<Position> list }

let NewWorld = {
    Inputs = []
    Velocities = []
    CurrentPositions = [] }

let NewInput (input: Environment.IInput) = {
    Input = input
}

let AddHero entityId input position health world =
    { world with
        Inputs = { EntityId = entityId; Value = NewInput(input) } :: world.Inputs
        Velocities = { EntityId = entityId; Value = Velocity(Vector2.Zero) } :: world.Velocities
        CurrentPositions = { EntityId = entityId; Value = Position(position) } :: world.CurrentPositions }

// let SetInput entityId input world =
//     let newInputs = Map.add entityId input world.Inputs
//     { world with Inputs = newInputs }
    
let Update world =
    let nextVelocities =
        world.Velocities
        |> nextValueWithSameEntity2 Systems.calcVelocityWithInput world.Inputs
        |> List.ofSeq
        
    let nextPositions =
        world.CurrentPositions
        |> nextValueWithSameEntity2 Systems.calcPosition nextVelocities
        |> List.ofSeq
        
    { world
      with Velocities = nextVelocities; CurrentPositions = nextPositions }
