module FsLib.Systems

open System
open System.Numerics
open FsLib.Components


let calcAttackAnimation attackAnimation input =
    if
        input.Input.Attack() && attackAnimation.Animation.IsPlaying() = false
    then
        attackAnimation.Animation.Play()
    attackAnimation
    
let calcDirectionWithInput (Direction direction) input =
    match input.Input.Left(), input.Input.Right(), input.Input.Up(), input.Input.Down() with
        | true, false, false, false -> -90f + 360f //MathF.PI
        | false, true, false, false -> 90f //0f
        | false, false, true, false -> 0f //MathF.PI * 1.5f
        | false, false, false, true -> 180f //MathF.PI * 0.5f
        | true, false, true, false -> -45f + 360f //MathF.PI * 5.0f / 4.0f
        | true, false, false, true -> -135f + 360f //MathF.PI * 3.0f / 4.0f
        | false, true, true, false -> 45f //MathF.PI * 7.0f / 4.0f
        | false, true, false, true -> 135f //MathF.PI / 04.0f
        | _ -> direction
    
    |> Direction
        
        

let calcVelocityWithInput _ input =
    let vx = if input.Input.Left() then -0.1f elif input.Input.Right() then 0.1f else 0.0f
    let vy = if input.Input.Up() then -0.1f elif input.Input.Down() then 0.1f else 0.0f

    Vector2(vx, vy)
    |> Velocity
    
let calcPosition (Position current) (Velocity velocity) =
    current + velocity
    |> Position
    
    