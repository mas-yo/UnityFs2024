module FsLib.Components

type Team = Team of int

[<Struct>]
type Input = {
    left : bool
    right : bool
    up : bool
    down : bool
    attack : bool
}

