module Tests

open System
open Xunit
open FsLib.Components
open FsLib.World

[<Fact>]
let TestAddHero () =
    let w = NewWorld
    AddHero w
    Assert.Equal(1, w.Velocities.Length)
