module FsLib.Systems

open System
open System.Numerics
open FsLib.Components


let calcMoveTarget (otherPositions: Position seq) _ (Position currentPosition) =
    let nearestPos, len = otherPositions
                        |> Seq.map (fun (Position x) -> x, (currentPosition - x).LengthSquared())
                        |> Seq.minBy (fun (_, len) -> len)
        
    match (nearestPos, len) with
    | nearest, len when len < 100.0f -> Some(nearest)
    | _ -> None
    
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
        
let calcDirectionWithMoveTarget (Direction direction) (Position currentPosition) (MoveTarget moveTarget) =
    match moveTarget with
    | Some(target) ->
        let diff = target - currentPosition
        (atan2 diff.Y diff.X) * 180.0f / float32 Math.PI
    | _ -> direction
    |> Direction

let calcVelocityWithInput _ input =
    let vx = if input.Input.Left() then -0.1f elif input.Input.Right() then 0.1f else 0.0f
    let vy = if input.Input.Up() then 0.1f elif input.Input.Down() then -0.1f else 0.0f

    Vector2(vx, vy)
    |> Velocity
    
let calcVelocityWithMoveTarget _ (Position currentPosition) (MoveTarget moveTarget) =
    match moveTarget with
    | Some(target) -> Vector2.Multiply(Vector2.Normalize(Vector2(target.X - currentPosition.X, target.Y - currentPosition.Y)), 0.05f)
    | _ -> Vector2.Zero
    |> Velocity
    
let calcPosition (Position current) (Velocity velocity) =
    current + velocity
    |> Position
    
    