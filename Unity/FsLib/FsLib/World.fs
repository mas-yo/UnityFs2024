module FsLib.World

open FsLib.EntityComponent
open FsLib.Components

[<Struct>]
type World = {
    Inputs: Component<Input> list
    Velocities: Component<Velocity> list
    CurrentPositions: Component<Position> list }

let NewWorld = {
    Inputs = List.Empty
    Velocities = List.Empty
    CurrentPositions = List.Empty }

// let AddHero (world:World) (Position position) (Health health) =
//     world.Velocities = List.Empty
    