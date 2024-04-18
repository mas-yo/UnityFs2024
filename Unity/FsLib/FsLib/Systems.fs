module FsLib.Systems

open System.Numerics
open FsLib.Components


let calcVelocityWithInput _ input =
    let vx = if input.Input.Left() then -0.1f elif input.Input.Right() then 0.1f else 0.0f
    let vy = if input.Input.Up() then -0.1f elif input.Input.Down() then 0.1f else 0.0f

    Vector2(vx, vy)
    |> Velocity
    
let calcPosition (Position current) (Velocity velocity) =
    current + velocity
    |> Position
    
