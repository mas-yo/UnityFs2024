module FsLib.Systems

open System.Numerics
open FsLib.Components


let calcVelocityWithInput _ input =
    let vx = if input.Left then -2.0f elif input.Right then 2.0f else 0.0f
    let vy = if input.Up then -2.0f elif input.Down then 2.0f else 0.0f

    Vector2(vx, vy)
    |> Velocity
    
let calcPosition (Position current) (Velocity velocity) =
    current + velocity
    |> Position
    
