module FsLib.Systems

open System
open System.Numerics
open FsLib.Components


let calcAttackAnimation attackAnimation input =
    let nextIsPlaying =
        match input.Input.Attack(), attackAnimation.Animation.IsPlaying() with
        | true, true -> true
        | true, false -> true
        | false, true -> true
        | false, false -> false
    
    { attackAnimation with
        IsPlaying = nextIsPlaying }
    
let calcDirectionWithInput (Direction direction) input =
    match input.Input.Left(), input.Input.Right(), input.Input.Up(), input.Input.Down() with
        | true, false, false, false -> -90f
        | false, true, false, false -> 90f
        | false, false, true, false -> 0f
        | false, false, false, true -> 180f
        | true, false, true, false -> -45f
        | true, false, false, true -> -135f
        | false, true, true, false -> 45f
        | false, true, false, true -> 135f
        | _ -> direction
    
    |> Direction
        
        

let calcVelocityWithInput _ input =
    let vx = if input.Input.Left() then -0.1f elif input.Input.Right() then 0.1f else 0.0f
    let vy = if input.Input.Up() then 0.1f elif input.Input.Down() then -0.1f else 0.0f

    Vector2(vx, vy)
    |> Velocity
    
let calcPosition (Position current) (Velocity velocity) =
    current + velocity
    |> Position
    
    