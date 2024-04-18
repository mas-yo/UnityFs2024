module FsLib.Environment

type IAttackAnimation =
    abstract member Play: unit -> unit
    abstract member IsPlaying: unit -> bool

type IInput =
    abstract member Up: unit -> bool
    abstract member Down: unit -> bool
    abstract member Left: unit -> bool
    abstract member Right: unit -> bool
    abstract member Attack: unit -> bool