module FsLib.Components

open System
open System.Drawing
open System.Numerics
open FsLib.Environment

type Team = Team of int

[<Struct>]
type Input = {
    Input : IInput
}

type MoveTarget = MoveTarget of Vector2

type Velocity = Velocity of Vector2

type Position = Position of Vector2

type Direction = Direction of float32

[<Struct>]
type CharacterView = {
    Position : Vector2
    Direction : float32
    Radius : float32
    RadiusScale : float32
    Color : Color
    WeaponDirection : float32
}

[<Struct>]
type StatusBarView = {
    Position : Vector2
    Color : Color
    FrameLength : int
    AnimatedLength : int
    CurrentLength : int
}

[<Struct>]
type Health = {
    MaxHealth : int
    CurrentHealth : int
}

