module FsLib.World

open System.Numerics
open FsLib.EntityComponent
open FsLib.Components

[<Struct>]
type World = {
    Inputs: Map<EntityId, Input>
    Velocities: Component<Velocity> list
    CurrentPositions: Component<Position> list }

let NewWorld = {
    Inputs = Map []
    Velocities = []
    CurrentPositions = [] }

let NewInput = {
    Left = false
    Right = false
    Up = false
    Down = false
    Attack = false
}

let AddHero entityId position health world =
    { Inputs = Map.add entityId NewInput world.Inputs
      Velocities = { EntityId = entityId; Value = Velocity(Vector2.Zero) } :: world.Velocities
      CurrentPositions = { EntityId = entityId; Value = Position(position) } :: world.CurrentPositions }

let SetInput entityId input world =
    let newInputs = Map.add entityId input world.Inputs
    { world with Inputs = newInputs }